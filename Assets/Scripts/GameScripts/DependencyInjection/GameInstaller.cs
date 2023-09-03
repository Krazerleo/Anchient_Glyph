using AncientGlyph.GameScripts.Core.Services;
using AncientGlyph.GameScripts.Core.Services.Interfaces;

using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IComponentLocatorService>().To<ComponentLocatorService>().AsSingle();
        }
    }
}