// Squelette : champs privés pas encore utilisés -> avertissements masqués. Retirer la ligne une fois implémenté.
#pragma warning disable CS0169, CS0414, CS0649

using System.Collections.Generic;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Combine les sorties matériau et les écrit dans un MaterialPropertyBlock par index de matériau.
    /// L'asset du matériau n'est jamais modifié, les autres objets ne sont pas affectés.
    /// </summary>
    public class MaterialApplier : IApplier
    {
        Renderer _renderer;
        readonly Dictionary<int, MaterialPropertyBlock> _blocks = new Dictionary<int, MaterialPropertyBlock>();

        public void Initialize(in FeedbackContext ctx)
        {
            // TODO [T15] Récupérer le Renderer du Visual ; avertir s'il n'y en a pas.
        }

        public void BeginFrame()
        {
            // TODO [T15] Repartir des valeurs d'origine des propriétés touchées (lues sur le matériau).
        }

        /// <summary>Contribue une couleur à une propriété.</summary>
        public void SetColor(int materialIndex, int propertyId, Color color, BlendMode blend, float weight)
        {
            // TODO [T15] Combiner selon blend (Replace = lerp par weight, Multiply, Add).
        }

        /// <summary>Contribue une valeur numérique à une propriété.</summary>
        public void SetFloat(int materialIndex, int propertyId, float value, BlendMode blend, float weight)
        {
            // TODO [T15]
        }

        public void EndFrame(in FeedbackContext ctx)
        {
            // TODO [T15] _renderer.SetPropertyBlock(block, materialIndex) pour chaque index touché.
        }

        public void Restore()
        {
            // TODO [T15] Retirer les property blocks (SetPropertyBlock(null, index)).
        }
    }
}
