using UnityEditor;
using UnityEngine.UIElements;

namespace BlueShift.Feedback.Editor
{
    /// <summary>
    /// Inspecteur du FeedbackController (UI Toolkit).
    /// NB : "Editor" est aussi le nom de ce namespace, d'où UnityEditor.Editor écrit en entier.
    /// </summary>
    [CustomEditor(typeof(FeedbackController))]
    public class FeedbackControllerEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            // TODO [T18] Section "Visual" + "Profile" (bouton "Enregistrer comme profil").
            // TODO [T18] Liste des signaux : nom, menu de choix de la source, liste des mappings.
            // TODO [T18] Liste des sorties : menu "Ajouter" listant les types (TypeCache.GetTypesDerivedFrom<FeedbackOutput>()),
            //            liste déroulante du signal lu, poids, activé.
            // TODO [T18] En Play mode : afficher la valeur courante de chaque signal.
            // TODO [T11] Avertissements : Visual = racine, noms en double, signal introuvable.
            return null; // null = inspecteur par défaut en attendant
        }
    }
}
