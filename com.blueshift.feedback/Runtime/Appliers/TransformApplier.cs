// Squelette : champs privés pas encore utilisés -> avertissements masqués. Retirer la ligne une fois implémenté.
#pragma warning disable CS0169, CS0414, CS0649

using UnityEngine;

namespace BlueShift.Feedback
{
    /// <summary>
    /// Combine les contributions des TransformOutput et écrit localScale / localPosition du Visual.
    /// Scales multipliés, offsets additionnés.
    /// </summary>
    public class TransformApplier : IApplier
    {
        Transform _visual;
        Vector3 _baseScale;
        Vector3 _basePosition;
        Quaternion _baseRotation;

        Vector3 _scale = Vector3.one;
        Vector3 _offset;
        Matrix4x4 _deformation = Matrix4x4.identity;

        public void Initialize(in FeedbackContext ctx)
        {
            if (ctx.Visual==null)
            {
                return;
            }
            _visual = ctx.Visual;
            _basePosition = ctx.Visual.localPosition;
            _baseRotation = ctx.Visual.localRotation;
            _baseScale = ctx.Visual.localScale;
        }

        public void BeginFrame()
        {
            _scale = Vector3.one;
            _offset = Vector3.zero;
            _deformation = Matrix4x4.identity;
        }

        /// <summary>Multiplie l'échelle (axes locaux du Visual).</summary>
        public void AddScale(Vector3 multiplier)
        {
            _scale = Vector3.Scale(_scale, multiplier);
        }

        /// <summary>Ajoute une déformation orientée (AxisMode.Velocity).</summary>
        public void AddDeformation(Matrix4x4 deformation)
        {
            // TODO [T3] _deformation = deformation * _deformation.
            // NB : un transform ne sait pas étirer en biais : on oriente le Visual vers la direction
            // puis on étire sur Z. Ce cas sera vraiment exact en mode mesh (T13).
        }

        /// <summary>Ajoute un décalage de position (tremblement).</summary>
        public void AddOffset(Vector3 offset)
        {
            _offset += offset;
        }

        public void EndFrame(in FeedbackContext ctx)
        {
            // TODO [T3] Appliquer _deformation (AxisMode.Velocity) : orienter le Visual vers la direction puis étirer sur Z.
            if (_visual == null)
            {
                return;
            }
            _visual.localScale = Vector3.Scale(_baseScale, _scale);
            _visual.localPosition = _basePosition + _offset;
            _visual.localRotation = _baseRotation;
        }

        public void Restore()
        {
            if (_visual == null)
            {
                return;
            }
            _visual.localPosition = _basePosition;
            _visual.localRotation = _baseRotation;
            _visual.localScale = _baseScale;
        }
    }
}
