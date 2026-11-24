# BlueShift — Plan et répartition

Nov 24, 2026 · @Stéphane

## Objectif et périmètre

BlueShift est un package Unity qui donne du feedback visuel à des objets selon leur vitesse, leurs chocs ou le temps : déformation (scale du transform, puis sommets du mesh, calculés sur le CPU) et modification de leurs matériaux (couleur, émission). Il se configure sans code dans l'inspecteur et se pilote par une API publique.

| Contrainte | Choix |
| --- | --- |
| Version d'Unity | 6.3.23f1 |
| Calcul de la déformation | CPU uniquement (transform puis mesh) |
| Render pipeline | Sans impact sur le runtime, puisque rien ne passe par un shader. Ne compte que pour les matériaux des samples. |
| Volume visé | Quelques objets déformés en même temps, un `LateUpdate` par objet |
| NaughtyAttributes | Dépendance optionnelle, activée seulement si le package est installé |
| Configuration | Données (composant générique + presets ScriptableObject), pas de génération de code |

Hors périmètre de la v1 : déformation par shader (GPU), `SkinnedMeshRenderer` (personnages animés), centaines d'objets simultanés, netteté (flou ou brillance), VFX et son. Les VFX et le son viendront plus tard : l'architecture les accueille comme de nouveaux types de sortie, sans toucher au noyau.

Pour connaître votre pipeline : *Edit → Project Settings → Graphics*, champ *Default Render Pipeline*. S'il est vide, c'est le Built-in ; sinon le nom de l'asset indique URP ou HDRP. Un projet créé avec le modèle « Universal 3D » d'Unity 6 est en URP.

## Architecture

Un composant `FeedbackController` par objet contient deux listes : des **signaux** (ce qui mesure) et des **sorties** (ce qui réagit). Chaque sortie lit un signal par son nom, et un même signal peut piloter autant de sorties qu'on veut.

&#91;embedded content: architecture · signaux, sorties, appliers\]

La vitesse de la balle est calculée une seule fois et pilote à la fois sa couleur, son émission et son étirement. Les appliers regroupent ensuite toutes les sorties d'un même type et écrivent une seule fois par frame.

- **Signal** = source + mapping, avec un nom. Sources : `RigidbodyVelocity`, `FrameDelta`, `Impact` (Δv), `Time`, `Manual`. Mappings : `Normalize`, `Curve`, `Threshold`, `Smoothing`.
- **Sortie** = ce qui reçoit la valeur. Les sorties *continues* (couleur, stretch) suivent le signal à chaque frame. Les sorties *événementielles* (squash, plus tard son de choc ou burst de particules) implémentent `ITriggerable` et réagissent à un déclenchement.
- **Axe** (sorties de déformation) : X, Y, Z, uniforme ou aligné sur la vitesse, avec conservation du volume en option.
- **Appliers** : `TransformApplier` (scale + offset), `MeshApplier` (sommets CPU, phase 5), `MaterialApplier` (`MaterialPropertyBlock`). Ils agissent sur l'enfant Visual, jamais sur la racine.
- **Mélange** : scales multipliés, offsets additionnés ; pour les matériaux, chaque sortie choisit *remplacer*, *multiplier* ou *ajouter*. Le poids interpole vers la valeur neutre.
- **Timing** : sources physiques échantillonnées en `FixedUpdate`, évaluation et écriture en `LateUpdate`.
- **Configuration en données** : listes `[SerializeReference]` et presets `FeedbackProfile` (ScriptableObject), sans génération de code.
- **Plus tard, VFX et son** : de nouveaux types de sortie (taux d'émission de particules, pitch et volume, son à l'impact), sans changement du noyau.

```csharp
public interface ISignalSource   { void Sample(in FeedbackContext ctx); float Value { get; } Vector3 Direction { get; } }
public interface IValueMapping   { float Map(float input); }
public abstract class FeedbackOutput { public string signal; public float weight = 1f; public abstract void Evaluate(float value, in FeedbackContext ctx); }
public interface ITriggerable    { void Trigger(float intensity); }
```

## Structure du package

Le package suit le format UPM : deux assemblies (Runtime et Editor) pour que le code d'éditeur ne parte jamais dans un build, et des samples importables depuis le Package Manager.

```
com.blueshift.feedback/
├── package.json
├── README.md · CHANGELOG.md · LICENSE.md
├── Runtime/                       BlueShift.Feedback.asmdef
│   ├── Core/                      interfaces, FeedbackContext, Signal, AxisMode, BlendMode
│   ├── FeedbackController.cs      composant : signaux, sorties, appliers, API publique
│   ├── FeedbackProfile.cs         preset ScriptableObject
│   ├── Signals/
│   │   ├── Sources/               RigidbodyVelocity, FrameDelta, Impact, Time, Manual
│   │   └── Mappings/              Normalize, Curve, Threshold, Smoothing
│   ├── Outputs/
│   │   ├── Transform/             Stretch, Squash, Shake, Heartbeat, Pulse
│   │   ├── Mesh/                  Bend, Wave, Jelly
│   │   └── Material/              Color, Emission, FloatProperty
│   └── Appliers/                  TransformApplier, MeshApplier, MaterialApplier
├── Editor/                        BlueShift.Feedback.Editor.asmdef (plateforme : Editor)
│   ├── FeedbackControllerEditor.cs  inspecteur UI Toolkit : signaux + sorties
│   ├── FeedbackWizard.cs          fenêtre de création
│   └── Naughty/                   compilé seulement si NaughtyAttributes est présent
├── Tests/
│   ├── Editor/                    signaux, mappings, maths de déformation et de mélange
│   └── Runtime/                   FeedbackController sur de vrais objets
└── Samples~/
    ├── Balls/                     balle physique et balle sans physique
    ├── Heartbeat/
    └── Jelly/
```

- **Namespace** : `BlueShift.Feedback` pour le runtime, `BlueShift.Feedback.Editor` pour l'éditeur (réglé via `rootNamespace` dans chaque asmdef).
- **NaughtyAttributes optionnel** : dans l'asmdef Editor, une entrée `versionDefines` sur `com.dbrizov.naughtyattributes` définit `BLUESHIFT_NAUGHTY`, et le code du dossier `Naughty/` est entouré de `#if BLUESHIFT_NAUGHTY`.
- **Pendant le développement** : le package est à la racine du dépôt, à côté du projet de test `Wololo_Project`, qui le référence en local dans son `manifest.json` (`file:../../com.blueshift.feedback`). Il reste modifiable directement depuis Unity.

## Plan détaillé par phases

Neuf phases, chacune terminée par un résultat testable dans Unity. La phase 1 se fait à deux, car elle fige les contrats dont tout le reste dépend. Ensuite, chacun avance en parallèle.

0. **Mise en place** (fait en grande partie)
   - Dépôt GitHub, projet de test `Wololo_Project` en Unity 6000.3.23f1 et URP.
   - Package `com.blueshift.feedback` référencé en local, asmdefs `BlueShift.Feedback`, `testables` dans le manifest.
   - Reste : supprimer les fichiers d'exemple du modèle, renommer les fichiers `.asmdef`.
   - *Terminé quand* : le package apparaît sous le nom BlueShift dans le Package Manager et le projet compile chez vous deux.
1. **Noyau runtime** (à deux)
   - Interfaces : `ISignalSource`, `IValueMapping`, `FeedbackOutput`, `IApplier`, `ITriggerable`.
   - `FeedbackContext` : temps, deltaTime, transform cible, Rigidbody éventuel.
   - `Signal` : nom, source, mapping. `FeedbackOutput` : nom du signal lu, poids, activée.
   - `FeedbackController` : listes `[SerializeReference]` de signaux et de sorties, échantillonnage en `FixedUpdate` pour les sources physiques, évaluation en `LateUpdate`, écriture unique par applier.
   - `TransformApplier` : écrit le scale (et un offset de position) sur l'enfant Visual.
   - `AxisMode` : X, Y, Z, Uniforme, Aligné sur la vitesse.
   - *Terminé quand* : un signal Vitesse et une sortie Stretch codés en dur reproduisent notre balle physique actuelle.
2. **Signaux : sources et mappings**
   - Sources : `RigidbodyVelocity`, `FrameDelta` (moyenne sur N frames), `Impact` (Δv par pas), `Time` (temps normal ou non affecté par `timeScale`), `Manual`.
   - Mappings : `Normalize` (min/max → 0..1), `Curve` (AnimationCurve), `Threshold`, `Smoothing`.
   - Tests EditMode sur chaque source et chaque mapping.
   - *Terminé quand* : les tests passent, et la balle sans physique s'étire comme celle avec Rigidbody.
3. **Sorties de déformation (transform)**
   - `Stretch`, `Squash` (impulsion + retour, `ITriggerable`), `Shake` (bruit de Perlin, offset), `Heartbeat` (courbe périodique réglée en BPM), `Pulse`.
   - Règle de combinaison : les scales se multiplient, les offsets s'additionnent, le poids interpole vers le neutre.
   - *Terminé quand* : une scène montre chaque sortie seule, puis stretch + squash + shake ensemble sans conflit.
4. **API publique et événements**
   - `Trigger(nom, intensité)`, `SetWeight`, `SetOutputEnabled`, `SetSignalValue`, `ResetAll`, `Pause`.
   - `UnityEvent` exposés dans l'inspecteur (ex. : impact détecté).
   - Messages d'erreur clairs si un nom de signal ou de sortie n'existe pas.
   - *Terminé quand* : un script de test et un bouton d'UI pilotent les sorties sans toucher au package.
5. **Applier mesh CPU**
   - `MeshApplier` : copie du mesh par objet, sommets d'origine en cache, écriture via `Mesh.SetVertices` sans allocation par frame, recalcul des normales et des bounds.
   - Les sorties transform marchent aussi en mode mesh (même déformation exprimée en matrice).
   - Sorties propres au mesh : `Bend`, `Wave`, `Jelly`.
   - Avertissement dans l'inspecteur si le mesh n'a pas *Read/Write* activé.
   - *Terminé quand* : un cube subdivisé se plie et ondule, et un objet sans feedback retrouve son mesh d'origine.
6. **Sorties matériau**
   - `MaterialApplier` : un `MaterialPropertyBlock` par renderer et par index de matériau, écrit une fois par frame, nettoyé quand la sortie est désactivée.
   - Sorties : `Color` (Gradient évalué avec le signal normalisé), `Emission` (couleur HDR × intensité), `FloatProperty` (n'importe quelle propriété numérique du shader).
   - Nom de propriété libre, avec préréglages URP Lit (`_BaseColor`, `_EmissionColor`).
   - Modes de mélange : remplacer, multiplier, ajouter.
   - Avertissement si l'émission n'est pas activée sur le matériau, et rappel qu'un Bloom est nécessaire pour l'effet lumineux.
   - *Terminé quand* : la balle change de couleur avec sa vitesse et brille à l'impact, sans modifier l'asset du matériau ni les autres balles.
7. **Éditeur**
   - `FeedbackProfile` (ScriptableObject) et bouton « Enregistrer comme profil ».
   - Inspecteur UI Toolkit : liste des signaux, liste des sorties avec menu « Ajouter » par type, choix du signal dans une liste déroulante, aperçu en Play mode.
   - Assistant : crée l'enfant Visual, ajoute le `FeedbackController`, propose des presets.
   - Attributs NaughtyAttributes optionnels sur les champs simples.
   - *Terminé quand* : quelqu'un qui n'a jamais vu le code configure une balle cartoon en moins de 5 minutes.
8. **Samples, documentation, version 0.1.0**
   - Samples : balles, cœur qui bat, gelée. Presets fournis.
   - README (installation, démarrage rapide, API), CHANGELOG, tag git `v0.1.0`.
   - *Terminé quand* : le package s'installe par URL git dans un projet vierge et les samples marchent.

## Scène de test

Une scène « bac à sable » par personne permet de tester chaque phase dès qu'elle avance, sans toucher au package. Elle se monte pendant la phase 0 et s'enrichit au fil des phases. Les scripts de test (spawner, déplacement de la balle sans physique, affichage de debug) vivent dans `Assets/Sandbox/`, jamais dans le package.

```
Sandbox_A (scène)
├── Main Camera            (0, 6, -12), tournée vers le centre
├── Directional Light
├── Environment
│   ├── Ground             Plane, scale (5, 1, 5), layer Obstacle
│   └── Walls              3 ou 4 Cubes avec BoxCollider, layer Obstacle
├── Spawner                (0, 1, -5), forward vers +Z, BallSpawnScript
├── Heart                  objet fixe pour les effets liés au temps
│   └── Visual             le mesh, déformé par le FeedbackController
├── JellyBlock             mesh subdivisé pour les effets mesh (phase 5)
│   └── Visual
└── DebugOverlay           affichage des valeurs + curseur de timeScale
```

1. **Créer la scène** : `Assets/Sandbox/Scenes/Sandbox_A.unity` (et `Sandbox_B` pour l'autre). Chacun ne modifie que la sienne.
2. **Créer les layers** `Ball` et `Obstacle` (*Edit → Project Settings → Tags and Layers*). Dans *Physics → Layer Collision Matrix*, décocher Ball × Ball pour que les balles ne se gênent pas entre elles.
3. **Monter l'environnement** : sol et murs sur le layer Obstacle. Placer un mur face au spawner à environ 10 m, pour avoir des chocs à chaque tir.
4. **Prefab balle physique** (`BallPhysics`) :
   - Racine : `SphereCollider` (rayon 0,5) + `Rigidbody` (Interpolate, Collision Detection *Continuous*, rotations gelées), layer Ball.
   - Enfant `Visual` : une Sphere dont on retire le collider. C'est elle que le `FeedbackController` déforme.
   - Un Physic Material rebondissant (Bounciness 0,6, Bounce Combine *Maximum*), pour tester la détection de choc par Δv.
5. **Prefab balle sans physique** (`BallKinematic`) : même structure sans `Rigidbody`, plus un script de test `BallMover` qui la fait avancer et rebondir avec un `SphereCast` sur le layer Obstacle (le code de notre balle actuelle, sans la partie déformation).
6. **Entrées** : dans les actions du projet (*Project Settings → Input System Package*), deux actions `Shoot` (clic gauche) et `ShootAlt` (clic droit). Le spawner tire `BallPhysics` sur l'une et `BallKinematic` sur l'autre. Corriger au passage la force : le spawner regarde vers +Z, donc `transform.forward * ShootingForce`, sans le signe moins.
7. **Objet « Heart »** : un cube fixe avec un enfant Visual, pour les effets qui ne dépendent que du temps (Heartbeat, Pulse) et pour tester l'API (`Trigger`, `SetSignalValue`) depuis des boutons d'UI.
8. **Objet « JellyBlock »** (phase 5) : un mesh avec assez de sommets pour se plier. Le Cube par défaut d'Unity n'a que 24 sommets, trop peu pour Bend ou Wave. Utiliser un cube subdivisé fait avec ProBuilder ou importé depuis Blender, avec *Read/Write* activé dans ses réglages d'import.
9. **Debug** : un script `DebugOverlay` qui affiche à l'écran la vitesse, le Δv et le scale courant de la dernière balle tirée, avec un curseur `Time.timeScale` (0,1 à 1) pour voir les chocs au ralenti. La fenêtre *Window → Analysis → Physics Debugger* aide à visualiser les colliders.

Ce qu'on vérifie à chaque phase :

| Phase | Test dans la scène |
| --- | --- |
| 1 | La balle physique s'étire comme aujourd'hui, avec le `FeedbackController` à la place de `BallScript` |
| 2 | Les deux balles s'étirent pareil à vitesse égale ; l'overlay affiche des valeurs cohérentes |
| 3 | Squash visible sur chaque choc contre le mur, Heart bat en continu |
| 4 | Les boutons d'UI déclenchent le squash et changent le rythme du cœur |
| 5 | JellyBlock se plie et ondule ; désactiver le `FeedbackController` lui rend sa forme d'origine |
| 6 | La balle change de couleur avec sa vitesse et brille à l'impact ; les autres balles et l'asset du matériau ne changent pas (Bloom ajouté au Volume de la scène) |
| 7 | Refaire la balle physique uniquement avec l'assistant, sans écrire de code |

## Répartition des tâches

**Stéphane** s'occupe du moteur : sources de signaux, sorties de déformation, mesh. **Aymeric** prend ce qui se règle ou se voit autour : mappings, API, matériaux, éditeur. Chacun a 9 tâches, plus les tâches communes.

| # | Tâche | Phase | Qui | Dépend de | Statut |
| --- | --- | --- | --- | --- | --- |
| 1 | Dépôt GitHub, projet de test, squelette du package, ménage du modèle | 0 | Stéphane + Aymeric | — | En cours |
| 2 | Interfaces, `FeedbackContext`, `Signal`, `FeedbackOutput` | 1 | Stéphane + Aymeric | 1 | À faire |
| 3 | `FeedbackController` (boucle, combinaison) + `TransformApplier` + `AxisMode` | 1 | Stéphane + Aymeric | 2 | À faire |
| 4 | Sources `RigidbodyVelocity`, `FrameDelta`, `Impact` | 2 | Stéphane | 3 | À faire |
| 5 | Sources `Time`, `Manual` | 2 | Stéphane | 3 | À faire |
| 6 | Mappings `Normalize`, `Curve`, `Threshold`, `Smoothing` + tests | 2 | Aymeric | 3 | À faire |
| 7 | Tests EditMode des sources | 2 | Stéphane | 4, 5 | À faire |
| 8 | Sorties `Stretch`, `Squash` (`ITriggerable`) | 3 | Stéphane | 4 | À faire |
| 9 | Sorties `Shake`, `Heartbeat`, `Pulse` | 3 | Stéphane | 5, 6 | À faire |
| 10 | API publique (`Trigger`, `SetWeight`, `SetSignalValue`…) | 4 | Aymeric | 8, 9 | À faire |
| 11 | `UnityEvent` dans l'inspecteur + messages d'erreur | 4 | Aymeric | 10 | À faire |
| 12 | `MeshApplier` (cache, `SetVertices`, normales, bounds) | 5 | Stéphane | 3 | À faire |
| 13 | Sorties transform exprimées en matrice pour le mode mesh | 5 | Stéphane | 8, 12 | À faire |
| 14 | Sorties mesh `Bend`, `Wave`, `Jelly` | 5 | Stéphane | 12 | À faire |
| 15 | `MaterialApplier` (`MaterialPropertyBlock`, modes de mélange) | 6 | Aymeric | 3 | À faire |
| 16 | Sorties `Color`, `Emission`, `FloatProperty` + préréglages URP Lit | 6 | Aymeric | 6, 15 | À faire |
| 17 | `FeedbackProfile` (ScriptableObject) | 7 | Aymeric | 3 | À faire |
| 18 | Inspecteur UI Toolkit (signaux + sorties) | 7 | Aymeric | 9, 16, 17 | À faire |
| 19 | Assistant de création (enfant Visual, presets) | 7 | Aymeric | 18 | À faire |
| 20 | Samples balles + gelée | 8 | Stéphane | 14, 16 | À faire |
| 21 | Sample cœur, presets, README, CHANGELOG | 8 | Aymeric | 19 | À faire |
| 22 | Test d'installation par URL git, tag `v0.1.0` | 8 | Stéphane + Aymeric | 20, 21 | À faire |

Trois points de synchronisation, à faire ensemble :

1. **Fin de la phase 1** : les interfaces sont figées. Toute modification ensuite passe par une discussion, car elle casse le travail de l'autre.
2. **Fins des phases 3 et 6** : Aymeric s'appuie sur la liste des sorties pour construire l'inspecteur. On vérifie ensemble que chaque sortie expose des champs sérialisables propres.
3. **Avant la phase 8** : revue croisée complète. Chacun teste le travail de l'autre dans un projet vierge.

Le chemin le plus long passe par les matériaux puis l'éditeur (tâches 15 → 16 → 18 → 19 → 21), tous chez Aymeric. Si ça coince, Stéphane peut reprendre la tâche 16 ou 17 une fois le mesh terminé.

## Conventions de travail à deux

La principale source de conflits sur un projet Unity à plusieurs, ce sont les scènes et les prefabs modifiés par deux personnes à la fois. Ces règles servent surtout à éviter ça.

- **Réglages Unity** (à vérifier chez vous deux) : *Project Settings → Editor*, *Asset Serialization* en **Force Text**. Les fichiers `.meta` toujours commités avec leur asset.
- **Branches** : `main` toujours fonctionnelle, une branche `feature/<tache>` par tâche du tableau (ex. : `feature/12-mesh-applier`). Fusion par pull request.
- **Revue** : chaque pull request est relue par l'autre avant fusion. C'est aussi ce qui permet à chacun de comprendre la partie de l'autre.
- **Scènes** : une scène de test par personne (`Sandbox_A`, `Sandbox_B`), jamais modifiées par l'autre. Les scènes partagées ne vivent que dans `Samples~`.
- **Contrats** : un changement d'interface du dossier `Core/` se discute avant d'être codé et fait l'objet de sa propre pull request.
- **Nommage** : classes en PascalCase, champs privés `_camelCase`, champs sérialisés privés avec `[SerializeField]` plutôt que publics. Un fichier par classe.
- **Suivi** : mettez à jour la colonne Statut du tableau ci-dessus quand une tâche change d'état.

## Questions ouvertes et risques

| Sujet | Risque | Parade |
| --- | --- | --- |
| NaughtyAttributes et `[SerializeReference]` | Ne gère probablement pas les listes polymorphes, qui sont le cœur de l'inspecteur | Inspecteur personnalisé en UI Toolkit. NaughtyAttributes seulement sur les champs simples. À vérifier dès la phase 1. |
| Interpolation du Rigidbody | Scale calculé en `LateUpdate` à partir d'une vitesse mesurée en `FixedUpdate` : saccades possibles | Lissage de la source, et tests avec l'interpolation activée puis désactivée |
| Déformation de la racine | Le collider et les enfants seraient déformés, et `forward` dévié | Convention de l'enfant Visual, créé par l'assistant, et avertissement si le `FeedbackController` cible la racine |
| Mesh non lisible | Le `MeshApplier` échoue si *Read/Write* est désactivé | Avertissement dans l'inspecteur avec bouton pour corriger le réglage d'import |
| Performance du mesh CPU | Meshes très détaillés coûteux à chaque frame | Pas d'allocation par frame, et limite de sommets documentée dans le README |
| Renommage d'un type sérialisé | Un type sous `[SerializeReference]` renommé casse les objets déjà configurés | `[MovedFrom]` sur les types renommés, et noms figés avant la v0.1.0 |

Questions à trancher ensemble :

- [x] Nom du package : `com.blueshift.feedback`, nom affiché BlueShift
- [x] Render pipeline du projet de test : URP 17.3
- [x] Hébergement git : GitHub
- [x] Répartition : Stéphane (A) et Aymeric (B)
- [ ] `Dossier du package renommé ; reste à renommer les fichiers .asmdef (à la main)`
- [ ] `EditorBuildSettings.asset n'est plus ignoré : à commiter (liste des scènes du build partagée)`
