namespace Day13
{
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
        
        protected bool Equals(Layer other)
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