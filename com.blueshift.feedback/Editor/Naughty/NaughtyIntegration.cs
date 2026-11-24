#if BLUESHIFT_NAUGHTY
namespace BlueShift.Feedback.Editor
{
    /// <summary>
    /// Code compilé seulement si NaughtyAttributes est installé (define BLUESHIFT_NAUGHTY,
    /// à déclarer dans les versionDefines de BlueShift.Feedback.Editor.asmdef).
    /// </summary>
    internal static class NaughtyIntegration
    {
        // TODO [T18] Intégrations optionnelles (ShowIf, MinMaxSlider, CurveRange…) sur les champs simples.
    }
}
#endif
