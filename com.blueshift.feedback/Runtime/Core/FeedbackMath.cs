using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>Utilitaires mathématiques partagés par les sorties de déformation.</summary>
    public static class FeedbackMath
    {
        /// <summary>
        /// Construit un multiplicateur de scale qui étire un objet sur un axe.
        /// Le facteur va sur l'axe choisi ; les deux autres axes valent 1,
        /// ou 1/√facteur si preserveVolume est vrai (l'objet s'amincit quand il s'allonge).
        /// Ex. : axis = Z, factor = 2 → (1, 1, 2) ; avec preserveVolume → (0.71, 0.71, 2).
        /// AxisMode.Velocity renvoie le même résultat que Z : l'orientation vers la vitesse se fait avec StretchAlong.
        /// AxisMode.Uniform applique le facteur aux trois axes et ignore preserveVolume.
        /// Un facteur inférieur à 0.01 est ramené à 0.01.
        /// </summary>
        /// <param name="axis">Axe à étirer.</param>
        /// <param name="factor">Facteur d'étirement : 1 = neutre, 2 = deux fois plus long, 0.5 = écrasé de moitié.</param>
        /// <param name="preserveVolume">Vrai pour compenser sur les deux autres axes et garder le volume constant.</param>
        /// <returns>Le multiplicateur à appliquer au localScale (1, 1, 1 = aucun changement).</returns>
        public static Vector3 AxisScale(AxisMode axis, float factor, bool preserveVolume)
        {
            factor = Mathf.Max(factor, 0.01f);
            float side;
            if (preserveVolume)
                side = 1f / Mathf.Sqrt(factor);
            else
                side = 1f;
            switch (axis)
            {
                case AxisMode.X:
                    return new Vector3(factor, side, side);
                case AxisMode.Y:
                    return new Vector3(side, factor, side);
                case AxisMode.Z:
                case AxisMode.Velocity:
                    return new Vector3(side, side, factor);
                case AxisMode.Uniform:
                    return new Vector3(factor, factor, factor);
                default:
                    return Vector3.one;
            }
        }

        /// <summary>
        /// Construit une matrice qui étire un objet le long d'une direction quelconque
        /// (ex. la direction de vol d'une balle), et pas seulement sur X, Y ou Z.
        /// Principe : on tourne pour amener la direction sur Z, on étire sur Z avec AxisScale,
        /// puis on tourne en sens inverse (r * s * rInv).
        /// Si la direction est nulle ou quasi nulle (objet immobile), renvoie l'identité : aucun changement.
        /// Utilisée pour AxisMode.Velocity et par le MeshApplier (phase 5).
        /// </summary>
        /// <param name="direction">Direction d'étirement, dans l'espace local du Visual. Sa longueur n'a pas d'importance.</param>
        /// <param name="factor">Facteur d'étirement le long de la direction : 1 = neutre, 2 = deux fois plus long.</param>
        /// <param name="preserveVolume">Vrai pour amincir l'objet perpendiculairement à la direction et garder le volume constant.</param>
        /// <returns>La matrice de déformation à appliquer aux sommets ou à combiner avec les autres déformations.</returns>
        public static Matrix4x4 StretchAlong(Vector3 direction, float factor, bool preserveVolume)
        {
            if (direction.sqrMagnitude < 0.000001f)
            {
                return Matrix4x4.identity;
            }
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward,direction.normalized);
            Matrix4x4 r = Matrix4x4.Rotate(rotation);
            Matrix4x4 s = Matrix4x4.Scale(AxisScale(AxisMode.Z, factor, preserveVolume));
            
            Matrix4x4 rInv = Matrix4x4.Rotate(rotation).inverse;
            return r*s*rInv;
        }

        /// <summary>Interpole un multiplicateur vers la valeur neutre (1,1,1) selon le poids.</summary>
        public static Vector3 WeightScale(Vector3 scale, float weight)
        {
            return Vector3.LerpUnclamped(Vector3.one, scale, weight);
        }
    }
}
