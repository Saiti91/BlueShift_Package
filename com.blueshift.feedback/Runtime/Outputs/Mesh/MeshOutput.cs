using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Base des sorties qui déplacent les sommets du mesh (phase 5).
    /// Le MeshApplier appelle Displace pour chaque sommet, après avoir appelé Prepare une fois par frame.
    /// </summary>
    [Serializable]
    public abstract class MeshOutput : FeedbackOutput
    {
        public override void Apply(float value, Vector3 direction, in FeedbackContext ctx, FeedbackAppliers appliers)
        {
            // TODO [T12] Prepare(value, direction, ctx) puis appliers.Mesh.Register(this).
        }

        /// <summary>Calcule les paramètres de la frame (une fois, pas par sommet).</summary>
        protected abstract void Prepare(float value, Vector3 direction, in FeedbackContext ctx);

        /// <summary>
        /// Renvoie la position déformée d'un sommet (espace local du mesh).
        /// bounds = bounds d'origine du mesh, utiles pour normaliser la hauteur du sommet.
        /// </summary>
        public abstract Vector3 Displace(Vector3 vertex, Bounds bounds);
    }
}
