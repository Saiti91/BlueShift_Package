// Squelette : champs privés pas encore utilisés -> avertissements masqués. Retirer la ligne une fois implémenté.
#pragma warning disable CS0169, CS0414, CS0649

using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Gelée : le haut de l'objet suit le bas avec du retard, comme un ressort amorti.
    /// </summary>
    [Serializable]
    public class JellyOutput : MeshOutput
    {
        [Tooltip("Raideur du ressort.")]
        public float stiffness = 60f;
        [Tooltip("Amortissement.")]
        public float damping = 6f;
        public float maxOffset = 0.3f;

        Vector3 _springPosition;
        Vector3 _springVelocity;

        protected override void Prepare(float value, Vector3 direction, in FeedbackContext ctx)
        {
            // TODO [T14] Ressort amorti qui suit le mouvement de la racine (direction * value) ; borner à maxOffset.
        }

        public override Vector3 Displace(Vector3 vertex, Bounds bounds)
        {
            // TODO [T14] vertex += décalage du ressort * hauteur normalisée du sommet.
            return vertex;
        }
    }
}
