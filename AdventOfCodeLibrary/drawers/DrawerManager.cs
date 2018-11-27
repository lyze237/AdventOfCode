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
using System.Threading.Tasks;

namespace AdventOfCodeLibrary.drawers
{
    public class DrawerManager
    {
        private List<Drawer> drawers = new List<Drawer>();

        public DrawerManager()
        {
        }

        public void Add(Drawer drawer)
        {
            drawers.Add(drawer);
        }

        public void Start()
        {
            Task.Run(async () => await Run());
        }

        private async Task Run()
        {
            while (true)
            {
                foreach (var drawer in drawers.Where(d => !d.Done || d.DoneTick))
                    drawer.Tick();

                foreach (var drawer in drawers.Where(d => d.Dirty))
                {
                    drawer.Draw();
                    await Task.Delay(10);
                }

                await Task.Delay(100);
            }
        }
    }
}
