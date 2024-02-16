using AncientGlyph.GameScripts.Interactors.Entities.Factories;

using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection
{
    public class CreatureFactoryInstaller : MonoInstaller<CreatureFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ICreatureFactory>().To<CreatureFactory>().AsSingle();
        }
    }
}