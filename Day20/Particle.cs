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