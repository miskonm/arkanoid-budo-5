using Arkanoid.Game.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ChangeScorePickUp : PickUp
    {
        #region Variables

        [SerializeField] private int _scoreToChange = 10;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();

            GameService.Instance.ChangeScore(_scoreToChange);
        }

        #endregion
    }
}