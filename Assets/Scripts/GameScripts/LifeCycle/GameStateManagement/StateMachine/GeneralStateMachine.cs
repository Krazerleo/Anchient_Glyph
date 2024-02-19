using System.Collections.Generic;
using System.Linq;

using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateMachine
{
    public class GeneralStateMachine : IGameStateMachine
    {
        private readonly IDictionary<System.Type, IGameState> _states = new Dictionary<System.Type, IGameState>();

        public IGameState CurrentState { get; private set; }

        public GeneralStateMachine(IGameState[] states)
        {
            foreach (var state in states)
            {
                state.LateStateMachineBinding(this);
            }

            _states = states
                .ToDictionary(entry => entry.GetType(), entry => entry);
        }

        public void EnterState<TState, TNextStateParams>(TNextStateParams parameters) where TState : IGameState
        {
            CurrentState?.Exit();
            CurrentState = _states[typeof(TState)];
            CurrentState.Enter(parameters);
        }
    }
}