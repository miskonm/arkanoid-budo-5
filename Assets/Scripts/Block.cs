using UnityEngine;

namespace Arkanoid
{
    public class Block : MonoBehaviour
    {
        #region Variables

        [SerializeField] private int _score;

        #endregion

        #region Unity lifecycle

        private void OnCollisionEnter2D(Collision2D other)
        {
            ApplyHit();
        }

        #endregion

        #region Private methods

        private void ApplyHit()
        {
            GameService.Instance.AddScore(_score);
            
            Destroy(gameObject);
        }

        #endregion
    }
}