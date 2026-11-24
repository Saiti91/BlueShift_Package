namespace BlueShift.Feedback
{
    /// <summary>
    /// Transformation appliquée à la valeur brute d'une source (normalisation, courbe, seuil, lissage).
    /// Les mappings d'un signal s'enchaînent dans l'ordre de la liste.
    /// </summary>
    public interface IValueMapping
    {
        /// <summary>Transforme la valeur. deltaTime sert aux mappings qui ont un état (lissage).</summary>
        float Map(float input, float deltaTime);

        /// <summary>Remet l'état interne à zéro.</summary>
        void Reset();
    }
}
