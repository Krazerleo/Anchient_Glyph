using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.Interfaces;

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
        public void Enter()
        {
        }

        public void Exit()
        {
            _stateMachine.EnterState<MenuState, object>(null);
        }
        #endregion
    }
}