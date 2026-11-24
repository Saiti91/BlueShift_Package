using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Pilote n'importe quelle propriété numérique du shader (lissage, découpe, intensité personnalisée…).
    /// </summary>
    [Serializable]
    public class FloatPropertyOutput : MaterialOutput
    {
        public AnimationCurve valueCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        public override void Apply(float value, Vector3 direction, in FeedbackContext ctx, FeedbackAppliers appliers)
        {
            // TODO [T16] appliers.Material.SetFloat(materialIndex, PropertyId, valueCurve.Evaluate(value), blend, weight).
        }
    }
}
