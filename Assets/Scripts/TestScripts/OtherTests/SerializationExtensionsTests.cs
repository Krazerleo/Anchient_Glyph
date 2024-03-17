using System;
using AncientGlyph.GameScripts.Serialization;
using NUnit.Framework;

using UnityEngine;

namespace AncientGlyph.TestScripts.OtherTests
{
    public class SerializationExtensionsTests
    {
        // Vector3Int

        [Test]
        public void StringToVector3Int_ZEROS()
        {
            var Vec3Expected = new Vector3Int(0, 0, 0);
            var Vec3String = "(0, 0, 0)";

            Assert.AreEqual(Vec3Expected, SerializationExtensions.ParseVector3Int(Vec3String));
        }

        [Test]
        public void StringToVector3Int_FIRST()
        {
            var Vec3Expected = new Vector3Int(5, 1, 3);
            var Vec3String = "(5, 1, 3)";

            Assert.AreEqual(Vec3Expected, SerializationExtensions.ParseVector3Int(Vec3String));
        }

        [Test]
        public void StringToVector3Int_SECOND()
        {
            var Vec3Expected = new Vector3Int(125, 643, 123);
            var Vec3String = "(125, 643, 123)";

            Assert.AreEqual(Vec3Expected, SerializationExtensions.ParseVector3Int(Vec3String));
        }

        [Test]
        public void StringToVector3Int_DoubleFailing()
        {
            var Vec3String = "(125, 643.0032, 123)";

            Assert.Throws<ArgumentException>(() => SerializationExtensions.ParseVector3Int(Vec3String));
        }

        [Test]
        public void StringToVector3Int_ToSmall()
        {
            var Vec3String = "(125, 643)";

            Assert.Throws<ArgumentException>(() => SerializationExtensions.ParseVector3Int(Vec3String));
        }

        [Test]
        public void StringToVector3Int_ToBig()
        {
            var Vec3String = "(125, 643, 123, 1231)";

            Assert.Throws<ArgumentException>(() => SerializationExtensions.ParseVector3Int(Vec3String));
        }

        // Vector3

        [Test]
        public void StringToVector3_ZEROS()
        {
            var Vec3Expected = new Vector3(0, 0, 0);
            var Vec3String = "(0, 0, 0)";

            Assert.AreEqual(Vec3Expected, SerializationExtensions.ParseVector3(Vec3String));
        }

        [Test]
        public void StringToVector3_FIRST()
        {
            var Vec3Expected = new Vector3(5, 1.53f, 3);
            var Vec3String = "(5, 1.53, 3)";

            Assert.AreEqual(Vec3Expected, SerializationExtensions.ParseVector3(Vec3String));
        }

        [Test]
        public void StringToVector3_SECOND()
        {
            var Vec3Expected = new Vector3(125.123f, 643f, 123.111f);
            var Vec3String = "(125.123, 643, 123.111)";

            Assert.AreEqual(Vec3Expected, SerializationExtensions.ParseVector3(Vec3String));
        }

        [Test]
        public void StringToVector3_ToSmall()
        {
            var Vec3String = "(125, 643)";

            Assert.Throws<ArgumentException>(() => SerializationExtensions.ParseVector3(Vec3String));
        }

        [Test]
        public void StringToVector3_ToBig()
        {
            var Vec3String = "(125, 643, 123, 1231)";

            Assert.Throws<ArgumentException>(() => SerializationExtensions.ParseVector3(Vec3String));
        }
    }
}