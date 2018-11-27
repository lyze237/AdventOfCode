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

using System;
using System.Linq;
using Utils;

namespace Day4
{
    public class Day4Challenge2 : Challenge<int>
    {
        public Day4Challenge2() : base("input")
        {
        }

        public override int Run()
        {
            int okLines = 0;
            foreach (var line in GetInputFilePerLine())
            {
                var wordsArray = line.Split(" ");

                bool foundSame = false;

                for (var i = 0; i < wordsArray.Length; i++)
                {
                    string word = wordsArray[i];

                    if (wordsArray.Where((otherWord, j) => i != j).Any(otherWord => checkAnagram(word, otherWord)))
                    {
                        foundSame = true;
                    }

                    if (foundSame)
                        break;
                }

                if (!foundSame)
                    okLines++;
            }
            return okLines;
        }

        private static bool checkAnagram(string our, string other)
        {
            if (our == other)
                return true;

            if (our.Length != other.Length)
                return false;

            var ourArray = our.ToCharArray();
            var otherArray = other.ToCharArray();

            Array.Sort(ourArray);
            Array.Sort(otherArray);

            var result = new string(ourArray) == new string(otherArray);
            if (result)
            {
                Console.WriteLine($"{our} is anagram of {other}");
            }
            return result;
        }
    }
}
