using AncientGlyph.GameScripts.LifeCycle.GameStateManagement.GameStates;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateMachine;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates
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