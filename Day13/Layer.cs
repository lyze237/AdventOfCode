//-----------------------------------------------------------------------
// <copyright>
// MIT License
//
// Copyright (c) 2018 Michael Weinberger lyze@owl.sh
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------

namespace Day13
{
    public class Layer
    {
        public int Position { get; }
        public int Depth { get; }

        private int DepthMultiply;
        
        public Layer(int position, int depth)
        {
            Position = position;
            Depth = depth;
            DepthMultiply = (Depth - 1) * 2;
        }

        public bool IsCaught(int delay)
        {
            return (delay % DepthMultiply) == 0;
        }
        
        public int GetSeverity()
        {
            return Depth * Position;
        }

        #region stuff
        
        protected bool Equals(Layer other)
        {
            return Position == other.Position;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Layer) obj);
        }

        public override int GetHashCode()
        {
            return Position;
        }

        public override string ToString()
        {
            return $"{nameof(Position)}: {Position}, {nameof(Depth)}: {Depth}";
        }
        
        #endregion
    }
}
