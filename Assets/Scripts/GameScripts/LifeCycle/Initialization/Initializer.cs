using AncientGlyph.GameScripts.LifeCycle.GameStateManagment;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates;
using AncientGlyph.GameScripts.Services.Interfaces;

using UnityEngine;

using Zenject;

namespace AncientGlyph.GameScripts.Services
{
    public class Initializer : MonoBehaviour
    {
        private IComponentLocatorService _componentLocator;
        private ISceneManagmentService _sceneManagmentService;

        #region UnityMessages
        private void Awake()
        {
            Initialize();
        }
        #endregion

        #region Public Methods
        [Inject]
        public void Construct(IComponentLocatorService componentLocator, ISceneManagmentService sceneManagmentService)
        {
            _componentLocator = componentLocator;
            _sceneManagmentService = sceneManagmentService;
        }
        #endregion

        #region Private Methods
        private void Initialize()
        {
            if (_componentLocator.IsComponentExist(this))
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            var stateMachine = new GameStateMachine(_sceneManagmentService);
            stateMachine.EnterState<BootstrapState, object>(null);
        }
        #endregion
    }
}