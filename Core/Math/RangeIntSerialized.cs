using UnityEngine;

namespace BricksBucket.Core.Math
{
    /// <!-- RangeIntSerialized -->
    /// 
    /// <summary>
    /// Serialized Range for integer values.
    /// </summary>
    ///
    /// <seealso cref="RangeFloatSerialized"/>
    /// <seealso cref="MinMaxRangeAttribute"/>
    /// 
    /// <!-- By Javier García | @jvrgms | 2020 -->
    [System.Serializable]
    public struct RangeIntSerialized
    {
        #region Class Members

        /// <summary> Min value in range. </summary>
        public int m_min;

        /// <summary> Max value in range. </summary>
        public int m_max;

        #endregion


        #region Accessors
        
        /// <summary> Length of the range. </summary>
        public int Length => m_max - m_min;

        #endregion


        #region Constructor

        /// <summary>
        /// Creates a new struct with min and max values.
        /// </summary>
        /// <param name="min"> Min value in range. </param>
        /// <param name="max"> Max value in range. </param>
        public RangeIntSerialized (int min, int max)
        {
            if (max < min) Utils.Swap (ref min, ref max);
            m_min = min;
            m_max = max;
        }

        #endregion


        #region Class Implementation

        /// <summary>
        /// Evaluates whether a value is in range.
        /// </summary>
        /// <param name="value"> Value to evaluate </param>
        /// <returns> Whether a value is in range. </returns>
        public bool InRange (int value) => value.InRange (m_min, m_max);

        /// <summary>
        /// Returns a random number between [Min, Max].
        /// </summary>
        /// <param name="maxInclusive"> Whether max must be inclusive</param>
        /// <returns> Random number. </returns>
        public int GetRandom (bool maxInclusive = true) =>
            Random.Range (m_min, maxInclusive ? m_max + 1 : m_max);

        #endregion
    }
}
