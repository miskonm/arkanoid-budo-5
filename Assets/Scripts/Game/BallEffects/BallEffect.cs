using UnityEngine;

namespace Arkanoid.Game
{
    public abstract class BallEffect : MonoBehaviour
    {
        #region Properties

        protected Ball Owner { get; private set; }

        #endregion

        #region Unity lifecycle

        public void Reset()
        {
            OnReset();
        }

        #endregion

        #region Public methods

        public abstract void Copy(BallEffect originalBallEffect);

        public void PerformCollision(Collision2D other)
        {
            OnPerformCollision(other);
        }

        public void SetOwner(Ball ball)
        {
            Owner = ball;
        }

        #endregion

        #region Protected methods

        protected virtual void OnPerformCollision(Collision2D other) { }

        protected virtual void OnReset() { }

        #endregion
    }
}