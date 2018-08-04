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

        [Test]
        public void TestCrossProduct()
        {
            Vector vec1 = new Vector { 1, 2, 3 };
            Vector vec2 = new Vector { -2, 1, -1 };
            Vector vecCross = vec1.CrossProduct(vec2);

            Assert.AreEqual(vecCross, new Vector {-5, -5, 5});
            Assert.IsTrue(vec1.IsOrthogonalTo(vecCross));
            Assert.IsTrue(vec2.IsOrthogonalTo(vecCross));

            vec1 = new Vector { 0, 1, 1 };
            vec2 = new Vector { 1, -1, 0 };
            vecCross = vec1.CrossProduct(vec2);

            Assert.AreEqual(vecCross, new Vector { 1, 1, -1 });
            Assert.IsTrue(vec1.IsOrthogonalTo(vecCross));
            Assert.IsTrue(vec2.IsOrthogonalTo(vecCross));

            vec1 = new Vector { 1, 2, -3 };
            vec2 = new Vector { 3, -1, 1 };
            vecCross = vec1.CrossProduct(vec2);

            Assert.AreEqual(vecCross, new Vector { -1, -10, -7 });
            Assert.IsTrue(vec1.IsOrthogonalTo(vecCross));
            Assert.IsTrue(vec2.IsOrthogonalTo(vecCross));

            vec1 = new Vector { 0, 1, 1 };
            vec2 = new Vector { 1, -1, 0 };
            vecCross = vec1.CrossProduct(vec2);
            double dEucl = vecCross.Length();

            Assert.AreEqual(dEucl, Math.Sqrt(3), 0.00001);
            Assert.AreEqual(vecCross, new Vector { 1, 1, -1 });
            Assert.AreEqual(vecCross / dEucl, new Vector() {1 / Math.Sqrt(3), 1 / Math.Sqrt(3), -1 / Math.Sqrt(3)});
            Assert.IsTrue(vec1.IsOrthogonalTo(vecCross));
            Assert.IsTrue(vec2.IsOrthogonalTo(vecCross));
        }

        [Test]
        public void TestIsCollinearTo()
        {
            Vector vec1 = new Vector { 1, 0, 0 };
            Vector vec2 = new Vector { -1, 0, 0 };
            Assert.IsTrue(vec1.IsCollinearTo(vec2));

            vec1 = new Vector { 1, 4, -32 };
            vec2 = new Vector { 25, 100, -800 };
            Assert.IsTrue(vec1.IsCollinearTo(vec2));

            vec1 = new Vector { 1, 4, -32 };
            vec2 = new Vector { -25, -100, 800 };
            Assert.IsTrue(vec1.IsCollinearTo(vec2));

            vec1 = new Vector { 1, 4, 1 };
            vec2 = new Vector { -2, 2, -3 };
            Assert.IsFalse(vec1.IsCollinearTo(vec2));

            Vector vecCross = vec1.CrossProduct(vec2);
            double t = vecCross * new Vector() {0, 0, 0};

            vec1 = new Vector { 3, 2, 1 };
            vec2 = new Vector { 7, -2, 3 };
            Assert.IsFalse(vec1.IsCollinearTo(vec2));
        }

        [Test]
        public void TestAngleBetween()
        {
            Vector vec1 = new Vector {2, 3};
            Vector vec2 = new Vector {5, -7};

            Assert.AreEqual(vec1.AngleBetween(vec2).GetRadian(), Math.Acos(-11 / Math.Sqrt(13*74)), 0.00001);
        }

        [Test]
        public void Combined1()
        {
            Vector vec1 = new Vector { 2, 1, 1 };
            Vector vec2 = new Vector { 1,-1, 4 };

            Assert.AreEqual((vec1 + 2 * vec2).Length(), 7 * Math.Sqrt(2), 0.00001);
            Assert.AreEqual((vec1 + vec2).AngleBetween(vec1 - vec2).GetRadian(), Math.Acos(-12 / (2 * Math.Sqrt(119))));
        }
    }
}