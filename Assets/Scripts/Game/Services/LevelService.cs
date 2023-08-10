using System;
using Arkanoid.Utility;
using UnityEngine;

namespace Arkanoid.Game.Services
{
    public class LevelService : SingletonMonoBehaviour<LevelService>
    {
        #region Events

        public event Action OnAllBlocksDestroyed;

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            Block[] blocks = GetAllAliveBlocks();
            if (blocks.Length == 0)
            {
                Debug.Log("OnAllBlocksDestroyed");
                OnAllBlocksDestroyed?.Invoke();
            }
        }

        #endregion

        #region Private methods

        private Block[] GetAllAliveBlocks()
        {
            // TODO: Nikita Replace FindObjectsOfType with better code
            return FindObjectsOfType<Block>();
        }

        #endregion
    }
}