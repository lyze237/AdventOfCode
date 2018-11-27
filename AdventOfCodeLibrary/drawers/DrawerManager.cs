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
