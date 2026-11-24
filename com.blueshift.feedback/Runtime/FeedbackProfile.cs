// Squelette : champs privés pas encore utilisés -> avertissements masqués. Retirer la ligne une fois implémenté.
#pragma warning disable CS0169, CS0414, CS0649

using System.Collections.Generic;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Preset réutilisable : un jeu de signaux et de sorties à appliquer à plusieurs objets.
    /// </summary>
    [CreateAssetMenu(fileName = "FeedbackProfile", menuName = "BlueShift/Feedback Profile")]
    public class FeedbackProfile : ScriptableObject
    {
        [SerializeField] List<Signal> signals = new List<Signal>();
        [SerializeReference] List<FeedbackOutput> outputs = new List<FeedbackOutput>();

        public IReadOnlyList<Signal> Signals => signals;
        public IReadOnlyList<FeedbackOutput> Outputs => outputs;

        /// <summary>Copie profonde des signaux, pour que chaque objet ait son propre état.</summary>
        public List<Signal> CloneSignals()
        {
            // TODO [T17] Copie profonde (JsonUtility ou copie champ par champ ; attention aux [SerializeReference]).
            return new List<Signal>();
        }

        /// <summary>Copie profonde des sorties.</summary>
        public List<FeedbackOutput> CloneOutputs()
        {
            // TODO [T17]
            return new List<FeedbackOutput>();
        }
    }
}
