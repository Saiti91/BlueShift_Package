namespace BlueShift.Feedback
{
    /// <summary>
    /// Écrit le résultat combiné de toutes les sorties d'un même type, une seule fois par frame.
    /// Cycle : Initialize → (BeginFrame → contributions des sorties → EndFrame) chaque frame → Restore.
    /// </summary>
    public interface IApplier
    {
        /// <summary>Mémorise l'état d'origine (scale, mesh, matériaux).</summary>
        void Initialize(in FeedbackContext ctx);

        /// <summary>Remet les accumulateurs à la valeur neutre.</summary>
        void BeginFrame();

        /// <summary>Écrit le résultat combiné sur l'objet.</summary>
        void EndFrame(in FeedbackContext ctx);

        /// <summary>Rend à l'objet son état d'origine (désactivation, destruction, ResetAll).</summary>
        void Restore();
    }
}
