using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Ramène la valeur entre 0 et 1 : min → 0, max → 1.
    /// </summary>
    [Serializable]
    public class NormalizeMapping : IValueMapping
    {
        public float min = 0f;
        public float max = 10f;
        public bool clamp = true;

        public float Map(float input, float deltaTime)
        {
            // TODO [T6] InverseLerp (non clampé si clamp == false), gérer min == max.
            return input;
        }

        public void Reset()
        {
        }
    }
}
