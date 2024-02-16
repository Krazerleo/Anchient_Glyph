using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateMachine;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates
{
    public interface IGameState
    {
        public void LateStateMachineBinding(IGameStateMachine stateMachine);

        public void Enter<TNextStateParams>(TNextStateParams parameters);

        public void Exit();
    }
}