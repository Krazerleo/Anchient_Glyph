using AncientGlyph.GameScripts.LifeCycle.Services;
using AncientGlyph.GameScripts.Services;
using AncientGlyph.GameScripts.Services.Interfaces;

using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
        }

        private void BindServices()
        {
            Container.Bind<IComponentLocatorService>().To<ComponentLocatorService>().AsSingle();
            Container.Bind<ISceneManagmentService>().To<SceneManagmentService>().AsSingle();
        }
    }
}