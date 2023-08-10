using Arkanoid.Game.Services;
using UnityEngine;

namespace Arkanoid.Ui
{
    public class GameOverScreen : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject _contentObject;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            _contentObject.SetActive(false);
        }

        private void Start()
        {
            GameService.Instance.OnHpOver += OnHpOver;
        }

        private void OnDestroy()
        {
            GameService.Instance.OnHpOver -= OnHpOver;
        }

        #endregion

        #region Private methods

        private void OnHpOver()
        {
            _contentObject.SetActive(true);
        }

        #endregion
    }
}