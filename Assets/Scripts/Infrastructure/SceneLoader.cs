using Arkanoid.Utility;
using UnityEngine.SceneManagement;

namespace Arkanoid.Infrastructure
{
    public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
    {
        #region Public methods

        public void LoadNextGameScene()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }

        #endregion
    }
}