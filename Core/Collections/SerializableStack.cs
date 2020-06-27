using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace BricksBucket.Collections
{
    /// <summary>
    ///
    /// Serialized Stack.
    ///
    /// <para>
    /// LIFO collection structure that can be handled by Unity.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// </summary>
    public abstract class SerializableStack<T> :
    Stack<T>, ISerializationCallbackReceiver
    {
        #region Class Properties

        /// <summary> List for keys, used just for serialization. </summary>
        [SerializeField, HideInInspector]
        [SuppressMessage ("Style", "IDE0044:Add readonly modifier")]
        private List<T> _values = new List<T> ();

        #endregion

        #region ISerializationCallbackReceiver Implementation

        /// <summary> Callback After been deserialized. </summary>
        void ISerializationCallbackReceiver.OnAfterDeserialize ()
        {
            Clear ();
            for (int i = _values.Count -1; i >= 0; i--)
                Push (_values[i]);
        }

        /// <summary> Callback before been serialized. </summary>
        void ISerializationCallbackReceiver.OnBeforeSerialize ()
        {
            _values.Clear ();
            T[] items = ToArray ();
            for(int i = 0; i < items.Length; i++)
                _values.Add (items[i]);
        }

        #endregion
    }
}
