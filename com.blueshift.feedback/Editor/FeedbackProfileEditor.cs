using UnityEditor;
using UnityEngine.UIElements;

namespace BlueShift.Feedback.Editor
{
    /// <summary>Inspecteur des presets FeedbackProfile.</summary>
    [CustomEditor(typeof(FeedbackProfile))]
    public class FeedbackProfileEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            // TODO [T18] Réutiliser les mêmes éléments que FeedbackControllerEditor (signaux + sorties).
            return null;
        }
    }
}
