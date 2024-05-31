using AncientGlyph.GameScripts.EntityModel.Factory;
using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection.FactoryInstaller
{
    public class CreatureFactoryInstaller : MonoInstaller<CreatureFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ICreatureFactory>()
                .To<CreatureFactory>()
                .AsSingle();
            
            Container.Bind<IPlayerFactory>()
                .To<PlayerFactory>()
                .AsSingle();
        }
    }
}