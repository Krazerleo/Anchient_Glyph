using AncientGlyph.GameScripts.LifeCycle.GameStateManagement.StateMachine;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagement.GameStates
{
    public interface IGameState
    {
        public void LateStateMachineBinding(IGameStateMachine stateMachine);

        public void Enter<TNextStateParams>(TNextStateParams parameters);

        public void Exit();
    }
}