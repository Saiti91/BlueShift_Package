namespace BlueShift.Feedback
{
    /// <summary>Façon dont une sortie se combine avec les autres sorties visant la même propriété.</summary>
    public enum BlendMode
    {
        /// <summary>Remplace la valeur (interpolée par le poids).</summary>
        Replace,
        /// <summary>Multiplie la valeur.</summary>
        Multiply,
        /// <summary>Ajoute à la valeur.</summary>
        Add
    }
}
