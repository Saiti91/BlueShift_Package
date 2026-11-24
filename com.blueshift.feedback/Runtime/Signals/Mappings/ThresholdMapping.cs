using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Ignore les valeurs sous un seuil (ex. ne réagir qu'aux vrais chocs).
    /// </summary>
    [Serializable]
    public class ThresholdMapping : IValueMapping
    {
        public float threshold = 3f;
        [Tooltip("Vrai : soustrait le seuil (la sortie démarre à 0 au seuil). Faux : 0 sous le seuil, valeur brute au-dessus.")]
        public bool subtract = true;

        public float Map(float input, float deltaTime)
        {
            // TODO [T6]
            return input;
        }

        public void Reset()
        {
        }
    }
}
