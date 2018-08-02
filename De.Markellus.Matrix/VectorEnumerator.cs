// <copyright file=VectorEnumerator>Copyright (c) 2018 Marcel Bulla</copyright>
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
    public class VectorEnumerator : IEnumerator<double>
    {
        private Vector _vector;

        private int _position = -1;

        public VectorEnumerator(Vector vector)
        {
            _vector = vector;
        }

        public bool MoveNext()
        {
            _position++;
            return (_position < _vector.Dimensions);
        }

        public void Reset()
        {
            _position = -1;
        }

        public double Current => _vector[_position];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}
