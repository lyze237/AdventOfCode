namespace Day10
{
    public class WrapArray<T>
    {
        private T[] array;
        
        public WrapArray(T[] array)
        {
            this.array = array;
        }

        public T this[int index]
        {
            get => array[index % array.Length];
            set => array[index % array.Length] = value;
        }

        public int Length => array.Length;
    }
}