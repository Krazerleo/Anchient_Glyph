using AncientGlyph.GameScripts.LifeCycle.GameStateManagement.GameStates;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagement.StateMachine
{
    public interface IGameStateMachine
    {
        public IGameState CurrentState { get; }

        public void EnterState<TState, TNextStateParams>(TNextStateParams parameters)
            where TState : IGameState;
    }
}