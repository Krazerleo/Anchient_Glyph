using System;

using UnityEngine.SceneManagement;

using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace AncientGlyph.GameScripts.Services.SceneManagmentService
{
    public class SceneManagmentService : ISceneManagmentService
    {
        public static string MainMenuSceneName => "MainMenuScene";

        public event Action<float> OnLoadingProgressUpdated;

        public SceneManagmentService() { }

        public void LoadScene(string sceneName, Action onLoadCallback)
        {
            InternalLoadScene(sceneName, onLoadCallback).Forget();
        }

        private async UniTaskVoid InternalLoadScene(string sceneName, Action onLoadCallback)
        {
            if (sceneName == SceneManager.GetActiveScene().name)
            {
                return;
            }

            var loadingProgress = Progress.CreateOnlyValueChanged<float>(progress => OnLoadingProgressUpdated?.Invoke(progress));

            try
            {
                var handle = await Addressables
                    .LoadSceneAsync(MainMenuSceneName, LoadSceneMode.Additive)
                    .ToUniTask(loadingProgress);

                await handle.ActivateAsync();
                onLoadCallback();
            }
            catch
            {
                throw new Exception("Scene cannot be loaded");
            }
        }
    }
}