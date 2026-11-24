using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Intensité d'impact : changement brutal du vecteur vitesse (Δv, en m/s) entre deux pas. Direction = direction du changement.
    /// </summary>
    [Serializable]
    public class ImpactSource : ISignalSource
    {
        [Tooltip("Vrai : lit le Rigidbody en FixedUpdate. Faux : mesure le déplacement par frame.")]
        public bool usePhysics = true;

        public float Value { get; private set; }
        public Vector3 Direction { get; private set; }
        public bool UsesPhysicsStep => usePhysics;

        public void Initialize(in FeedbackContext ctx)
        {
            // TODO [T4] Mémoriser la vitesse initiale ; pas de choc détecté au premier pas.
        }

        public void Sample(in FeedbackContext ctx)
        {
            // TODO [T4] Δv = (vitesse - vitessePrécédente).magnitude, sans diviser par dt (indépendant du framerate).
            // TODO [T4] Value = Δv ; Direction = (vitesse - vitessePrécédente).normalized.
        }

        public void Reset()
        {
            Value = 0f;
            Direction = Vector3.zero;
            // TODO [T4] Oublier la vitesse précédente.
        }
    }
}
