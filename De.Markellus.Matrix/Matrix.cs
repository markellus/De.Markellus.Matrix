// <copyright file=Matrix>Copyright (c) 2018 Marcel Bulla</copyright>
// <author>Marcel Bulla</author>
// <date> 7/31/2018 6:25:45 PM</date>
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
using System.Collections;
using System.Collections.Generic;

namespace De.Markellus.Matrix
{
    /// <summary>
    /// This class represents a dynamic matrix of any dimension.
    /// The implementation is not mathematically correct, but who cares if the results are the same?
    /// </summary>
    public class Matrix : IEnumerable<double>
    {
        #region Variables

        /// <summary>
        /// The longest row so far. This info is required because all row vectors have to be the same length.
        /// If one row vector gets longer than _iLongestRow, all other row vector will be adjusted to the same length.
        /// </summary>
        private int _iLongestRow;

        /// <summary>
        /// The matrix is built from vectors. Each vector represents a single row of the matrix.
        /// </summary>
        private List<Vector> _listVecRows;

        #endregion

        #region Properties

        /// <summary>
        /// The column count of the matrix
        /// </summary>
        public int Columns => _listVecRows.Count > 0 ? _listVecRows[0].Dimensions : 0;

        /// <summary>
        /// The row count of the matrix
        /// </summary>
        public int Rows => _listVecRows.Count;

        #endregion

        #region Accessors

        /// <summary>
        /// Allows access to the elements of the matrix.
        /// </summary>
        /// <param name="column">The column of the wanted element</param>
        /// <param name="row">The row of the wanted element</param>
        /// <returns>The matrix element</returns>
        public double this[int column, int row]
        {
            get => GetRow(row)[column];

            set
            {
                if (_iLongestRow < column + 1)
                {
                    for (int i = 0; i < this.Rows; i++)
                    {
                        if (this[i].Dimensions <= column)
                        {
                            for (int j = this[i].Dimensions; j <= column; j++)
                            {
                                this[i][j] = default(double);
                            }
                        }
                    }

                    _iLongestRow = column + 1;
                }

                AddVector(ref _listVecRows, row);
                _listVecRows[row][column] = value;
            }
        }

        /// <summary>
        /// Allows access to the rows of the matrix.
        /// </summary>
        /// <param name="row">The row of the wanted element</param>
        /// <returns>A vector representing the a row of the matrix</returns>
        public Vector this[int row] => GetRow(row);

        public Vector GetRow(int row)
        {
            if (this.Rows <= row || row < 0)
            {
                throw new IndexOutOfRangeException();
            }
            return _listVecRows[row];
        }

        public Vector GetColumn(int column)
        {
            if (this.Columns <= column || column < 0)
            {
                throw new IndexOutOfRangeException();
            }

            Vector vecColumn = new Vector(Columns);

            for (int i = 0; i < Columns; i++)
            {
                vecColumn[i] = _listVecRows[i][column];
            }
            return vecColumn;
        }

        #endregion

        public Matrix()
        {
            _listVecRows = new List<Vector>();
        }

        #region IEnumerable Implementation

        public void Add(params double[] row)
        {
            int rowCurrent = this.Rows;

            for (int x = 0; x < row.Length; x++)
            {
                this[x, rowCurrent] = row[x];
            }
        }

        public IEnumerator<double> GetEnumerator()
        {
            return new MatrixEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
        private void AddVector(ref List<Vector> listVectors,  int pos)
        {
            for (int i = listVectors.Count; i <= pos; i++)
            {
                listVectors.Add(new Vector(_iLongestRow));
            }
        }
    }
}
