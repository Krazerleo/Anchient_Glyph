using AncientGlyph.GameScripts.LifeCycle.GameStateManagement.GameStates;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateMachine
{
    public class PlayStateMachine : GeneralStateMachine
    {
        public PlayStateMachine(IGameState[] states) : base(states) { }
    }
}