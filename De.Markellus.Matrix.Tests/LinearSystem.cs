using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace De.Markellus.Matrix.Tests
{
    [TestFixture]
    public class LinearSystemTest
    {
        [Test]
        public void TestMultiplyRow()
        {
            Matrix mat = new Matrix()
            {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
            };

            LinearSystem ls = new LinearSystem(mat);
            ls.MultiplyRow(1, 2);
            Assert.AreEqual(ls.CoefficientMatrix.GetRow(0), new Vector {1, 2, 3});
            Assert.AreEqual(ls.CoefficientMatrix.GetRow(1), new Vector {8, 10, 12});
            Assert.AreEqual(ls.CoefficientMatrix.GetRow(2), new Vector {7, 8, 9});

            ls.MultiplyRow(0, 1, 2);
            Assert.AreEqual(ls.CoefficientMatrix.GetRow(0), new Vector { 17, 22, 27 });
            Assert.AreEqual(ls.CoefficientMatrix.GetRow(1), new Vector { 8, 10, 12 });
            Assert.AreEqual(ls.CoefficientMatrix.GetRow(2), new Vector { 7, 8, 9 });

            ls.MultiplyRow(1, 1.0/8.0);
            Assert.AreEqual(ls.CoefficientMatrix.GetRow(0), new Vector { 17, 22, 27 });
            Assert.AreEqual(ls.CoefficientMatrix.GetRow(1), new Vector { 1, 10/8.0, 6/4.0 });
            Assert.AreEqual(ls.CoefficientMatrix.GetRow(2), new Vector { 7, 8, 9 });

            ls.MultiplyRow(0, 1, -17);
            Assert.AreEqual(ls.CoefficientMatrix.GetRow(0), new Vector { 0, 0.75, 1.5 });
            Assert.AreEqual(ls.CoefficientMatrix.GetRow(1), new Vector { 1, 10 / 8.0, 6 / 4.0 });
            Assert.AreEqual(ls.CoefficientMatrix.GetRow(2), new Vector { 7, 8, 9 });
        }

        [Test]
        public void TestCreateRowEchelonForm()
        {
            Matrix mat = new Matrix()
            {
                {1, 1, 2, 0},
                {0, 1, -1, 0},
                {3, 4, 5, 0},
                {3, 5, 4, 0}
            };

            LinearSystem ls = new LinearSystem(mat);
            ls.CreateLowerTriangularCoefficientMatrix();

            Assert.AreEqual(ls.CoefficientMatrix,
                new Matrix
                {
                    {1, 1, 2, 0},
                    {0, 1, -1, 0},
                    {0, 0, 0, 0},
                    {0, 0, 0, 0}
                });


            mat = new Matrix()
            {
                {1, 1, -1, 2},
                {-2, 0, 1, -2},
                {5, -1, 2, 4},
                {2, 6, -3, 5}
            };

            ls = new LinearSystem(mat);
            ls.CreateLowerTriangularCoefficientMatrix();

            Assert.AreEqual(ls.CoefficientMatrix,
                new Matrix
                {
                    {1, 1, -1, 2},
                    {0, 1, -0.5, 1},
                    {0, 0, 1, 0},
                    {0, 0, 0, -3}
                });
        }

        [Test]
        public void TestCreateUpperTriangularCoefficientMatrix()
        {
            Matrix mat = new Matrix()
            {
                {1, 1, 2, 0},
                {0, 1, -1, 0},
                {3, 4, 5, 0},
                {3, 5, 4, 0}
            };

            LinearSystem ls = new LinearSystem(mat);
            ls.CreateUpperTriangularCoefficientMatrix();

            Assert.IsTrue(ls.CoefficientMatrix.Equals(
                new Matrix
                {
                    {0, 0, 0, 0},
                    {1/3.0, 1, 0, 0},
                    {3/5.0, 4/5.0, 1, 0},
                    {0, 0, 0, 0}
                }));
        }

        [Test]
        public void TestBothTriangularCoefficientMatrix()
        {
            Matrix mat = new Matrix()
            {
                {1, 1, -1, 2},
                {-2, 0, 1, -2},
                {5, -1, 2, 4},
                {2, 6, -3, 5}
            };

            LinearSystem ls = new LinearSystem(mat);
            ls.CreateLowerTriangularCoefficientMatrix();
            ls.CreateUpperTriangularCoefficientMatrix();

            Assert.AreEqual(ls.CoefficientMatrix,
                new Matrix
                {
                    {1, 0, 0, 1},
                    {0, 1, 0, 1},
                    {0, 0, 1, 0},
                    {0, 0, 0, -3}
                });

            mat = new Matrix()
            {
                {1, 1, -1, 2},
                {-2, 0, 1, -2},
                {5, -1, 2, 4},
                {2, 6, -3, 5}
            };

            ls = new LinearSystem(mat);
            ls.CreateUpperTriangularCoefficientMatrix();
            ls.CreateLowerTriangularCoefficientMatrix();

            Assert.AreEqual(ls.CoefficientMatrix,
                new Matrix
                {
                    {1, 0, 0, 1},
                    {0, 1, 0, 1},
                    {0, 0, 1, 0},
                    {0, 0, 0, -3}
                });

            mat = new Matrix()
            {
                {2, 3, 4},
                {3, 2, 2},
            };

            ls = new LinearSystem(mat);
            ls.CreateLowerTriangularCoefficientMatrix();
            ls.CreateUpperTriangularCoefficientMatrix();

            Assert.IsTrue(ls.CoefficientMatrix.Equals(
                new Matrix
                {
                    {1, 0, -0.4},
                    {0, 1, 1.6},
                }));

            mat = new Matrix()
            {
                {6, 7, 5},
                {1, 1, 0},
            };

            ls = new LinearSystem(mat);
            ls.CreateLowerTriangularCoefficientMatrix();
            ls.CreateUpperTriangularCoefficientMatrix();

            Assert.IsTrue(ls.CoefficientMatrix.Equals(
                new Matrix
                {
                    {1, 0, -5},
                    {0, 1, 5},
                }));

            mat = new Matrix()
            {
                {2, -3, 9},
                {0, 1, 2},
            };

            ls = new LinearSystem(mat);
            ls.CreateLowerTriangularCoefficientMatrix();
            ls.CreateUpperTriangularCoefficientMatrix();

            Assert.IsTrue(ls.CoefficientMatrix.Equals(
                new Matrix
                {
                    {1, 0, 7.5},
                    {0, 1, 2},
                }));
        }


        [Test]
        public void TestResolve()
        {
            Matrix mat = new Matrix()
            {
                {3,5,20 },
                {-1, 1, 0 }
            };

            LinearSystem ls = new LinearSystem(mat);
            Dictionary<string, double> dic = ls.Resolve();

            Assert.AreEqual(dic["0"], 2.5);
            Assert.AreEqual(dic["1"], 2.5);
        }
    }
}
