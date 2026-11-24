using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>Utilitaires mathématiques partagés par les sorties de déformation.</summary>
    public static class FeedbackMath
    {
        /// <summary>
        /// Construit un multiplicateur de scale à partir d'un facteur sur un axe.
        /// Ex. axis = Z, factor = 1.5 → (1, 1, 1.5) ; avec preserveVolume → (1/√1.5, 1/√1.5, 1.5).
        /// </summary>
        public static Vector3 AxisScale(AxisMode axis, float factor, bool preserveVolume)
        {
            // TODO [T3] Gérer X, Y, Z et Uniform.
            // TODO [T3] Conservation du volume : les deux autres axes valent 1 / sqrt(factor).
            // TODO [T3] AxisMode.Velocity : renvoyer le facteur sur Z ; l'alignement se fait par AlignToDirection.
            return Vector3.one;
        }

        /// <summary>
        /// Matrice de déformation qui étire de "factor" le long de "direction" (espace local du Visual).
        /// Utilisée pour AxisMode.Velocity et par le MeshApplier (phase 5).
        /// </summary>
        public static Matrix4x4 StretchAlong(Vector3 direction, float factor, bool preserveVolume)
        {
            // TODO [T3/T13] R * S * R^-1 avec R qui aligne Z sur direction.
            return Matrix4x4.identity;
        }

        /// <summary>Interpole un multiplicateur vers la valeur neutre (1,1,1) selon le poids.</summary>
        public static Vector3 WeightScale(Vector3 scale, float weight)
        {
            return Vector3.LerpUnclamped(Vector3.one, scale, weight);
        }
    }
}
