using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Mesure brute d'un signal : vitesse, Δv, temps, valeur manuelle…
    /// Implémentations dans Runtime/Signals/Sources. Les classes doivent être [Serializable]
    /// pour apparaître dans les listes [SerializeReference].
    /// </summary>
    public interface ISignalSource
    {
        /// <summary>Valeur brute mesurée (avant mapping).</summary>
        float Value { get; }

        /// <summary>Direction associée (ex. direction de la vitesse), Vector3.zero si sans objet.
        /// Sert aux sorties dont l'axe est AxisMode.Velocity.</summary>
        Vector3 Direction { get; }

        /// <summary>Vrai si la source doit être échantillonnée en FixedUpdate (sources physiques).</summary>
        bool UsesPhysicsStep { get; }

        /// <summary>Appelé une fois au démarrage du FeedbackController.</summary>
        void Initialize(in FeedbackContext ctx);

        /// <summary>Mesure de la frame (FixedUpdate si UsesPhysicsStep, sinon LateUpdate).</summary>
        void Sample(in FeedbackContext ctx);

        /// <summary>Remet la source à zéro (buffers, valeur précédente…).</summary>
        void Reset();
    }
}
