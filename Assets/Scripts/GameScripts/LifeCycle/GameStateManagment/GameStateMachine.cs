using System.Collections.Generic;

using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.Interfaces;

namespace AncientGlyph.GameScripts.Core.GameStateManagment
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly IDictionary<System.Type, IGameState> _states = new Dictionary<System.Type, IGameState>();

        public IGameState CurrentState { get; private set; }

        public GameStateMachine()
        {
            _states[typeof(BootstrapState)] = new BootstrapState(this);
            _states[typeof(LoadSceneState)] = new LoadSceneState(this);
            _states[typeof(MenuState)] = new MenuState(this);
            _states[typeof(PlayState)] = new PlayState(this);
        }

        public void EnterState<TState, TNextStateParams>(TNextStateParams parameters) where TState : IGameState
        {
            CurrentState?.Exit();
            CurrentState = _states[typeof(TState)];
            CurrentState.Enter();
        }
    }
}