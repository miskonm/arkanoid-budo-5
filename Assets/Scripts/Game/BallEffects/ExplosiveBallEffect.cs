using Arkanoid.Game.Blocks;
using UnityEngine;

namespace Arkanoid.Game
{
    public class ExplosiveBallEffect : BallEffect
    {
        #region Variables

        [SerializeField] private LayerMask _blockMask;
        [SerializeField] private GameObject _vfxPrefab;

        private float _explosiveRadius;
        private bool _isExplosive;

        #endregion

        #region Unity lifecycle

        protected override void OnPerformCollision(Collision2D other)
        {
            base.OnPerformCollision(other);
            
            if (_isExplosive)
            {
                ExplodeBlocks(other);
            }
        }

        #endregion

        #region Public methods

        public void Activate(float radius)
        {
            _isExplosive = true;
            _explosiveRadius = radius;

            Owner.SpriteRenderer.color = Color.blue;
        }

        public override void Copy(BallEffect originalBallEffect)
        {
            ExplosiveBallEffect origin = (ExplosiveBallEffect)originalBallEffect;
            _explosiveRadius = origin._explosiveRadius;
            _isExplosive = origin._isExplosive;
        }

        #endregion

        #region Protected methods

        protected override void OnReset()
        {
            base.OnReset();

            _isExplosive = false;

            // TODO: Change color. Think
        }

        #endregion

        #region Private methods

        private void ExplodeBlocks(Collision2D other)
        {
            InstantiateVfx();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosiveRadius, _blockMask);

            foreach (Collider2D col in colliders)
            {
                if (col.TryGetComponent(out Block block))
                {
                    block.ForceDestroy();
                }
            }
        }

        private void InstantiateVfx()
        {
            if (_vfxPrefab == null)
            {
                return;
            }

            Instantiate(_vfxPrefab, transform.position, Quaternion.identity);
        }

        #endregion
    }
}