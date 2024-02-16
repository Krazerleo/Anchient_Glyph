using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateMachine;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates
{
    public class GamePlayState : IGameState
    {
        private IGameStateMachine _gameStateMachine;

        public void LateStateMachineBinding(IGameStateMachine stateMachine)
            => _gameStateMachine = stateMachine;

        public void Enter<TNextStateParams>(TNextStateParams parameters)
        {
        }

        public void Exit()
        {
        }
    }
}