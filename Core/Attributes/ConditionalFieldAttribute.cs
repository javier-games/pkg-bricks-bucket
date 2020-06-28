using System;
using UnityEngine;

namespace BricksBucket.Core
{
	// ReSharper disable CommentTypo
	/// <!-- ConditionalFieldAttribute -->
	///
	/// <summary>
	///
	/// <para>
	/// Hides or show the property according to other property.
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
	[AttributeUsage (AttributeTargets.Field)]
	public class ConditionalFieldAttribute : PropertyAttribute
	{
		#region Class Members

		/// <summary> Name of the property to check. </summary>
		public readonly string propertyToCheck;

		/// <summary> Value to compare. </summary>
		public readonly object compareValue;

		/// <summary> Inverts the behaviour. </summary>
		public readonly bool inverse;

		#endregion

        
		#region Constructor

		/// <summary> Creates an instance of this attribute. </summary>
		/// <param name="propertyToCheck"> Property to check. </param>
		/// <param name="compareValue"> Use this value to compare. </param>
		/// <param name="inverse"> Inverse the behaviour. </param>
		public ConditionalFieldAttribute (
			string propertyToCheck,
			object compareValue = null,
			bool inverse = false
		)
		{
			this.propertyToCheck = propertyToCheck;
			this.compareValue = compareValue;
			this.inverse = inverse;
		}

		#endregion
	}
}