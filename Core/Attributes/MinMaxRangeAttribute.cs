using System;

namespace BricksBucket.Core
{
	// ReSharper disable CommentTypo
	/// <!-- MinMaxRangeAttribute -->
	///
	/// <summary>
	///
	/// <para>
	/// Defines the max and min value for a number.
	/// </para>
	/// 
	/// <para>
	/// Based in the <see href="https://github.com/Deadcows/MyBox">MyBox
	/// project by @deadcows</see>.
	/// </para>
	///
	/// </summary>
	///
	/// <seealso href="https://github.com/Deadcows/MyBox">
	/// Deadcows/MyBox</seealso>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	// ReSharper restore CommentTypo
	public class MinMaxRangeAttribute : Attribute
	{
		#region Class Members

		/// <summary> Minimum value to use for the property. </summary>
		public readonly float min;

		/// <summary> Maximum value to use for the property. </summary>
		public readonly float max;

		#endregion

        
		#region Constructor

		/// <summary> Creates a new instance of min max attribute. </summary>
		/// <param name="min"> Minimum value to use for the property. </param>
		/// <param name="max"> Maximum value to use for the property. </param>
		public MinMaxRangeAttribute (float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		#endregion
	}
}