namespace Framework.Generics
{
    public interface IVariable
    {
        DataType Type { get; }

        object Get(System.Type desiredType);

        void Set(object value);
    }
}