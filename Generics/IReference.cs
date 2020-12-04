using UnityEngine;

namespace Framework.Generics
{
    public interface IReference
    {
        Object Component { get; }
        
        string Property { get; }
        
        IVariable Variable { get; }
        
        IHardwiredRegistry Hardwired { get; }

        void SetReference(Object reference);

        void SetProperty(string propertyName);

        object GetValue();

        void SetValue(object propertyValue);
    }
}