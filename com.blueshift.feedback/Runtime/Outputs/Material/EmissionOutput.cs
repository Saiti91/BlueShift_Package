using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Émission lumineuse pilotée par le signal. Le matériau doit avoir l'émission activée, et la scène un Bloom pour l'effet lumineux.
    /// </summary>
    [Serializable]
    public class EmissionOutput : MaterialOutput
    {
        [ColorUsage(false, true)] public Color color = Color.white;
        [Tooltip("Intensité HDR au signal maximal.")]
        public float maxIntensity = 4f;

        public override void Apply(float value, Vector3 direction, in FeedbackContext ctx, FeedbackAppliers appliers)
        {
            // TODO [T16] appliers.Material.SetColor(materialIndex, PropertyId, color * value * maxIntensity, blend, weight).
        }

        public override void Initialize(in FeedbackContext ctx)
        {
            base.Initialize(ctx);
            // TODO [T16] propertyName par défaut "_EmissionColor" ; avertir si le mot-clé _EMISSION n'est pas activé.
        }
    }
}
