// Squelette : champs privés pas encore utilisés -> avertissements masqués. Retirer la ligne une fois implémenté.
#pragma warning disable CS0169, CS0414, CS0649

using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Plie l'objet : plus un sommet est haut, plus il est décalé dans la direction du signal.
    /// </summary>
    [Serializable]
    public class BendOutput : MeshOutput
    {
        [Tooltip("Décalage maximal au sommet de l'objet, en unités locales.")]
        public float amount = 0.3f;
        public AnimationCurve heightFalloff = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        Vector3 _bendOffset;

        protected override void Prepare(float value, Vector3 direction, in FeedbackContext ctx)
        {
            // TODO [T14] _bendOffset = -direction (en local) * value * amount * weight.
        }

        public override Vector3 Displace(Vector3 vertex, Bounds bounds)
        {
            // TODO [T14] h = hauteur normalisée du sommet dans bounds ; vertex += _bendOffset * heightFalloff(h).
            return vertex;
        }
    }
}
