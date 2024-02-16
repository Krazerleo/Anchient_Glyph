using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateArguments;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateMachine;
using AncientGlyph.GameScripts.Services.LoggingService;
using AncientGlyph.GameScripts.Services.SceneManagmentService;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates
{
    public class LoadSceneState : IGameState
    {
        private IGameStateMachine _stateMachine;
        private readonly ISceneManagmentService _sceneManagmentService;
        private readonly ILoggingService _loggingService;

        public LoadSceneState(ISceneManagmentService sceneManagmentService,
            ILoggingService loggingService)
        {
            _sceneManagmentService = sceneManagmentService;
            _loggingService = loggingService;
        }

        public void Enter<TNextStateParams>(TNextStateParams parameters)
        {
            var loadingParameters = parameters as LoadSceneArguments;

            if (loadingParameters == null)
            {
                _loggingService.LogError("Wrong scene loading params");
            }

            _sceneManagmentService.LoadScene(loadingParameters.SceneName, loadingParameters.OnLoadAction);
        }

        public void Exit() { }

        public void LateStateMachineBinding(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}