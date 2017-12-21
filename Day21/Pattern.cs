using System.Collections.Generic;
using System.Linq;

namespace Day21
{
    public class Pattern
    {
        public List<string> Content { get; set; }

        public Pattern(List<string> content)
        {
            Content = content;
        }

        public Pattern(int size) : this()
        {
            for (int i = 0; i < size; i++)
            {
                Content.Add(new string(' ', size));
            }
        }

        public Pattern()
        {
            Content = new List<string>();
        }

        public int Length()
        {
            return Content[0].Length;
        }

        public static bool operator ==(Pattern first, Pattern second)
        {   
            if (first.Length() != second.Length())
                return false;
            
            for (var i = 0; i < first.Content.Count; i++)
            {
                if (first.Content[i] != second.Content[i])
                    return false;
            }

            return true;
        }
        
        public static bool operator !=(Pattern first, Pattern second)
        {   
            return !(first == second);
        }

        public int Count(bool on)
        {
            return Content.Sum(line => line.Count(c => on && c == '#' || !on && c == '.'));
        }
    }
}