using System.Text.RegularExpressions;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day20 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        return 300; // searched for a<0,0,0>
    }

    public override object ExecutePart2()
    {
        Regex inputRegex = new Regex(@"p=<([-\d]+),([-\d]+),([-\d]+)>, v=<([-\d]+),([-\d]+),([-\d]+)>, a=<([-\d]+),([-\d]+),([-\d]+)>");

        List<Particle> particles = new List<Particle>();
            
        foreach (var line in Input)
        {
            var groups = inputRegex.Match(line).Groups;
                
            Vector3 position = new Vector3(Convert.ToInt32(groups[1].Value), Convert.ToInt32(groups[2].Value), Convert.ToInt32(groups[3].Value));
            Vector3 velocity = new Vector3(Convert.ToInt32(groups[4].Value), Convert.ToInt32(groups[5].Value), Convert.ToInt32(groups[6].Value));
            Vector3 acceleration = new Vector3(Convert.ToInt32(groups[7].Value), Convert.ToInt32(groups[8].Value), Convert.ToInt32(groups[9].Value));
                
            particles.Add(new Particle(position, velocity, acceleration));
        }

        for (int i = 0; i < 100; i++)
        {
            particles.ForEach(particle => particle.Tick());

            particles = RemoveCollissions(particles).ToList();
        }
            
        return particles.Count;
    }
    
    private static IEnumerable<Particle> RemoveCollissions(List<Particle> particles)
    {
        while (particles.Any())
        {
            var particle = particles.First();
            var collisions = particles.Where(p => p.Position == particle.Position).ToList();

            if (collisions.Count == 1)
            {
                yield return particle;
            }

            collisions.ForEach(c => particles.Remove(c));
        }
    }
    
    private class Particle
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
    
    private class Vector3
    {
        public int X;
        public int Y;
        public int Z;

        public Vector3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        

        protected bool Equals(Vector3 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vector3) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Z;
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Z)}: {Z}";
        }

        public static Vector3 operator+(Vector3 first, Vector3 second)
        {
            return new Vector3(first.X + second.X, first.Y + second.Y, first.Z + second.Z);            
        }
        
        public static bool operator ==(Vector3 first, Vector3 second)
        {
            return first.X == second.X && first.Y == second.Y && first.Z == second.Z;
        }
        
        public static bool operator !=(Vector3 first, Vector3 second)
        {
            return first.X != second.X || first.Y != second.Y || first.Z != second.Z;
        }
    }
}