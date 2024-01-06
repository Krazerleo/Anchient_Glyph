using AncientGlyph.GameScripts.Helpers;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.Interfaces;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateArguments;
using AncientGlyph.GameScripts.Services.Interfaces;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates
{
    public class LoadSceneState : IGameState
    {
        private IGameStateMachine _stateMachine;
        private ISceneManagmentService _sceneManagmentService;

        public LoadSceneState(IGameStateMachine stateMachine, ISceneManagmentService sceneManagmentService)
        {
            _stateMachine = stateMachine;
            _sceneManagmentService = sceneManagmentService;
        }

        #region Public Methods
        public void Enter<TNextStateParams>(TNextStateParams parameters)
        {
            var loadingParameters = parameters as LoadSceneArguments;

            if (loadingParameters == null)
            {
                LogTools.LogWarning(this, "wrong scene loading params");
            }

            _sceneManagmentService.LoadScene(loadingParameters.SceneName, loadingParameters.OnLoadAction);
        }

        public void Exit()
        {
        }
        #endregion
    }
}