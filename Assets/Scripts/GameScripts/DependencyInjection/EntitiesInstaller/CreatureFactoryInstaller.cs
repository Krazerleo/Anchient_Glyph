using AncientGlyph.GameScripts.Interactors.Entities.Factory;
using AncientGlyph.GameScripts.Interactors.Entities.Factory._Interfaces;
using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection.EntitiesInstaller
{
    public class CreatureFactoryInstaller : MonoInstaller<CreatureFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ICreatureFactory>().To<CreatureFactory>().AsSingle();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
        }
    }
}