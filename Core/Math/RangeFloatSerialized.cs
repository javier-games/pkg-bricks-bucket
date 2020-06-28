using BricksBucket.Core;
using UnityEngine;

namespace BricksBucket
{
	/// <summary>
	///
	/// Range Float Serialized.
	///
	/// <para>
	/// Serialized Range for floating point values.
	/// </para>
	///
	/// <para> By Javier García | @jvrgms | 2019 </para>
	/// </summary>
	[System.Serializable]
	public struct RangeFloatSerialized
	{
		#region Class Members

		/// <summary> Min value in range. </summary>
		public float Min;

		/// <summary> Max value in range. </summary>
		public float Max;

		#endregion



		#region Accessors

		/// <summary> Lenght of the range. </summary>
		public float Lenght { get { return Max - Min; } }

		#endregion



		#region Constructor

		/// <summary>
		/// Creates a new struct with min and max values.
		/// </summary>
		/// <param name="min">Min value in range.</param>
		/// <param name="max">Max value in range.</param>
		public RangeFloatSerialized (float min, float max)
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
		/// <param name="value">Value to evaluate.</param>
		/// <returns>Wether a value is in range.</returns>
		public bool InRange (float value)
		{
			return value.InRange (Min, Max);
		}

		/// <summary>
		/// Returns a random number between [Min, Max].
		/// </summary>
		/// <returns> Random number. </returns>
		public float GetRandom ()
		{
			return Random.Range (Min, Max);
		}

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
				min: Min.RoundToInt (roundTypeMin),
				max: Max.RoundToInt (roundTypeMax)
			);
		}

		#endregion
	}
}