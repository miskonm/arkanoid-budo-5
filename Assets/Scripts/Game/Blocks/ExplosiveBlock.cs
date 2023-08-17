using UnityEngine;

namespace Arkanoid.Game.Blocks
{
    public class ExplosiveBlock : Block
    {
        #region Variables

        [Header(nameof(ExplosiveBlock))]
        [SerializeField] private float _explosiveRadius = 1f;
        [SerializeField] private LayerMask _blockMask;

        #endregion

        #region Unity lifecycle

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _explosiveRadius);
            
            
        }

        #endregion

        #region Protected methods

        protected override void OnDestroyedActions()
        {
            
            
            base.OnDestroyedActions();

            // TODO: Vfx
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosiveRadius, _blockMask);

            foreach (Collider2D col in colliders)
            {
                if (col.TryGetComponent(out Block block))
                {
                    block.ForceDestroy();
                }
            }
            
            Debug.Log($"");
        }

        #endregion
    }
}