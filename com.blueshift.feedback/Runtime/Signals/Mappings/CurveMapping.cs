using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Passe la valeur dans une AnimationCurve (en général après une normalisation).
    /// </summary>
    [Serializable]
    public class CurveMapping : IValueMapping
    {
        public AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        public float Map(float input, float deltaTime)
        {
            // TODO [T6] curve.Evaluate(input), gérer curve == null.
            return input;
        }

        public void Reset()
        {
        }
    }
}
