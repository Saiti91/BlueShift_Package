using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Base des sorties de déformation par transform (scale + offset de position du Visual).
    /// Les sous-classes calculent un facteur ; cette base l'applique sur le bon axe.
    /// </summary>
    [Serializable]
    public abstract class TransformOutput : FeedbackOutput
    {
        public AxisMode axis = AxisMode.Z;
        public bool preserveVolume = true;

        /// <summary>Envoie un facteur d'échelle sur l'axe choisi au TransformApplier.</summary>
        protected void ApplyFactor(float factor, Vector3 direction, FeedbackAppliers appliers)
        {
            // TODO [T3] FeedbackMath.AxisScale (ou StretchAlong pour AxisMode.Velocity),
            //           pondéré par weight, puis appliers.Transform.AddScale(...).
        }
    }
}
