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
