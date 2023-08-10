using System;
using UnityEngine;

namespace Arkanoid
{
    public class GameService : SingletonMonoBehaviour<GameService>
    {
        #region Variables
        
        [Header("Auto Play")]
        [SerializeField] private bool _needAutoPlay;
        
        [Header("Configs")]
        [SerializeField] private int _startHp = 3;

        #endregion

        #region Events

        public event Action OnHpOver;

        #endregion

        #region Properties

        public int Hp { get; private set; }

        public int Score { get; private set; }

        public bool NeedAutoPlay => _needAutoPlay;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            Hp = _startHp;
            LevelService.Instance.OnAllBlocksDestroyed += OnAllBlocksDestroyed;
        }

        private void OnDestroy()
        {
            LevelService.Instance.OnAllBlocksDestroyed -= OnAllBlocksDestroyed;
        }

        #endregion

        #region Public methods

        public void AddScore(int value)
        {
            Score += value;
        }

        public void DecrementHp()
        {
            Hp--;

            if (Hp <= 0)
            {
                OnHpOver?.Invoke();
            }
        }

        public void ResetBall()
        {
            Ball ball = FindObjectOfType<Ball>();
            ball.ResetBall();
        }

        #endregion

        #region Private methods

        private void LoadNextLevel()
        {
            SceneLoader.Instance.LoadNextGameScene();
        }

        private void OnAllBlocksDestroyed()
        {
            LoadNextLevel();
        }

        #endregion
    }
}