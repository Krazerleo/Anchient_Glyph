using System;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagement.StateArguments
{
    public class LoadSceneArguments
    {
        public readonly Action OnLoadAction;
        public readonly string SceneName;

        public LoadSceneArguments(string sceneName, Action onLoadAction)
        {
            OnLoadAction = onLoadAction;
            SceneName = sceneName;
        }
    }
}