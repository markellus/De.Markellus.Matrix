// <copyright file=Vector>Copyright (c) 2018 Marcel Bulla</copyright>
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
    /// This class represents a vector of any dimension.
    /// </summary>
    public class Vector : IEnumerable<double>
    {
        private const double PRECISION = 0.00000000001;

        public static readonly Vector Vector1Zero = new Vector {0};
        public static readonly Vector Vector2Zero = new Vector {0, 0};
        public static readonly Vector Vector3Zero = new Vector {0, 0, 0};

        /// <summary>
        /// The dimensions of the vector
        /// </summary>
        private List<double> _listDimensions;

        /// <summary>
        /// Returns the number of dimensions
        /// </summary>
        public int Dimensions => _listDimensions.Count;

        public double this[int index]
        {
            get => _listDimensions[index];

            set
            {
                if (index >= Dimensions)
                {
                    for (int i = Dimensions; Dimensions < index; i++)
                    {
                        _listDimensions.Add(default(double));
                    }
                    _listDimensions.Add(value);
                }

                _listDimensions[index] = value;
            }
        }

        public static Vector operator +(Vector vec1, Vector vec2)
        {
            if (vec1.Dimensions != vec2.Dimensions)
            {
                throw new Exception();//TODO: Add exceptions when math stuff fails in Vector/Matrix
            }
            Vector vecNew = new Vector();

            for (int i = 0; i < vec1.Dimensions; i++)
            {
                vecNew[i] = vec1[i] + vec2[i];
            }

            return vecNew;
        }

        public static Vector operator -(Vector vec1, Vector vec2)
        {
            if (vec1.Dimensions != vec2.Dimensions)
            {
                throw new Exception();//TODO: Add exceptions when math stuff fails in Vector/Matrix
            }
            Vector vecNew = new Vector();

            for (int i = 0; i < vec1.Dimensions; i++)
            {
                vecNew[i] = vec1[i] - vec2[i];
            }

            return vecNew;
        }

        public static double operator *(Vector vec1, Vector vec2)
        {
            if (vec1.Dimensions != vec2.Dimensions)
            {
                throw new Exception();//TODO: Add exceptions when math stuff fails in Vector/Matrix
            }

            double dResult = 0;

            for (int i = 0; i < vec1.Dimensions; i++)
            {
                dResult += vec1[i] * vec2[i];
            }

            return dResult;
        }

        public static Vector operator *(Vector vec1, double dScalar)
        {
            Vector vecNew = new Vector();

            for (int i = 0; i < vec1.Dimensions; i++)
            {
                vecNew[i] = vec1[i] * dScalar;
            }

            return vecNew;
        }

        public static Vector operator *(double dScalar, Vector vec1)
        {
            return vec1 * dScalar;
        }

        public static Vector operator /(Vector vec1, double dScalar)
        {
            return vec1 * (1.0 / dScalar);
        }

        public Vector()
        {
            _listDimensions = new List<double>();
        }

        public Vector(int dimensions) : this()
        {
            if (dimensions < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dimensions));
            }

            if (dimensions > 0)
            {
                this[dimensions-1] = default(double);
            }
        }

        public double DistanceSquared(Vector vecOther)
        {
            if (this.Dimensions != vecOther.Dimensions)
            {
                throw new Exception();//TODO: Add exceptions when math stuff fails in Vector/Matrix
            }

            double dTotal = 0;

            for (int i = 0; i < this.Dimensions; i++)
            {
                dTotal += Math.Pow(this[i] - vecOther[i], 2);
            }

            return dTotal;
        }

        public double Distance(Vector vecOther)
        {
            return Math.Sqrt(DistanceSquared(vecOther));
        }

        public double LengthSquared()
        {
            double dResult = 0;

            for (int i = 0; i < this.Dimensions; i++)
            {
                dResult += Math.Pow(this[i], 2);
            }

            return dResult;
        }

        public double Length()
        {
            return Math.Sqrt(LengthSquared());
        }

        /// <summary>
        /// Checks if the given vector "stands" on this one (90 degrees between both vectors)
        /// </summary>
        /// <param name="vecOther">The other vector</param>
        /// <param name="dDelta">Error margin</param>
        /// <returns></returns>
        public bool IsOrthogonalTo(Vector vecOther, double dDelta = PRECISION)
        {
            return Math.Abs(this * vecOther) < dDelta;
        }

        public Vector OrthogonalProjection(Vector vecOnto)
        {
            return (this * vecOnto) / LengthSquared() * this;
        }

        public Vector CrossProduct(Vector vecOther)
        {
            if (this.Dimensions != 3 || vecOther.Dimensions != 3)
            {
                throw new Exception();//TODO: Add exceptions when math stuff fails in Vector/Matrix
            }

            return new Vector
            {
                this[1] * vecOther[2] - this[2] * vecOther[1],
                this[2] * vecOther[0] - this[0] * vecOther[2],
                this[0] * vecOther[1] - this[1] * vecOther[0]
            };
        }

        public bool IsCollinearTo(Vector vecOther)
        {
            return this.CrossProduct(vecOther).Equals(Vector3Zero);
        }

        public void SetToLength(double length)
        {
            this._listDimensions = (this / Length() * length)._listDimensions;
        }

        public void Add(double obj)
        {
            this[Dimensions] = obj;
        }

        public override bool Equals(object obj)
        {
            Vector vecOther = (Vector) obj;

            if (vecOther == null || this.Dimensions != vecOther.Dimensions)
            {
                return false;
            }

            for (int i = 0; i < this.Dimensions; i++)
            {
                if (Math.Abs(this[i] - vecOther[i]) > PRECISION) return false;
            }

            return true;
        }

        public IEnumerator<double> GetEnumerator()
        {
            return new VectorEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
