using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.Interfaces;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateArguments;
using AncientGlyph.GameScripts.Services;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates
{
    public class BootstrapState : IGameState
    {
        private readonly IGameStateMachine _stateMachine;

        public BootstrapState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        #region Public Methods
        public void Enter<TNextStateParams>(TNextStateParams parameters)
        {
            _stateMachine.EnterState<LoadSceneState, LoadSceneArguments>(
                new LoadSceneArguments(SceneManagmentService.MainMenuSceneName, OnMenuSceneLoaded));
        }

        public void Exit()
        {
        }
        #endregion

        #region Private Methods
        private void OnMenuSceneLoaded()
        {
            _stateMachine.EnterState<MenuState, object>(null);
        }
        #endregion
    }
}