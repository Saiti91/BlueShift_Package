using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Vitesse calculée à partir du déplacement de la racine, moyennée sur N frames. Pour les objets sans Rigidbody.
    /// </summary>
    [Serializable]
    public class FrameDeltaSource : ISignalSource
    {
        [Min(1)] public int sampleCount = 5;

        public float Value { get; private set; }
        public Vector3 Direction { get; private set; }
        public bool UsesPhysicsStep => false;

        public void Initialize(in FeedbackContext ctx)
        {
            // TODO [T4] Allouer les buffers circulaires (déplacements + deltaTime), mémoriser la position.
        }

        public void Sample(in FeedbackContext ctx)
        {
            // TODO [T4] Ajouter le déplacement de la frame au buffer, ignorer deltaTime == 0.
            // TODO [T4] Value = |somme des déplacements| / somme des durées ; Direction = déplacement moyen normalisé.
        }

        public void Reset()
        {
            Value = 0f;
            Direction = Vector3.zero;
            // TODO [T4] Vider les buffers, re-mémoriser la position.
        }
    }
}
