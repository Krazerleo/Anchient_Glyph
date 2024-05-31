using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection.FactoryInstaller
{
    public class ItemFactoryInstaller : MonoInstaller<ItemFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IItemFactory>()
                .To<ItemFactory>()
                .AsSingle();
        }
    }
}