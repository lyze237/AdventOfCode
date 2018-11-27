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

namespace Day20
{
    public class Particle
    {
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Acceleration { get; set; }

        public Particle(Vector3 position, Vector3 velocity, Vector3 acceleration)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
        }

        
        /// <summary> 
        /// Increase the X velocity by the X acceleration.
        /// Increase the Y velocity by the Y acceleration.
        /// Increase the Z velocity by the Z acceleration.
        /// Increase the X position by the X velocity.
        /// Increase the Y position by the Y velocity.
        /// Increase the Z position by the Z velocity.
        /// </summary>
        public void Tick()
        {
            Velocity += Acceleration;
            Position += Velocity;
        }

        public int Distance()
        {
            return Position.X + Position.Y + Position.Z;
        }

        public override string ToString()
        {
            return $"{nameof(Position)}: {Position}, {nameof(Velocity)}: {Velocity}, {nameof(Acceleration)}: {Acceleration}";
        }
    }
}
