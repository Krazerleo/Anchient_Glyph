using AncientGlyph.GameScripts.GameSystems.InventorySystem;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.Services.LoggingService;
using NUnit.Framework;

namespace AncientGlyph.TestScripts.ModelTests
{
    [TestFixture]
    public class EquipmentTests
    {
        private PlayerEquipment _playerEquipment;

        [SetUp]
        public void SetUp()
        {
            ILoggingService logger = new TestLoggingService();
            _playerEquipment = new PlayerEquipment(logger);
        }

        [Test]
        public void EquipSimpleItem_CanBeStoredInSlot()
        {
            EquipableItem item = EquipableItem.CreateInstance(EquipableItemType.Helmet);
            bool result = _playerEquipment.TrySetItemInSlot(item, EquipableItemType.Helmet, pushItemToSlot: true);

            Assert.That(result, Is.True);
        }
        
        [Test]
        public void EquipSimpleItem_SlotOccupied()
        {
            EquipableItem item = EquipableItem.CreateInstance(EquipableItemType.Helmet);
            bool result = _playerEquipment.TrySetItemInSlot(item, EquipableItemType.Helmet, pushItemToSlot: true);

            Assert.That(result, Is.True);
            
            EquipableItem newItem = EquipableItem.CreateInstance(EquipableItemType.Helmet);
            bool resultNext = _playerEquipment.TrySetItemInSlot(newItem, EquipableItemType.Helmet, pushItemToSlot: true);
            
            Assert.That(resultNext, Is.False);
        }
    }
}