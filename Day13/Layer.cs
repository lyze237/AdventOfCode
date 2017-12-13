namespace Day13
{
    public class Layer
    {
        public int Position { get; }
        public int Depth { get; }

        private bool scannerMovesUp = false;
        public int ScannerDepth { get; private set; } = 0;
        
        public Layer(int position, int depth)
        {
            Position = position;
            Depth = depth;
        }

        public void Reset()
        {
            ScannerDepth = 0;
            scannerMovesUp = false;
        }

        public void MoveScanner()
        {
            if (scannerMovesUp)
            {
                if (ScannerDepth <= 0)
                {
                    scannerMovesUp = false;
                    ScannerDepth++;
                }
                else
                {
                    ScannerDepth--;
                }
            }
            else
            {
                if (ScannerDepth >= Depth - 1)
                {
                    scannerMovesUp = true;
                    ScannerDepth--;
                }
                else
                {
                    ScannerDepth++;
                }
            }
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
            return $"{nameof(Position)}: {Position}, {nameof(Depth)}: {Depth}, {nameof(ScannerDepth)}: {ScannerDepth}";
        }
        
        #endregion
    }
}