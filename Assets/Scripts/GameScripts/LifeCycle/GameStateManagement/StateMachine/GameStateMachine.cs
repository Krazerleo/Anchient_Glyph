using AncientGlyph.GameScripts.LifeCycle.GameStateManagement.GameStates;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagement.StateMachine
{
    public class GameStateMachine : GeneralStateMachine
    {
        public GameStateMachine(IGameState[] states) : base(states) { }
    }
}