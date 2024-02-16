using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateMachine;
using AncientGlyph.GameScripts.Services.ComponentLocatorService;
using AncientGlyph.GameScripts.Services.AssetProviderService;
using AncientGlyph.GameScripts.Services.LoggingService;
using AncientGlyph.GameScripts.Services.SceneManagmentService;

using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
            BindAssetProvides();
            BindGameStateMachine();
        }

        private void BindServices()
        {
            Container.Bind<IComponentLocatorService>()
                .To<ComponentLocatorService>()
                .AsSingle();

            Container.Bind<ISceneManagmentService>()
                .To<SceneManagmentService>()
                .AsSingle();

            Container.Bind<ILoggingService>()
                .To<DebugLoggingService>()
                .AsSingle();
        }

        private void BindAssetProvides()
        {
            Container.Bind(typeof(IAssetProviderService<>))
                .To(typeof(AssetProviderService<>))
                .AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<GameStateMachine>()
                .FromSubContainerResolve()
                .ByMethod(subContainer =>
                {
                    subContainer.Bind<GameStateMachine>().AsSingle();

                    subContainer.BindInterfacesAndSelfTo<BootstrapState>()
                        .AsSingle();

                    subContainer.BindInterfacesAndSelfTo<MenuState>()
                        .AsSingle();

                    subContainer.BindInterfacesAndSelfTo<LoadSceneState>()
                        .AsSingle();
                })
                .AsSingle();
        }
    }
}