using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Base des sorties qui modifient une propriété de matériau via le MaterialApplier
    /// (MaterialPropertyBlock : l'asset du matériau n'est jamais modifié).
    /// </summary>
    [Serializable]
    public abstract class MaterialOutput : FeedbackOutput
    {
        [Tooltip("Nom de la propriété du shader. URP Lit : _BaseColor, _EmissionColor, _Smoothness…")]
        public string propertyName = "_BaseColor";

        [Tooltip("Index du matériau sur le renderer (0 pour un seul matériau).")]
        [Min(0)] public int materialIndex;

        public BlendMode blend = BlendMode.Replace;

        /// <summary>Identifiant de la propriété, calculé une fois (Shader.PropertyToID).</summary>
        protected int PropertyId { get; private set; }

        public override void Initialize(in FeedbackContext ctx)
        {
            // TODO [T16] PropertyId = Shader.PropertyToID(propertyName).
            // TODO [T16] Avertir si le matériau n'a pas cette propriété (material.HasProperty).
        }
    }
}
