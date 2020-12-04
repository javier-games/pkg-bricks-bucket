namespace Framework.Generics
{
    public interface IHardwiredRegistry
    {
        string Path { get; }
        
        string NameSpace { get; }

        System.Type[] Array { get; }
        
        bool ContainsComponent(string component);
        
        bool ContainsProperty(string component, string property);
        
        object GetValue(object component, string property);

        void SetValue(object component, string property, object value);
    }
}