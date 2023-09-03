using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.Interfaces;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateArguments
{
    public class LoadSceneArguments
    {
        public readonly IGameState NextGameState;
        public readonly string SceneName;

        public LoadSceneArguments(IGameState nextGameState, string sceneName)
        {
            NextGameState = nextGameState;
            SceneName = sceneName;
        }
    }
}