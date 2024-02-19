using AncientGlyph.GameScripts.Interactors.Entities.Factories;
using AncientGlyph.GameScripts.Interactors.EntityModelElements.Entities.Factory;
using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection.CreaturesInstaller
{
    public class CreatureFactoryInstaller : MonoInstaller<CreatureFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ICreatureFactory>().To<CreatureFactory>().AsSingle();
        }
    }
}