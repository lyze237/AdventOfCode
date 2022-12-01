using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day13 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var layers = Input
            .Select(line => line.Split(':'))
            .Select(splitted =>
                new Layer(Convert.ToInt32(splitted[0].Trim()), Convert.ToInt32(splitted[1].Trim()))).ToList();

        var caughtAmount = 0;

        var maxLayerCount = layers.Max(l => l.Position);
        for (var i = 0; i <= maxLayerCount; i++)
        {
            var layer = layers.FirstOrDefault(l => l.Position == i);
            if (layer?.IsCaught(i) == true)
            {
                caughtAmount += layer.GetSeverity();
            }
        }
            
        return caughtAmount;
    }

    public override object ExecutePart2()
    {
        bool caught;
        var delay = 0;

        var layers = Input
            .Select(line => line.Split(':'))
            .Select(splitted =>
                new Layer(Convert.ToInt32(splitted[0].Trim()), Convert.ToInt32(splitted[1].Trim()))).ToList();

        do
        {
            caught = false;          

            var maxLayerCount = layers.Max(l => l.Position);
                
            for (var i = 0; i <= maxLayerCount; i++)
            {
                var layer = layers.FirstOrDefault(l => l.Position == i);
                if (layer?.IsCaught(delay + i) == true)
                {
                    caught = true;
                    delay++;
                    break;
                }
            }
        } while (caught);
            
        return delay;
    }

    public class Layer
    {
        public int Position { get; }
        public int Depth { get; }

        private int DepthMultiply;
        
        public Layer(int position, int depth)
        {
            Position = position;
            Depth = depth;
            DepthMultiply = (Depth - 1) * 2;
        }

        public bool IsCaught(int delay)
        {
            return (delay % DepthMultiply) == 0;
        }
        
        public int GetSeverity()
        {
            return Depth * Position;
        }

        #region stuff

        private bool Equals(Layer other)
        {
            return Position == other.Position;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Layer) obj);
        }

        public override int GetHashCode()
        {
            return Position;
        }

        public override string ToString()
        {
            return $"{nameof(Position)}: {Position}, {nameof(Depth)}: {Depth}";
        }
        
        #endregion
    }
}