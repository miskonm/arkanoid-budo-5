using Arkanoid.Game.Services;
using UnityEngine;

namespace Arkanoid.Game
{
    [RequireComponent(typeof(Collider2D))]
    public class BottomWall : MonoBehaviour
    {
        #region Unity lifecycle

        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(Tags.Ball))
            {
                GameService.Instance.DecrementHp();
                GameService.Instance.ResetBall();   
            }
            else
            {
                Destroy(other.gameObject);
            }
        }

        #endregion
    }
}