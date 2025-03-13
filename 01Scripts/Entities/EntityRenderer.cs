using LJS.Animators;
using UnityEngine;

namespace LJS.Entities
{
    public class EntityRenderer : AnimateRenderer, IEntityComponent
    {
        protected Entity _entity;

        [field: SerializeField] public float FacingDirection { get; private set; } = 1;
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        #region FlipController

        public void Flip()
        {
            FacingDirection *= -1;
            _entity.transform.Rotate(0, 180f, 0);
        }

        public void FlipController(float normalizeXMove)
        {
            if (Mathf.Abs(FacingDirection + normalizeXMove) < 0.5f)
                Flip();
        }

        #endregion
    }
}
