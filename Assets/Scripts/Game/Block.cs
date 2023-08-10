using Arkanoid.Game.PickUps;
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

        [Header("Pick Up")]
        [Range(0, 100)]
        [SerializeField] private int _pickUpDropChance = 50;
        [SerializeField] private PickUp _pickUpPrefab;

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

        private void CreatePickUp()
        {
            int chance = Random.Range(0, 101);
            if (_pickUpDropChance >= chance)
            {
                Instantiate(_pickUpPrefab, transform.position, Quaternion.identity);
            }
        }

        private void PerformDestroyActions()
        {
            GameService.Instance.AddScore(_score);
            Destroy(gameObject);
            CreatePickUp();
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