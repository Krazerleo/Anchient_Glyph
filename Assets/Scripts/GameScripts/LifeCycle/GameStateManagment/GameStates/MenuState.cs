using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.Interfaces;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates
{
    public class MenuState : IGameState
    {
        private readonly IGameStateMachine _stateMachine;

        public MenuState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        #region Public Methods
        public void Enter<TNextStateParams>(TNextStateParams parameters)
        {
        }

        public void Exit()
        {
        }
        #endregion
    }
}