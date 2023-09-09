using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.Interfaces;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates
{
    public class PlayState : IGameState
    {
        private IGameStateMachine _stateMachine;

        public PlayState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter<TNextStateParams>(TNextStateParams parameters)
        {
        }

        public void Exit()
        {
        }
    }
}