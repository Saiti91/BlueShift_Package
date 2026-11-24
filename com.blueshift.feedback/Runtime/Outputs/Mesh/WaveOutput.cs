// Squelette : champs privés pas encore utilisés -> avertissements masqués. Retirer la ligne une fois implémenté.
#pragma warning disable CS0169, CS0414, CS0649

using System;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Onde qui parcourt l'objet (drapeau, gelée qui ondule).
    /// </summary>
    [Serializable]
    public class WaveOutput : MeshOutput
    {
        public float amplitude = 0.05f;
        public float wavelength = 0.5f;
        public float speed = 2f;

        float _phase;
        float _intensity;

        protected override void Prepare(float value, Vector3 direction, in FeedbackContext ctx)
        {
            // TODO [T14] _phase avance avec speed * dt ; _intensity = value * weight.
        }

        public override Vector3 Displace(Vector3 vertex, Bounds bounds)
        {
            // TODO [T14] vertex += normale approx. * sin(position le long d'un axe / wavelength + _phase) * amplitude * _intensity.
            return vertex;
        }
    }
}
