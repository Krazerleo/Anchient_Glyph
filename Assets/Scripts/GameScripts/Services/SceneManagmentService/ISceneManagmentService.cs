using System;

namespace AncientGlyph.GameScripts.Services.SceneManagmentService
{
    public interface ISceneManagmentService
    {
        public static string MainMenuSceneName { get; }

        public void LoadScene(string sceneName, Action onLoadCallback);
    }
}