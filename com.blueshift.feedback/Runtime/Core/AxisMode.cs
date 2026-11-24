namespace BlueShift.Feedback
{
    /// <summary>Axe sur lequel une sortie de déformation agit.</summary>
    public enum AxisMode
    {
        X,
        Y,
        Z,
        /// <summary>Les trois axes à la fois.</summary>
        Uniform,
        /// <summary>Axe aligné sur la direction du signal (ex. direction de la vitesse).</summary>
        Velocity
    }
}
