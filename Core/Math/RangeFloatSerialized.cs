using UnityEngine;

namespace BricksBucket.Core.Math
{
	/// <!-- RangeFloatSerialized -->
	/// 
	/// <summary>
	/// Serialized Range for floating point values.
	/// </summary>
	///
	/// <seealso cref="RangeIntSerialized"/>
	/// <seealso cref="MinMaxRangeAttribute"/>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	public struct RangeFloatSerialized
	{
		#region Class Members

		/// <summary> Min value in range. </summary>
		public float m_min;

		/// <summary> Max value in range. </summary>
		public float m_max;

		#endregion
		

		#region Accessors

		/// <summary> Length of the range. </summary>
		public float Length => m_max - m_min;

		#endregion


		#region Constructor

		/// <summary>
		/// Creates a new struct with min and max values.
		/// </summary>
		/// <param name="min">Min value in range.</param>
		/// <param name="max">Max value in range.</param>
		public RangeFloatSerialized (float min, float max)
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
		/// <param name="value">Value to evaluate.</param>
		/// <returns>Whether a value is in range.</returns>
		public bool InRange (float value) => value.InRange (m_min, m_max);

		/// <summary>
		/// Returns a random number between [Min, Max].
		/// </summary>
		/// <returns> Random number. </returns>
		public float GetRandom () => Random.Range (m_min, m_max);

		/// <summary>
		/// Returns an int version of the range.
		/// </summary>
		/// <param name="roundTypeMin"> Round Type for minimum value. </param>
		/// <param name="roundTypeMax"> Round Type for maximum value. </param>
		/// <returns> RangeIntSerialized version of the range. </returns>
		public RangeIntSerialized ToRangeIntSerialized (
			RoundType roundTypeMin = RoundType.Round,
			RoundType roundTypeMax = RoundType.Round
		)
		{
			return new RangeIntSerialized (
				min: m_min.RoundToInt (roundTypeMin),
				max: m_max.RoundToInt (roundTypeMax)
			);
		}

		#endregion
	}
}