using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateMachine
{
    public interface IGameStateMachine
    {
        public IGameState CurrentState { get; }

        public void EnterState<TState, TNextStateParams>(TNextStateParams parameters)
            where TState : IGameState;
    }
}