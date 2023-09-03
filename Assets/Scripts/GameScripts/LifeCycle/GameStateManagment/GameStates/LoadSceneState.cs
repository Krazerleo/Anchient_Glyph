using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.Interfaces;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates
{
    public class LoadSceneState : IGameState
    {
        private IGameStateMachine _stateMachine;

        public LoadSceneState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        #region Public Methods
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}