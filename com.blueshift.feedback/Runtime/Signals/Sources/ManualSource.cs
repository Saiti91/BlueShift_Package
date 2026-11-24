using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Valeur fournie par le code de l'utilisateur via FeedbackController.SetSignalValue (vie, stress, charge…).
    /// </summary>
    [Serializable]
    public class ManualSource : ISignalSource
    {
        [Tooltip("Valeur de départ.")]
        public float initialValue;

        public float Value { get; private set; }
        public Vector3 Direction { get; private set; }
        public bool UsesPhysicsStep => false;

        public void Initialize(in FeedbackContext ctx)
        {
            Value = initialValue;
        }

        public void Sample(in FeedbackContext ctx)
        {
            // Rien à mesurer : la valeur est fixée par SetValue.
        }

        public void Reset()
        {
            Value = 0f;
            Direction = Vector3.zero;
            // TODO [T5] Revenir à initialValue plutôt qu'à 0 ?
        }

        /// <summary>Fixe la valeur (appelé par FeedbackController.SetSignalValue).</summary>
        public void SetValue(float value)
        {
            Value = value;
        }
    }
}
