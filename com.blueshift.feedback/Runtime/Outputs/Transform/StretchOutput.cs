using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Étire l'objet proportionnellement au signal (ex. balle qui s'allonge avec sa vitesse).
    /// </summary>
    [Serializable]
    public class StretchOutput : TransformOutput
    {
        [Tooltip("Facteur ajouté par unité de signal.")]
        public float amount = 0.5f;
        [Tooltip("Facteur maximum.")]
        public float maxFactor = 2f;

        public override void Apply(float value, Vector3 direction, in FeedbackContext ctx, FeedbackAppliers appliers)
        {
            // TODO [T8] factor = min(1 + value * amount, maxFactor) ; ApplyFactor(factor, direction, appliers).
        }
    }
}
