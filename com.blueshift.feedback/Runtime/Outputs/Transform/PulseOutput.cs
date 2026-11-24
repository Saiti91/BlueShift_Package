using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Pulsation sinusoïdale simple (respiration, objet qui attire l'attention). À brancher sur un TimeSource.
    /// </summary>
    [Serializable]
    public class PulseOutput : TransformOutput
    {
        [Tooltip("Pulsations par seconde.")]
        public float frequency = 1f;
        public float amplitude = 0.1f;

        public override void Apply(float value, Vector3 direction, in FeedbackContext ctx, FeedbackAppliers appliers)
        {
            // TODO [T9] factor = 1 + sin(value * frequency * 2π) * amplitude ; ApplyFactor(...).
        }
    }
}
