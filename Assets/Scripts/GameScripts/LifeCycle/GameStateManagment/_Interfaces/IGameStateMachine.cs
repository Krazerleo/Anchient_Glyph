namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.Interfaces
{
    public interface IGameStateMachine
    {
        public void EnterState<TState, TNextStateParams>(TNextStateParams parameters) where TState : IGameState;
    }
}