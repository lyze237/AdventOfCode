using System;

namespace Day24
{
    public class Component
    {
        public int Left { get; set; }
        public int Right { get; set; }

        private Guid guid;

        public Component(int left, int right)
        {
            Left = left;
            Right = right;

            guid = Guid.NewGuid();
        }

        protected bool Equals(Component other)
        {
            return guid.Equals(other.guid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Component) obj);
        }

        public override int GetHashCode()
        {
            return guid.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(Left)}: {Left}, {nameof(Right)}: {Right}";
        }
    }
}