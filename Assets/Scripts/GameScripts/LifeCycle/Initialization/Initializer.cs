using AncientGlyph.GameScripts.Core.GameStateManagment;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates;
using AncientGlyph.GameScripts.Services.Interfaces;

using UnityEngine;

using Zenject;

namespace AncientGlyph.GameScripts.Services
{
    public class Initializer : MonoBehaviour
    {
        private IComponentLocatorService _componentLocator;

        #region UnityMessages
        private void Awake()
        {
            Initialize();
        }
        #endregion

        #region Public Methods
        [Inject]
        public void Construct(IComponentLocatorService componentLocator)
        {
            _componentLocator = componentLocator;
        }
        #endregion

        #region Private Methods
        private void Initialize()
        {
            if (Exist())
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            var stateMachine = new GameStateMachine();
            stateMachine.EnterState<BootstrapState, object>(null);
        }

        private bool Exist()
        {
            var currentInitializer = _componentLocator.FindComponent<Initializer>();
            return currentInitializer != null && currentInitializer != this;
        }
        #endregion
    }
}