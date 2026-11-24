// Squelette : champs privés pas encore utilisés -> avertissements masqués. Retirer la ligne une fois implémenté.
#pragma warning disable CS0169, CS0414, CS0649

using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Lisse la valeur dans le temps (amortissement), pour éviter les à-coups.
    /// </summary>
    [Serializable]
    public class SmoothingMapping : IValueMapping
    {
        [Min(0f)] public float smoothTime = 0.1f;

        float _current;
        float _velocity;
        bool _hasValue;

        public float Map(float input, float deltaTime)
        {
            // TODO [T6] Mathf.SmoothDamp(_current, input, ref _velocity, smoothTime, Mathf.Infinity, deltaTime).
            // TODO [T6] Première valeur : prendre input directement.
            return input;
        }

        public void Reset()
        {
            _current = 0f;
            _velocity = 0f;
            _hasValue = false;
        }
    }
}
