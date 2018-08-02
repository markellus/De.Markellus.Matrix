using System;
using NUnit.Framework;

namespace De.Markellus.Matrix.Tests
{
    [TestFixture]
    public class VectorTest
    {
        [Test]
        public void TestLength()
        {
            Vector vec = new Vector {3, -5, 4};
            Assert.AreEqual(vec.LengthSquared(), 50, 0.00001, "Vector Length function is broken");
            Assert.AreEqual(vec.Length(), Math.Sqrt(50), 0.00001, "Vector Length function is broken");

            vec = new Vector { 1, 1, 1 };
            Assert.AreEqual(vec.LengthSquared(), 3, 0.01, "Vector Length function is broken");
            Assert.AreEqual(vec.Length(), Math.Sqrt(3), 0.00001, "Vector Length function is broken");
        }

        [Test]
        public void TestSetLength()
        {
            Vector vec = new Vector { 3, -5, 4 };
            vec.SetToLength(Math.Sqrt(50));

            Assert.AreEqual(vec[0], 3, 0.00001, "Vector SetLength function is broken");
            Assert.AreEqual(vec[1], -5, 0.00001, "Vector SetLength function is broken");
            Assert.AreEqual(vec[2], 4, 0.00001, "Vector SetLength function is broken");


            Assert.AreEqual(vec.LengthSquared(), 50, 0.00001, "Vector SetLength function is broken");
            Assert.AreEqual(vec.Length(), Math.Sqrt(50), 0.00001, "Vector SetLength function is broken");
            vec.SetToLength(20);

            Assert.AreEqual(vec[0], 12 / Math.Sqrt(2), 0.00001, "Vector SetLength function is broken");
            Assert.AreEqual(vec[1], -20 / Math.Sqrt(2), 0.00001, "Vector SetLength function is broken");
            Assert.AreEqual(vec[2], 16 / Math.Sqrt(2), 0.00001, "Vector SetLength function is broken");

            Assert.AreEqual(vec.LengthSquared(), 20*20, 0.00001, "Vector SetLength function is broken");
            Assert.AreEqual(vec.Length(), 20, 0.00001, "Vector SetLength function is broken");
        }

        [Test]
        public void TestIsOrthogonalTo()
        {
            Vector vec1 = new Vector { 3, -5, 4 };
            Vector vec2 = new Vector { 3, -5, 4 };
            Assert.False(vec1.IsOrthogonalTo(vec2));

            vec1 = new Vector { 3, -2, 2 };
            vec2 = new Vector { 1, 1, -1 };
            Assert.False(vec1.IsOrthogonalTo(vec2));

            vec1 = new Vector { 3, -1, 2 };
            vec2 = new Vector { 1, 1, -1 };
            Assert.True(vec1.IsOrthogonalTo(vec2));

            vec1 = new Vector { 6, -2, 4 };
            vec2 = new Vector { 1, 1, -1 };
            Assert.True(vec1.IsOrthogonalTo(vec2));
        }

        [Test]
        public void TestOrthogonalProjection()
        {
            Vector vec1 = new Vector { 2, -2, 1 };
            Vector vec2 = new Vector { -2, 5, 0 };

            Assert.AreEqual(vec1.OrthogonalProjection(vec2), new Vector{-28.0/9, 28.0/9, -14.0/9});
        }
    }
}