// Squelette : champs privés pas encore utilisés -> avertissements masqués. Retirer la ligne une fois implémenté.
#pragma warning disable CS0169, CS0414, CS0649

using System.Collections.Generic;
using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Déforme les sommets d'une copie du mesh du Visual sur le CPU (phase 5).
    /// Le mesh d'origine n'est jamais modifié.
    /// </summary>
    public class MeshApplier : IApplier
    {
        MeshFilter _filter;
        Mesh _originalMesh;
        Mesh _workingMesh;
        Vector3[] _originalVertices;
        readonly List<Vector3> _vertices = new List<Vector3>();
        readonly List<MeshOutput> _outputs = new List<MeshOutput>();
        Matrix4x4 _deformation = Matrix4x4.identity;

        public void Initialize(in FeedbackContext ctx)
        {
            // TODO [T12] Récupérer le MeshFilter du Visual ; erreur claire si le mesh n'est pas Read/Write.
            // TODO [T12] Copier le mesh (Object.Instantiate), MarkDynamic(), l'assigner au MeshFilter.
            // TODO [T12] Mettre en cache les sommets d'origine.
        }

        public void BeginFrame()
        {
            _outputs.Clear();
            _deformation = Matrix4x4.identity;
        }

        /// <summary>Enregistre une sortie mesh pour la frame.</summary>
        public void Register(MeshOutput output)
        {
            _outputs.Add(output);
        }

        /// <summary>Ajoute une déformation globale (sorties transform en mode mesh).</summary>
        public void AddDeformation(Matrix4x4 deformation)
        {
            // TODO [T13] _deformation = deformation * _deformation.
        }

        public void EndFrame(in FeedbackContext ctx)
        {
            // TODO [T12] Si aucune contribution : ne rien réécrire (éviter le coût par frame).
            // TODO [T12] Pour chaque sommet : _deformation, puis chaque MeshOutput.Displace.
            // TODO [T12] SetVertices sans allocation, RecalculateNormals, RecalculateBounds.
        }

        public void Restore()
        {
            // TODO [T12] Remettre le mesh d'origine sur le MeshFilter et détruire la copie.
        }
    }
}
