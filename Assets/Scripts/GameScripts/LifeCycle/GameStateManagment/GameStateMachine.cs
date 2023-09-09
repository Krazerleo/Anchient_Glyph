using System.Collections.Generic;

using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.Interfaces;
using AncientGlyph.GameScripts.Services.Interfaces;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly IDictionary<System.Type, IGameState> _states = new Dictionary<System.Type, IGameState>();

        public IGameState CurrentState { get; private set; }

        public GameStateMachine(ISceneManagmentService sceneManagmentService)
        {
            _states[typeof(BootstrapState)] = new BootstrapState(this);
            _states[typeof(LoadSceneState)] = new LoadSceneState(this, sceneManagmentService);
            _states[typeof(MenuState)] = new MenuState(this);
            _states[typeof(PlayState)] = new PlayState(this);
        }

        #region Public Methods
        public void EnterState<TState, TNextStateParams>(TNextStateParams parameters) where TState : IGameState
        {
            CurrentState?.Exit();
            CurrentState = _states[typeof(TState)];
            CurrentState.Enter(parameters);
        }
        #endregion
    }
}