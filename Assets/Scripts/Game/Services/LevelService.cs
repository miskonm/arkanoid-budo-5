using System;
using System.Collections.Generic;
using Arkanoid.Game.Blocks;
using Arkanoid.Utility;
using UnityEngine;

namespace Arkanoid.Game.Services
{
    public class LevelService : SingletonMonoBehaviour<LevelService>
    {
        #region Variables

        private readonly List<Ball> _balls = new();
        private readonly List<Block> _blocks = new();

        #endregion

        #region Events

        public event Action OnAllBlocksDestroyed;

        #endregion

        #region Properties

        public List<Ball> Balls => _balls;

        #endregion

        #region Unity lifecycle

        private void OnDestroy()
        {
            Ball.OnCreated -= OnBallCreated;
            Ball.OnDestroyed -= OnBallDestroyed;

            Block.OnCreated -= OnBlockCreated;
            Block.OnDestroyed -= OnBlockDestroyed;
        }

        #endregion

        #region Protected methods

        protected override void OnAwake()
        {
            base.OnAwake();

            Ball.OnCreated += OnBallCreated;
            Ball.OnDestroyed += OnBallDestroyed;

            Block.OnCreated += OnBlockCreated;
            Block.OnDestroyed += OnBlockDestroyed;
            
            _balls.AddRange(FindObjectsOfType<Ball>());
        }

        #endregion

        #region Private methods

        private void OnBallCreated(Ball ball)
        {
            _balls.Add(ball);
        }

        private void OnBallDestroyed(Ball ball)
        {
            _balls.Remove(ball);
        }

        private void OnBlockCreated(Block block)
        {
            _blocks.Add(block);
        }

        private void OnBlockDestroyed(Block block)
        {
            _blocks.Remove(block);

            if (_blocks.Count == 0)
            {
                Debug.Log("OnAllBlocksDestroyed");
                OnAllBlocksDestroyed?.Invoke();
            }
        }

        #endregion
    }
}