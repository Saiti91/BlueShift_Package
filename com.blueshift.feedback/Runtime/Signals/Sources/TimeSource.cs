using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Temps écoulé depuis l'activation, en secondes. Sert aux sorties périodiques (Heartbeat, Pulse).
    /// </summary>
    [Serializable]
    public class TimeSource : ISignalSource
    {
        [Tooltip("Vrai : ignore Time.timeScale (continue pendant une pause du jeu).")]
        public bool unscaled;
        [Tooltip("Multiplicateur de vitesse du temps.")]
        public float speed = 1f;

        public float Value { get; private set; }
        public Vector3 Direction { get; private set; }
        public bool UsesPhysicsStep => false;

        public void Initialize(in FeedbackContext ctx)
        {
            // TODO [T5] Mémoriser l'instant de départ.
        }

        public void Sample(in FeedbackContext ctx)
        {
            // TODO [T5] Value = (temps courant - départ) * speed, scaled ou unscaled.
        }

        public void Reset()
        {
            Value = 0f;
            Direction = Vector3.zero;
            // TODO [T5] Repartir de zéro.
        }
    }
}
