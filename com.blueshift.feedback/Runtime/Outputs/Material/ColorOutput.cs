using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Couleur évaluée dans un dégradé selon le signal (0 → début, 1 → fin). Normaliser le signal avant.
    /// </summary>
    [Serializable]
    public class ColorOutput : MaterialOutput
    {
        public Gradient gradient = new Gradient();

        public override void Apply(float value, Vector3 direction, in FeedbackContext ctx, FeedbackAppliers appliers)
        {
            // TODO [T16] appliers.Material.SetColor(materialIndex, PropertyId, gradient.Evaluate(clamp01(value)), blend, weight).
        }
    }
}
