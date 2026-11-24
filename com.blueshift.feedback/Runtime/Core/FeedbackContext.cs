using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Données de la frame transmises aux signaux, aux sorties et aux appliers.
    /// Construit une fois par frame (ou par pas physique) par le FeedbackController.
    /// </summary>
    public readonly struct FeedbackContext
    {
        /// <summary>Temps écoulé (scaled), comme Time.time.</summary>
        public readonly float Time;
        /// <summary>Temps écoulé non affecté par Time.timeScale.</summary>
        public readonly float UnscaledTime;
        /// <summary>Durée de la frame ou du pas physique en cours.</summary>
        public readonly float DeltaTime;
        /// <summary>Objet racine (celui qui porte le FeedbackController, le Rigidbody, le collider).</summary>
        public readonly Transform Root;
        /// <summary>Enfant visuel déformé / coloré. Jamais la racine.</summary>
        public readonly Transform Visual;
        /// <summary>Rigidbody de la racine, ou null pour un objet sans physique.</summary>
        public readonly Rigidbody Body;

        public FeedbackContext(float time, float unscaledTime, float deltaTime, Transform root, Transform visual, Rigidbody body)
        {
            Time = time;
            UnscaledTime = unscaledTime;
            DeltaTime = deltaTime;
            Root = root;
            Visual = visual;
            Body = body;
        }
    }
}
