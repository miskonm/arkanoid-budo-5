using Arkanoid.Game.Services;
using TMPro;
using UnityEngine;

namespace Arkanoid.Ui
{
    public class GameScreen : MonoBehaviour
    {
        #region Variables

        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private TMP_Text _hpLabel;

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            UpdateScore();
        }

        #endregion

        #region Private methods

        private void UpdateScore()
        {
            _scoreLabel.text = $"Score: {GameService.Instance.Score}";
            _hpLabel.text = $"Hp: {GameService.Instance.Hp}";
        }

        #endregion
    }
}