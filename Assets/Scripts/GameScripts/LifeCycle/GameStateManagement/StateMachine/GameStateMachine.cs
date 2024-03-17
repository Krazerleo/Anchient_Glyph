using AncientGlyph.GameScripts.LifeCycle.GameStateManagement.GameStates;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateMachine
{
    public class GameStateMachine : GeneralStateMachine
    {
        public GameStateMachine(IGameState[] states) : base(states) { }
    }
}