using System;
using System.Collections.Generic;
using System.Text;

namespace De.Markellus.Matrix
{
    public class LinearSystem
    {
        private Matrix _matCoefficient;

        public Matrix CoefficientMatrix => _matCoefficient;

        /// <summary>
        /// Creates a new linear system from a coefficient matrix.
        /// </summary>
        /// <param name="matCoefficient"></param>
        public LinearSystem(Matrix matCoefficient)
        {
            _matCoefficient = matCoefficient;
        }

        /// <summary>
        /// Creates a linear system from math equations
        /// </summary>
        /// <param name="strEquation"></param>
        public LinearSystem(string[] arrStrEquations)
        {
            throw new NotImplementedException();
        }

        public void CreateRowEchelonForm() => CreateLowerTriangularCoefficientMatrix();


        public void CreateLowerTriangularCoefficientMatrix()
        {
            if (_matCoefficient.Rows < 1 || _matCoefficient.Columns < 3)
            {
                throw new Exception();//TODO: Add exceptions when math stuff fails in Vector/Matrix
            }

            Vector vecFirst = _matCoefficient.GetRow(0);

            MultiplyRow(0, 1 / vecFirst[0]);

            int iErrors = 0;

            for (int i = 1; i < _matCoefficient.Rows; i++)
            {
                Vector vecRow = _matCoefficient.GetRow(i);

                for (int j = 0; j < i && j < _matCoefficient.Columns; j++)
                {
                    MultiplyRow(i, j, -vecRow[j]);
                }

                if (vecRow[i] == 0 && i + 1 != _matCoefficient.Rows)
                {
                    if (iErrors <= Math.Pow(_matCoefficient.Rows, 2))
                    {
                        iErrors++;
                        _matCoefficient.SwapRows(i, i-- + 1);
                    }
                }
                else if (i + 1 < _matCoefficient.Columns)
                {
                    MultiplyRow(i, 1 / vecRow[i]);
                }
            }
        }

        public void CreateUpperTriangularCoefficientMatrix()
        {
            if (_matCoefficient.Rows < 2 || _matCoefficient.Columns < 3)
            {
                throw new Exception();//TODO: Add exceptions when math stuff fails in Vector/Matrix
            }

            int iLastRow = _matCoefficient.Columns - 2 < _matCoefficient.Rows
                ? _matCoefficient.Columns - 2
                : _matCoefficient.Rows - 1;

            int iReserve = _matCoefficient.Rows - iLastRow - 1;
            int iLastReserve = iReserve;

            Vector vecLast = _matCoefficient.GetRow(iLastRow);

            MultiplyRow(iLastRow, 1 / vecLast[vecLast.Dimensions - 2]);

            int iErrors = 0;

            for (int i = iLastRow - 1; i >= 0; i--)
            {
                Vector vecRow = _matCoefficient.GetRow(i);

                for (int j = _matCoefficient.Columns - 2; j > i && j < _matCoefficient.Columns; j--)
                {
                    MultiplyRow(i, j, -vecRow[j]);
                }

                if (vecRow[i] == 0 && i + 1 != _matCoefficient.Rows)
                {
                    if (iErrors <= Math.Pow(_matCoefficient.Rows, 2))
                    {
                        if (iLastReserve == 0) iLastReserve = iReserve;

                        iErrors++;
                        _matCoefficient.SwapRows(i++, iLastRow + iLastReserve--);
                    }
                }
                else if (i + 1 < _matCoefficient.Columns)
                {
                    MultiplyRow(i, 1 / vecRow[i]);
                }
            }
        }

        public void MultiplyRow(int iRow, int iSourceRow, double dTimes)
        {
            if (iRow >= _matCoefficient.Rows || iRow < 0)
            {
                throw new Exception();//TODO: Add exceptions when math stuff fails in Vector/Matrix
            }
            if (iSourceRow >= _matCoefficient.Rows || iSourceRow < 0)
            {
                throw new Exception();//TODO: Add exceptions when math stuff fails in Vector/Matrix
            }

            Vector vecTarget = _matCoefficient.GetRow(iRow);
            Vector vecSource = _matCoefficient.GetRow(iSourceRow);

            for (int i = 0; i < vecTarget.Dimensions; i++)
            {
                vecTarget[i] = vecTarget[i] + vecSource[i] * dTimes;
            }
        }


        public void MultiplyRow(int iRow, double dFactor)
        {
            if (iRow >= _matCoefficient.Rows || iRow < 0)
            {
                throw new Exception();//TODO: Add exceptions when math stuff fails in Vector/Matrix
            }

            Vector vecRow = _matCoefficient.GetRow(iRow);

            for (int i = 0; i < vecRow.Dimensions; i++)
            {
                vecRow[i] = vecRow[i] * dFactor;
            }
        }

        public Dictionary<string, double> Resolve()
        {
            this.CreateLowerTriangularCoefficientMatrix();
            this.CreateUpperTriangularCoefficientMatrix();

            for (int i = 0; i < _matCoefficient.Rows; i++)
            {
                Vector vecRow = _matCoefficient.GetRow(i);
                if (vecRow.LengthSquared() == 0)
                {
                    _matCoefficient.DeleteRow(i--);
                }
                else
                {
                    bool bNotZero = false;

                    for (int j = 0; j < vecRow.Dimensions - 1; j++)
                    {
                        if (vecRow[j] != 0)
                        {
                            bNotZero = true;
                        }
                    }

                    if (!bNotZero && vecRow[vecRow.Dimensions - 1] != 0)
                    {
                        throw new Exception("Linear system has no solution");//TODO: Add exceptions when math stuff fails in Vector/Matrix
                    }
                }
            }

            if (_matCoefficient.Columns - 1 > _matCoefficient.Rows)
            {
                throw new NotImplementedException("Linear systems with more than one solution are not yet supported");
            }

            Vector vecSolution = _matCoefficient.GetColumn(_matCoefficient.Columns - 1);
            Dictionary<string, double> dicSolution = new Dictionary<string, double>(vecSolution.Dimensions);

            for (int i = 0; i < vecSolution.Dimensions; i++)
            {
                dicSolution.Add(i.ToString(),vecSolution[i]);//TODO: variable names
            }

            return dicSolution;
        }

        public override string ToString()
        {
            string strResult = "";
            string[] arrStrLines = _matCoefficient.ToString().Split(new[] {Environment.NewLine}, StringSplitOptions.None);

            foreach (string strLine in arrStrLines)
            {
                string[] strParts = strLine.Split(' ');

                for (int i = 0; i < strParts.Length; i++)
                {
                    strResult += strParts[i] + " ";
                    if (i + 1 == strParts.Length) strResult += "|";
                }

                strResult += Environment.NewLine;
            }

            return strResult;
        }
    }
}
