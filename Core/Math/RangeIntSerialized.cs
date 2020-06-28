using UnityEngine;
using BricksBucket.Core;

namespace BricksBucket
{
    /// <summary>
    ///
    /// Range Int Serialized.
    ///
    /// <para>
    /// Serialized Range for integer values.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// </summary>
    [System.Serializable]
    public struct RangeIntSerialized
    {
        #region Class Members

        /// <summary> Min value in range. </summary>
        public int Min;

        /// <summary> Max value in range. </summary>
        public int Max;

        #endregion



        #region Accessors


        /// <summary> Lenght of the range. </summary>
        public int Lenght { get { return Max - Min; } }

        #endregion



        #region Constructor

        /// <summary>
        /// Creates a new struct with min and max values.
        /// </summary>
        /// <param name="min"> Min value in range. </param>
        /// <param name="max"> Max value in range. </param>
        public RangeIntSerialized (int min, int max)
        {
            if (max < min)
                Core.Utils.Swap (ref min, ref max);

            Min = min;
            Max = max;
        }

        #endregion



        #region Class Implementation

        /// <summary>
        /// Evaluates wether a value is in range.
        /// </summary>
        /// <param name="value"> Value to evaluate </param>
        /// <returns> Wether a value is in range. </returns>
        public bool InRange (int value)
        {
            return value.InRange (Min, Max);
        }

        /// <summary>
        /// Returns a random number between [Min, Max].
        /// </summary>
        /// <param name="maxInclusive"> Wether max must be inclusive</param>
        /// <returns> Random number. </returns>
        public int GetRandom (bool maxInclusive = true)
        {
            return Random.Range (Min, maxInclusive ? Max + 1: Max);
        }

        #endregion
    }
}
