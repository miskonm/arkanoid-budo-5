using UnityEngine;

namespace Arkanoid
{
    public class BottomWall : MonoBehaviour
    {
        #region Unity lifecycle

        private void OnCollisionEnter2D(Collision2D other)
        {
            GameService.Instance.DecrementHp();
            GameService.Instance.ResetBall();
        }

        #endregion
    }
}