namespace AoC.Utils
{
    public interface IMatrixDatatype<T>
    {
        public T FromString(string value);
        public T GetValue();
    }
}