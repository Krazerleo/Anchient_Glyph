using AncientGlyph.GameScripts.LifeCycle.GameStateManagement.GameStates;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagement.StateMachine
{
    public class PlayStateMachine : GeneralStateMachine
    {
        public PlayStateMachine(IGameState[] states) : base(states) { }
    }
}