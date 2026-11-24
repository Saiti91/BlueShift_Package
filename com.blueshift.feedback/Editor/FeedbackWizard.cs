using UnityEditor;
using UnityEngine.UIElements;

namespace BlueShift.Feedback.Editor
{
    /// <summary>
    /// Assistant de création : prépare un objet pour BlueShift en quelques clics.
    /// </summary>
    public class FeedbackWizard : EditorWindow
    {
        [MenuItem("Tools/BlueShift/Feedback Wizard")]
        public static void Open()
        {
            GetWindow<FeedbackWizard>("BlueShift");
        }

        void CreateGUI()
        {
            rootVisualElement.Add(new Label("BlueShift — assistant (à venir)"));
            // TODO [T19] Étape 1 : objet sélectionné dans la scène (Selection.activeGameObject).
            // TODO [T19] Étape 2 : créer l'enfant "Visual" et y déplacer MeshFilter + MeshRenderer (avec Undo).
            // TODO [T19] Étape 3 : choisir un preset (balle cartoon, cœur, gelée…) ou partir de zéro.
            // TODO [T19] Étape 4 : ajouter le FeedbackController configuré, sélectionner l'objet.
        }
    }
}
