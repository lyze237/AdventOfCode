using System.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Utils;

namespace Day20
{
    public class Day20Challenge2 : Challenge<int>
    {
        public Day20Challenge2() : base("input")
        {
        }

        public override int Run()
        {
            Regex inputRegex = new Regex(@"p=<([-\d]+),([-\d]+),([-\d]+)>, v=<([-\d]+),([-\d]+),([-\d]+)>, a=<([-\d]+),([-\d]+),([-\d]+)>");

            List<Particle> particles = new List<Particle>();
            
            foreach (var line in GetInputFilePerLine())
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
    }
}