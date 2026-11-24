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
            if (source == null)
            {
                return;
            }

            source.Initialize(ctx);
            foreach (IValueMapping mapping in mappings)
            {
                if (mapping == null)
                {
                    continue;
                }

                mapping.Reset();
            }
        }

        /// <summary>Échantillonne la source puis applique les mappings dans l'ordre.</summary>
        public void Evaluate(in FeedbackContext ctx)
        {
            if (source == null)
            {
                Value = 0;
                return;
            }

            if (!source.UsesPhysicsStep)
            {
                source.Sample(ctx);
            }

            float value = source.Value;

            foreach (IValueMapping mapping in mappings)
            {
                if (mapping == null)
                {
                    continue;
                }

                value = mapping.Map(value, ctx.DeltaTime);
            }

            Value = value;
        }

        public void Reset()
        {
            Value = 0;
            if (source != null)
            {
                source.Reset();
            }
            foreach (IValueMapping mapping in mappings)
            {
                if (mapping == null)
                {
                    continue;
                }

                mapping.Reset();
            }
        }
    }
}