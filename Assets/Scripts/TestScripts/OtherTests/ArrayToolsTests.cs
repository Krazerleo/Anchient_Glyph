using AncientGlyph.GameScripts.Geometry;

using NUnit.Framework;

namespace AncientGlyph.TestScripts.OtherTests
{
    public class ArrayToolsTests
    {
        [Test]
        [TestCase(0)]   [TestCase(24)]  [TestCase(29)]
        [TestCase(53)]  [TestCase(64)] [TestCase(853)]
        [TestCase(141)] [TestCase(556)] [TestCase(1028)]
        public void ThreeDimToOneDimIndexAndReverse(int index)
        {
            var (xSize, zSize) = (64, 64);
            var (xIndex, yIndex, zIndex) = ArrayTools.Get3dArrayIndex(index, xSize, zSize);

            Assert.AreEqual(index, ArrayTools.Get1dArrayIndex(xIndex,
                yIndex, zIndex, xSize, zSize));
        }
    }
}
