using UnityEngine;

namespace Arkanoid
{
    public class Ball : MonoBehaviour
    {
        #region Variables

        public Rigidbody2D Rb;
        public Vector2 StartDirection;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            Rb.velocity = StartDirection;
        }

        #endregion
    }
}