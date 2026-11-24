using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Un signal nommé = une source + une chaîne de mappings.
    /// Calculé une fois par frame, lu par autant de sorties que nécessaire.
    /// </summary>
    [Serializable]
    public class Signal
    {
        [Tooltip("Nom utilisé par les sorties et par l'API (ex. \"Vitesse\").")]
        public string name = "Signal";

        [SerializeReference] public ISignalSource source;
        [SerializeReference] public List<IValueMapping> mappings = new List<IValueMapping>();

        /// <summary>Valeur après mappings, lue par les sorties.</summary>
        public float Value { get; private set; }

        /// <summary>Direction fournie par la source.</summary>
        public Vector3 Direction => source != null ? source.Direction : Vector3.zero;

        public void Initialize(in FeedbackContext ctx)
        {
            // TODO [T2] Initialiser la source et remettre les mappings à zéro.
        }

        /// <summary>Échantillonne la source puis applique les mappings dans l'ordre.</summary>
        public void Evaluate(in FeedbackContext ctx)
        {
            // TODO [T2] Value = mappings appliqués à source.Value (ne pas ré-échantillonner ici
            // une source physique : elle l'a été en FixedUpdate).
        }

        public void Reset()
        {
            // TODO [T2] Reset source + mappings, Value = 0.
        }
    }
}
