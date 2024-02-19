using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateArguments;
using AncientGlyph.GameScripts.Services.SceneManagmentService;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateMachine;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates
{
    public class BootstrapState : IGameState
    {
        private IGameStateMachine _stateMachine;

        public void Enter<TNextStateParams>(TNextStateParams parameters)
        {
            _stateMachine.EnterState<LoadSceneState, LoadSceneArguments>(
                new LoadSceneArguments(SceneManagmentService.MainMenuSceneName, OnMenuSceneLoaded));
        }

        public void Exit()
        {
        }

        private void OnMenuSceneLoaded()
        {
            _stateMachine.EnterState<MenuState, object>(null);
        }

        public void LateStateMachineBinding(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}