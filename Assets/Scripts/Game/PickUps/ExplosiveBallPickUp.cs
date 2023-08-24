using Arkanoid.Game.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ExplosiveBallPickUp : PickUp
    {
        #region Variables

        [SerializeField] private float _radius;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();

            foreach (Ball ball in LevelService.Instance.Balls)
            {
                ball.GetEffect<ExplosiveBallEffect>().Activate(_radius);
            }
        }

        #endregion
    }
}