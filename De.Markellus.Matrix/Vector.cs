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
using System.Text;

namespace De.Markellus.Matrix
{
    /// <summary>
    /// This class represents a vector of any dimension.
    /// </summary>
    public class Vector<T> : IEnumerable<T> where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
    { 
        private List<T> _listDimensions;

        public int Dimensions => _listDimensions.Count;

        public T this[int index]
        {
            get => _listDimensions[index];

            set
            {
                if (index >= Dimensions)
                {
                    for (int i = Dimensions; Dimensions < index; i++)
                    {
                        _listDimensions.Add(default(T));
                    }
                    _listDimensions.Add(value);
                }

                _listDimensions[index] = value;
            }
        }

        public Vector()
        {
            _listDimensions = new List<T>();
        }

        public Vector(int dimensions) : this()
        {
            if (dimensions < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dimensions));
            }

            if (dimensions > 0)
            {
                this[dimensions-1] = default(T);
            }
        }

        public void Add(T obj)
        {
            this[Dimensions] = obj;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new VectorEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
