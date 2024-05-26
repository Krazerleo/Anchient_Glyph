using AncientGlyph.GameScripts.LifeCycle.GameStateManagement.StateMachine;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagement.GameStates
{
    public class MenuState : IGameState
    {
        private IGameStateMachine _stateMachine;

        public void Enter<TNextStateParams>(TNextStateParams parameters) { }

        public void Exit() { }

        public void LateStateMachineBinding(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}