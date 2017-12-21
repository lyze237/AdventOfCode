using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Day21
{
    public class Rule
    {
        public List<Pattern> Froms { get; set; }
        public Pattern To { get; set; }

        public Rule(Pattern from, Pattern to)
        {
            To = to;
            
            GenerateFroms(from);
        }
        
        public Rule(string from, string to)
        {
            Pattern fromPattern = new Pattern();
            Froms = new List<Pattern>();
            To = new Pattern();
            
            foreach (var line in from.Split('/'))
            {
                fromPattern.Content.Add(line);
            }
            foreach (var line in to.Split('/'))
            {
                To.Content.Add(line);
            }
            
            GenerateFroms(fromPattern);
        }

        private void GenerateFroms(Pattern from)
        {
            Froms.Add(Rotate(from));
            for (int z = 0; z < 3; z++)
            {
                Froms.Add(Rotate(Froms.Last()));
            }

            from = FlipY(from);
            Froms.Add(Rotate(from));
            for (int z = 0; z < 3; z++)
            {
                Froms.Add(Rotate(Froms.Last()));
            }
            
            from = FlipX(from);
            Froms.Add(Rotate(from));
            for (int z = 0; z < 3; z++)
            {
                Froms.Add(Rotate(Froms.Last()));
            }

            from = FlipY(from);
            Froms.Add(Rotate(from));
            for (int z = 0; z < 3; z++)
            {
                Froms.Add(Rotate(Froms.Last()));
            }
            
            from = FlipX(from);
            Froms.Add(Rotate(from));
            for (int z = 0; z < 3; z++)
            {
                Froms.Add(Rotate(Froms.Last()));
            }
        }

        private Pattern FlipY(Pattern pattern)
        {
            Pattern newPattern = new Pattern(pattern.Length());

            for (int i = 0; i < newPattern.Length(); i++)
            {
                newPattern.Content[i] = pattern.Content[i];
            }

            newPattern.Content.Reverse();

            return newPattern;
        }

        private Pattern FlipX(Pattern pattern)
        {
            Pattern newPattern = new Pattern(pattern.Length());
            
            for (int i = 0; i < newPattern.Length(); i++)
            {
                newPattern.Content[i] = pattern.Content[i];
                newPattern.Content[i].Reverse();
            }

            return newPattern;
        }

        private Pattern Rotate(Pattern from)
        {
            var pattern = new Pattern(from.Length());
            
            
            for (var i = from.Content.Count - 1; i >= 0; i--)
            {
                for (var j = 0; j < from.Content[i].Length; j++)
                {
                    var sb = new StringBuilder(pattern.Content[j]);
                    sb[from.Content.Count - 1 - i] = from.Content[i][j];
                    pattern.Content[j] = sb.ToString();
                }
            }

            return pattern;
        }

        public bool Matches(Pattern input)
        {
            return Froms.Any(pattern => input == pattern);
        }
    }
}