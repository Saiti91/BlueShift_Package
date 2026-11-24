// Squelette : champs privés pas encore utilisés -> avertissements masqués. Retirer la ligne une fois implémenté.
#pragma warning disable CS0169, CS0414, CS0649

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Composant principal : calcule les signaux, évalue les sorties, fait écrire les appliers.
    /// À placer sur la racine de l'objet ; "visual" pointe sur l'enfant qui porte le mesh.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("BlueShift/Feedback Controller")]
    public class FeedbackController : MonoBehaviour
    {
        [Tooltip("Enfant déformé et coloré. Si vide, cherche un enfant nommé \"Visual\".")]
        [SerializeField] Transform visual;

        [Tooltip("Preset optionnel. S'il est renseigné, ses signaux et sorties remplacent les listes locales au démarrage.")]
        [SerializeField] FeedbackProfile profile;

        [SerializeField] List<Signal> signals = new List<Signal>();
        [SerializeReference] List<FeedbackOutput> outputs = new List<FeedbackOutput>();

        [Tooltip("Appelé à chaque Trigger, avec le nom de la sortie et l'intensité.")]
        public UnityEvent<string, float> onTriggered = new UnityEvent<string, float>();

        readonly FeedbackAppliers _appliers = new FeedbackAppliers();
        readonly Dictionary<string, Signal> _signalsByName = new Dictionary<string, Signal>();
        Rigidbody _body;

        /// <summary>Met le feedback en pause : plus aucune mise à jour, l'objet garde son état courant.</summary>
        public bool Paused { get; set; }

        public IReadOnlyList<Signal> Signals => signals;
        public IReadOnlyList<FeedbackOutput> Outputs => outputs;

        void Awake()
        {
            // TODO [T3] Résoudre visual (champ, sinon enfant "Visual", sinon avertissement + transform).
            // TODO [T3] Avertir si visual == transform (le collider serait déformé).
            // TODO [T17] Si profile != null : copier (cloner) ses signaux et sorties.
            // TODO [T3] Récupérer le Rigidbody, remplir _signalsByName, créer les appliers nécessaires
            //           selon le type des sorties, appeler Initialize partout.
            _body = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            // TODO [T3] Échantillonner les sources dont UsesPhysicsStep == true.
        }

        void LateUpdate()
        {
            if (Paused) return;
            // TODO [T3] 1. Construire le FeedbackContext.
            // TODO [T3] 2. Échantillonner les sources non physiques, évaluer tous les signaux.
            // TODO [T3] 3. _appliers.BeginFrame().
            // TODO [T3] 4. Pour chaque sortie active : Apply(valeur et direction de son signal).
            // TODO [T3] 5. _appliers.EndFrame().
        }

        void OnDisable()
        {
            // TODO [T3] Rendre l'état d'origine : _appliers.Restore().
        }

        // ------------------------------------------------------------------ API publique (phase 4)

        /// <summary>Déclenche une sortie ITriggerable par son nom.</summary>
        public void Trigger(string outputName, float intensity = 1f)
        {
            // TODO [T10] Trouver la sortie, vérifier ITriggerable, appeler Trigger, invoquer onTriggered.
            // TODO [T11] Message d'erreur clair si le nom n'existe pas ou si la sortie n'est pas déclenchable.
        }

        /// <summary>Change le poids d'une sortie (0 = sans effet, 1 = plein effet).</summary>
        public void SetWeight(string outputName, float weight)
        {
            // TODO [T10]
        }

        /// <summary>Active ou désactive une sortie.</summary>
        public void SetOutputEnabled(string outputName, bool enabled)
        {
            // TODO [T10]
        }

        /// <summary>Fixe la valeur d'un signal dont la source est ManualSource.</summary>
        public void SetSignalValue(string signalName, float value)
        {
            // TODO [T10] Erreur claire si la source n'est pas un ManualSource.
        }

        /// <summary>Lit la valeur courante d'un signal (après mappings).</summary>
        public float GetSignalValue(string signalName)
        {
            // TODO [T10]
            return 0f;
        }

        /// <summary>Remet signaux et sorties à zéro et rend à l'objet sa forme et ses matériaux d'origine.</summary>
        public void ResetAll()
        {
            // TODO [T10]
        }

        void OnValidate()
        {
            // TODO [T11] Signaler les noms en double et les sorties qui lisent un signal inexistant.
        }
    }
}
