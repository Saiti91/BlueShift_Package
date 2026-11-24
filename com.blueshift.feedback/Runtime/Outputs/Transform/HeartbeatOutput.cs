using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Battement de cœur : pulsation double (lub-dub) à un rythme donné en BPM. À brancher sur un TimeSource.
    /// </summary>
    [Serializable]
    public class HeartbeatOutput : TransformOutput
    {
        [Tooltip("Battements par minute. Peut être piloté par un second signal via l'API.")]
        public float bpm = 70f;
        [Tooltip("Gonflement maximal (0.15 = +15 %).")]
        public float amplitude = 0.15f;
        [Tooltip("Forme d'un battement sur une période (x de 0 à 1, y de 0 à 1).")]
        public AnimationCurve beatShape = new AnimationCurve(
            new Keyframe(0f, 0f), new Keyframe(0.1f, 1f), new Keyframe(0.2f, 0.2f),
            new Keyframe(0.3f, 0.7f), new Keyframe(0.45f, 0f), new Keyframe(1f, 0f));

        public override void Apply(float value, Vector3 direction, in FeedbackContext ctx, FeedbackAppliers appliers)
        {
            // TODO [T9] phase = frac(value * bpm / 60) ; factor = 1 + beatShape.Evaluate(phase) * amplitude.
            // TODO [T9] ApplyFactor(factor, direction, appliers) — axe Uniform conseillé.
        }
    }
}
