using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Base de toutes les sorties : ce qui réagit à un signal (déformation, matériau, plus tard VFX et son).
    /// Les sorties ne modifient jamais l'objet directement : elles contribuent à un applier.
    /// </summary>
    [Serializable]
    public abstract class FeedbackOutput
    {
        [Tooltip("Nom de la sortie, utilisé par l'API (Trigger, SetWeight…).")]
        public string name = "Output";

        [Tooltip("Nom du signal lu. Vide = sortie pilotée uniquement par Trigger.")]
        public string signal;

        public bool enabled = true;

        [Range(0f, 1f)] public float weight = 1f;

        /// <summary>Appelé une fois au démarrage.</summary>
        public virtual void Initialize(in FeedbackContext ctx) { }

        /// <summary>
        /// Calcule la contribution de la frame et la transmet à l'applier concerné.
        /// value = valeur du signal (0 si pas de signal), direction = direction du signal.
        /// </summary>
        public abstract void Apply(float value, Vector3 direction, in FeedbackContext ctx, FeedbackAppliers appliers);

        /// <summary>Remet l'état interne à zéro (impulsions en cours, phase…).</summary>
        public virtual void Reset() { }
    }
}
