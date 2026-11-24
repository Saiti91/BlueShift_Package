using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Vitesse du Rigidbody de la racine (m/s). Direction = direction du mouvement.
    /// </summary>
    [Serializable]
    public class RigidbodyVelocitySource : ISignalSource
    {
        [Tooltip("Vrai : vitesse dans le repère local de la racine. Faux : repère monde.")]
        public bool localSpace;

        public float Value { get; private set; }
        public Vector3 Direction { get; private set; }
        public bool UsesPhysicsStep => true;

        public void Initialize(in FeedbackContext ctx)
        {
            // TODO [T4] Avertir si ctx.Body est null (objet sans Rigidbody : utiliser FrameDeltaSource).
        }

        public void Sample(in FeedbackContext ctx)
        {
            // TODO [T4] Value = ctx.Body.linearVelocity.magnitude ; Direction = vitesse normalisée.
            // TODO [T4] Garder la dernière direction connue quand la vitesse est quasi nulle.
        }

        public void Reset()
        {
            Value = 0f;
            Direction = Vector3.zero;

        }
    }
}
