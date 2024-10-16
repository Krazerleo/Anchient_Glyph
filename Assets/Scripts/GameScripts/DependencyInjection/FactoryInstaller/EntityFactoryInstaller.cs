using AncientGlyph.GameScripts.EntityModel.Factory;
using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection.FactoryInstaller
{
    public class EntityFactoryInstaller : MonoInstaller<EntityFactoryInstaller>
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