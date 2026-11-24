namespace BlueShift.Feedback
{
    /// <summary>
    /// Regroupe les appliers d'un FeedbackController. Les sorties y déposent leurs contributions.
    /// Un applier est null tant qu'aucune sortie n'en a besoin.
    /// </summary>
    public class FeedbackAppliers
    {
        public TransformApplier Transform;
        public MeshApplier Mesh;
        public MaterialApplier Material;

        public void BeginFrame()
        {
            Transform?.BeginFrame();
            Mesh?.BeginFrame();
            Material?.BeginFrame();
        }

        public void EndFrame(in FeedbackContext ctx)
        {
            Transform?.EndFrame(ctx);
            Mesh?.EndFrame(ctx);
            Material?.EndFrame(ctx);
        }

        public void Restore()
        {
            Transform?.Restore();
            Mesh?.Restore();
            Material?.Restore();
        }
    }
}
