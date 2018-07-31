// <copyright file=Matrix>Copyright (c) 2018 Marcel Bulla</copyright>
// <author>Marcel Bulla</author>
// <date> 7/31/2018 6:25:32 PM</date>
// ****************************************************************************************
// Copyright (C) 2018 Marcel Bulla
// 
// Permission to use, copy, modify, and/or distribute this software for any purpose with or
// without fee is hereby granted, provided that the above copyright notice and this
// permission notice appear in all copies.
// 
// THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES WITH REGARD TO
// THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS. IN NO
// EVENT SHALL THE AUTHOR BE LIABLE FOR ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL
// DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER
// IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF OR IN
// CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
// ****************************************************************************************
using System;
using NUnit.Framework;

namespace De.Markellus.Matrix.Tests
{
    [TestFixture]
    public class Matrix
    {
        [Test]
        public void TestMatrixCreation()
        {
            Matrix<int> matrix = new Matrix<int>
            {
                {1,2,3},
                {4,5,6},
                {7,8,9}
            };

            Assert.AreEqual(3, matrix.Columns, "Column count is wrong");
            Assert.AreEqual(3, matrix.Rows, "Row count is wrong");

            Assert.Throws<IndexOutOfRangeException>(() => matrix.GetRow(-1));
            Assert.Throws<IndexOutOfRangeException>(() => matrix.GetRow(4));

            Assert.Throws<IndexOutOfRangeException>(() => matrix.GetColumn(-1));
            Assert.Throws<IndexOutOfRangeException>(() => matrix.GetColumn(4));

            Assert.AreEqual(new Vector<int>() { 1, 2, 3 }, matrix[0], "Matrix row accessor is broken");
            Assert.AreEqual(new Vector<int>() { 4, 5, 6 }, matrix[1], "Matrix row accessor is broken");
            Assert.AreEqual(new Vector<int>() { 7, 8, 9 }, matrix[2], "Matrix row accessor is broken");

            Assert.AreEqual(new Vector<int>() { 1, 2, 3 }, matrix.GetRow(0), "Matrix row accessor is broken");
            Assert.AreEqual(new Vector<int>() { 4, 5, 6 }, matrix.GetRow(1), "Matrix row accessor is broken");
            Assert.AreEqual(new Vector<int>() { 7, 8, 9 }, matrix.GetRow(2), "Matrix row accessor is broken");

            Assert.AreEqual(new Vector<int>() { 1, 4, 7 }, matrix.GetColumn(0), "Matrix column accessor is broken");
            Assert.AreEqual(new Vector<int>() { 2, 5, 8 }, matrix.GetColumn(1), "Matrix column accessor is broken");
            Assert.AreEqual(new Vector<int>() { 3, 6, 9 }, matrix.GetColumn(2), "Matrix column accessor is broken");

            for (int row = 0; row < matrix.Rows; row++)
            {
                for (int column = 0; column < matrix.Columns; column++)
                {
                    Assert.AreEqual(matrix[column, row], row * 3 + column + 1, "Matrix element accessor is broken");
                }
            }
        }

        [Test]
        public void TestMatrixCreationMissingMembers()
        {
            Matrix<int> matrix = new Matrix<int>
            {
                {1,2,3},
                {4,5},
                {7,8,9}
            };

            Assert.AreEqual(3, matrix.Columns, "Column count is wrong");
            Assert.AreEqual(3, matrix.Rows, "Row count is wrong");

            Assert.AreEqual(matrix[0], new Vector<int>() { 1, 2, 3 }, "Matrix row accessor is broken");
            Assert.AreEqual(matrix[1], new Vector<int>() { 4, 5, 0 }, "Matrix row accessor is broken");
            Assert.AreEqual(matrix[2], new Vector<int>() { 7, 8, 9 }, "Matrix row accessor is broken");

            matrix = new Matrix<int>
            {
                {1,2},
                {4,5,0},
                {7,8,9},
                {0}
            };

            Assert.AreEqual(3, matrix.Columns, "Column count is wrong");
            Assert.AreEqual(4, matrix.Rows, "Row count is wrong");

            Assert.AreEqual(matrix[0], new Vector<int>() { 1, 2, 0 }, "Matrix row accessor is broken");
            Assert.AreEqual(matrix[1], new Vector<int>() { 4, 5, 0 }, "Matrix row accessor is broken");
            Assert.AreEqual(matrix[2], new Vector<int>() { 7, 8, 9 }, "Matrix row accessor is broken");
            Assert.AreEqual(matrix[3], new Vector<int>() { 0, 0, 0 }, "Matrix row accessor is broken");
        }
    }
}
