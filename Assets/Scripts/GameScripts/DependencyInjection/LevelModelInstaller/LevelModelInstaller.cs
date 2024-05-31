using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Serialization;
using Zenject;

namespace AncientGlyph.GameScripts.DependencyInjection.LevelModelInstaller
{
    public class LevelModelInstaller : MonoInstaller<LevelModelInstaller>
    {
        public override void InstallBindings()
        {
            string levelModelPath = LevelPathProvider.GetPathFromRuntime();
            LevelDeserializer levelDeserializer = new(levelModelPath);
            
            (LevelModel levelModel, ItemsSerializationContainer gameItems) =
                levelDeserializer.Deserialize();

            Container.Bind<LevelModel>()
                .FromInstance(levelModel)
                .AsSingle();

            Container.Bind<ItemsSerializationContainer>()
                .FromInstance(gameItems)
                .AsSingle();
        }
    }
}