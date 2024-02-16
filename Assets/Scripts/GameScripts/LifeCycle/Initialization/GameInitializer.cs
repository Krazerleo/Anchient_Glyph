using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates;
using AncientGlyph.GameScripts.Services.ComponentLocatorService;

using Cysharp.Threading.Tasks;
using Zenject;

using UnityEngine;
using UnityEngine.AddressableAssets;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateMachine;


namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagment.Initialization
{
    public class GameInitializer : MonoBehaviour
    {
        private IComponentLocatorService _componentLocator;
        private IGameStateMachine _gameStateMachine;

        private void Awake()
        {
            Initialize();
        }

        [Inject]
        public void Construct(IComponentLocatorService componentLocator,
            GameStateMachine gameStateMachine)
        {
            _componentLocator = componentLocator;
            _gameStateMachine = gameStateMachine;
        }

        private void Initialize()
        {
            if (_componentLocator.IsComponentExist(this))
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            Addressables.InitializeAsync().ToUniTask().GetAwaiter().GetResult();

            _gameStateMachine.EnterState<BootstrapState, object>(null);
        }
    }
}