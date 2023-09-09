using System;

using Cysharp.Threading.Tasks;

namespace AncientGlyph.GameScripts.Services.Interfaces
{
    public interface ISceneManagmentService
    {
        public static string MainMenuSceneName { get; }

        public void LoadScene(string sceneName, Action onLoadCallback);
    }
}