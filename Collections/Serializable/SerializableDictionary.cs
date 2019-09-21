using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace BricksBucket.Collections
{
    /// <summary>
    ///
    /// Serializable Dictionary.
    ///
    /// <para>
    /// Dictionary version that can be handled by Unity.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// 
    /// </summary>
    public abstract class SerializableDictionary<TKey, TValue> :
    Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        #region Class Properties

        /// <summary> List for keys, used just for serialization. </summary>
        [SerializeField, HideInInspector]
        [SuppressMessage ("Style", "IDE0044:Add readonly modifier")]
        private List<TKey> _keyData = new List<TKey> ();

        /// <summary> List for values, used just for serialization. </summary>
        [SerializeField, HideInInspector]
        [SuppressMessage ("Style", "IDE0044:Add readonly modifier")]
        private List<TValue> _valueData = new List<TValue> ();

        #endregion

        #region ISerializationCallbackReceiver Implementation

        /// <summary> Callback After been deserialized. </summary>
        void ISerializationCallbackReceiver.OnAfterDeserialize ()
        {
            Clear ();
            for (int i = 0; i < _keyData.Count && i < _valueData.Count; i++)
                this[_keyData[i]] = _valueData[i];
        }

        /// <summary> Callback before been serialized. </summary>
        void ISerializationCallbackReceiver.OnBeforeSerialize ()
        {
            _keyData.Clear ();
            _valueData.Clear ();

            foreach (var item in this)
            {
                _keyData.Add (item.Key);
                _valueData.Add (item.Value);
            }
        }

        #endregion
    }
}