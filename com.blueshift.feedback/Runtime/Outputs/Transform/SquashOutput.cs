// Squelette : champs privés pas encore utilisés -> avertissements masqués. Retirer la ligne une fois implémenté.
#pragma warning disable CS0169, CS0414, CS0649

using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Écrase l'objet lors d'un impact, puis le laisse revenir à sa forme. Déclenché par un signal (ImpactSource) ou par Trigger.
    /// </summary>
    [Serializable]
    public class SquashOutput : TransformOutput, ITriggerable
    {
        [Range(0f, 0.9f)] public float maxSquash = 0.5f;
        [Tooltip("Intensité par unité de signal au-dessus du seuil du mapping.")]
        public float amount = 0.05f;
        [Tooltip("Vitesse de retour à la normale (par seconde).")]
        public float recovery = 4f;

        float _squash;

        public override void Apply(float value, Vector3 direction, in FeedbackContext ctx, FeedbackAppliers appliers)
        {
            // TODO [T8] Si value > 0 : _squash = max(_squash, min(value * amount, maxSquash)).
            // TODO [T8] _squash retombe vers 0 avec recovery * ctx.DeltaTime.
            // TODO [T8] ApplyFactor(1 - _squash, direction, appliers).
        }

        public void Trigger(float intensity)
        {
            // TODO [T8] _squash = max(_squash, intensity * maxSquash).
        }

        public override void Reset()
        {
            _squash = 0f;
        }
    }
}
