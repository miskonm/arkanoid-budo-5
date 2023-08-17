using Arkanoid.Game.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class CloneBallPickUp : PickUp
    {
        #region Variables

        [SerializeField] private int _clonesCount = 2;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();

            int ballsCount = LevelService.Instance.Balls.Count;
            for (int i = 0; i < ballsCount; i++)
            {
                Ball ball = LevelService.Instance.Balls[i];
                for (int j = 0; j < _clonesCount; j++)
                {
                    Ball clone = ball.Clone();
                    clone.RandomizeDirection();
                }
            }
            
            
            
        }

        #endregion
    }
}