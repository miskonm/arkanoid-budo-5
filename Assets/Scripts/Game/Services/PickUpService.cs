using System;
using Arkanoid.Game.PickUps;
using Arkanoid.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arkanoid.Game.Services
{
    public class PickUpService : SingletonMonoBehaviour<PickUpService>
    {
        #region Variables

        [Range(0, 100)]
        [SerializeField] private int _pickUpDropChance = 50;
        [SerializeField] private PickUpSpawnData[] _pickUps;

        #endregion

        #region Public methods

        public void CreatePickUp()
        {
            if (_pickUps == null || _pickUps.Length == 0)
            {
                return;
            }

            int chance = Random.Range(0, 101);
            if (_pickUpDropChance >= chance)
            {
                InstantiateRandomPickUp();
            }
        }

        #endregion

        #region Private methods

        private void InstantiateRandomPickUp()
        {
            int weightSum = 0;
            foreach (PickUpSpawnData spawnData in _pickUps)
            {
                weightSum += spawnData.SpawnWeight;
            }

            int randomWeight = Random.Range(0, weightSum);
            int selectedWeight = 0;

            for (int i = 0; i < _pickUps.Length; i++)
            {
                PickUpSpawnData spawnData = _pickUps[i];
                selectedWeight += spawnData.SpawnWeight;

                if (selectedWeight >= randomWeight)
                {
                    Instantiate(spawnData.PickUpPrefab, transform.position, Quaternion.identity);

                    return;
                }
            }
        }

        #endregion

        #region Local data

        [Serializable]
        private class PickUpSpawnData
        {
            #region Variables

            public PickUp PickUpPrefab;
            public int SpawnWeight;

            #endregion
        }

        #endregion
    }
}