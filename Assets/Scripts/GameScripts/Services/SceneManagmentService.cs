using System;

using AncientGlyph.GameScripts.Services.Interfaces;
using AncientGlyph.GameScripts.Helpers;

using UnityEngine.SceneManagement;

using Cysharp.Threading.Tasks;

namespace AncientGlyph.GameScripts.Services
{
    public class SceneManagmentService : ISceneManagmentService
    {
        public static string MainMenuSceneName => "MainMenuScene";

        public event Action<float> OnLoadingProgressUpdated;

        public SceneManagmentService()
        {
        }

        public void LoadScene(string sceneName, Action onLoadCallback)
        {
            InternalLoadScene(sceneName, onLoadCallback).Forget();
        }

        #region Public Methods
        private async UniTaskVoid InternalLoadScene(string sceneName, Action onLoadCallback)
        {
            LogTools.LogTodo(this, "make normal exception handling");

            if (sceneName == SceneManager.GetActiveScene().name)
            {
                return;
            }

            var loadingProgress = Progress.CreateOnlyValueChanged<float>(progress => OnLoadingProgressUpdated?.Invoke(progress));

            try
            {
                await SceneManager.LoadSceneAsync(sceneName).ToUniTask(loadingProgress);
                onLoadCallback();
            }
            catch
            {
                throw new Exception("scene not loaded");
            }
        }
        #endregion
    }
}