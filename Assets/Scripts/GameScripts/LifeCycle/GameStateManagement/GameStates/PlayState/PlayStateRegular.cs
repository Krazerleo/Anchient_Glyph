using AncientGlyph.GameScripts.LifeCycle.GameStateManagement.StateMachine;
using JetBrains.Annotations;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagement.GameStates.PlayState
{
    [UsedImplicitly]
    public class PlayStateRegular : IGameState
    {
        private IGameStateMachine _stateMachine;

        public void Enter<TNextStateParams>(TNextStateParams parameters)
        {
        }

        public void Exit()
        {
        }

        public void LateStateMachineBinding(IGameStateMachine stateMachine)
            => _stateMachine = stateMachine;
    }
}