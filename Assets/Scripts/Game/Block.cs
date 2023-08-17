using Arkanoid.Game.Services;
using Arkanoid.Utility;
using UnityEngine;

namespace Arkanoid.Game
{
    public class Block : MonoBehaviour
    {
        #region Variables

        [Header("Components")]
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Header("Base Settings")]
        [SerializeField] private int _score;
        [SerializeField] private int _hp = 1;
        [SerializeField] private bool _isInvisible;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            if (_isInvisible)
            {
                _spriteRenderer.SetAlpha(0);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            ApplyHit();
        }

        #endregion

        #region Private methods

        private void ApplyHit()
        {
            if (TryUpdateInvisibility())
            {
                return;
            }

            _hp--;

            if (_hp > 0)
            {
                return;
            }

            PerformDestroyActions();
        }

        private void PerformDestroyActions()
        {
            GameService.Instance.ChangeScore(_score);
            Destroy(gameObject);
            PickUpService.Instance.CreatePickUp();
        }

        private bool TryUpdateInvisibility()
        {
            if (_isInvisible)
            {
                _isInvisible = false;
                _spriteRenderer.SetAlpha(1);
                return true;
            }

            return false;
        }

        #endregion
    }
}