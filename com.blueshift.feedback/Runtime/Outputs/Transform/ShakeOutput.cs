// Squelette : champs privés pas encore utilisés -> avertissements masqués. Retirer la ligne une fois implémenté.
#pragma warning disable CS0169, CS0414, CS0649

using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Tremblement : décalage de position du Visual par bruit de Perlin. Continu (signal) ou ponctuel (Trigger).
    /// </summary>
    [Serializable]
    public class ShakeOutput : FeedbackOutput, ITriggerable
    {
        [Tooltip("Amplitude maximale du décalage, en unités locales.")]
        public float amplitude = 0.05f;
        [Tooltip("Fréquence du bruit.")]
        public float frequency = 25f;
        [Tooltip("Durée d'un tremblement déclenché par Trigger.")]
        public float triggerDuration = 0.3f;

        float _triggerTime;
        float _triggerIntensity;
        float _seed;

        public override void Apply(float value, Vector3 direction, in FeedbackContext ctx, FeedbackAppliers appliers)
        {
            // TODO [T9] intensité = max(value, intensité du Trigger en cours qui décroît sur triggerDuration).
            // TODO [T9] offset = (Perlin(t*frequency, seed) - 0.5) * 2 * amplitude * intensité sur X, Y, Z.
            // TODO [T9] appliers.Transform.AddOffset(offset * weight).
        }

        public override void Initialize(in FeedbackContext ctx)
        {
            // TODO [T9] _seed aléatoire, pour que deux objets ne tremblent pas à l'identique.
        }

        public void Trigger(float intensity)
        {
            // TODO [T9] Mémoriser l'instant et l'intensité.
        }
    }
}
