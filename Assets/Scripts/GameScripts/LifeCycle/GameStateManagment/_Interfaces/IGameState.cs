namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.Interfaces
{
    public interface IGameState
    {
        public void Enter<TNextStateParams>(TNextStateParams parameters);

        public void Exit();
    }
}