using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace BricksBucket.Collections
{
    /// <summary>
    ///
    /// Serializable Queue.
    ///
    /// <para>
    /// FIFO collection structure that can be handled by Unity.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// 
    /// </summary>
    public abstract class SerializableQueue<T> :
    Queue<T>, ISerializationCallbackReceiver
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
            for (int i = 0; i < _values.Count; i++)
                Enqueue (_values[i]);
        }

        /// <summary> Callback before been serialized. </summary>
        void ISerializationCallbackReceiver.OnBeforeSerialize ()
        {
            _values.Clear ();
            T[] items = ToArray ();
            for (int i = 0; i < items.Length; i++)
                _values.Add (items[i]);
        }

        #endregion
    }
}
