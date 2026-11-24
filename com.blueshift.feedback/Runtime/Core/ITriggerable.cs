namespace BlueShift.Feedback
{
    /// <summary>
    /// Sortie qui réagit à un déclenchement ponctuel (squash, shake, flash…),
    /// en plus ou à la place d'un signal continu. Appelé par FeedbackController.Trigger(nom, intensité).
    /// </summary>
    public interface ITriggerable
    {
        /// <summary>Démarre l'effet. intensity entre 0 et 1 en usage normal.</summary>
        void Trigger(float intensity);
    }
}
