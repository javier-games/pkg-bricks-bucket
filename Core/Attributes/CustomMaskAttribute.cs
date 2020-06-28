using System;
using UnityEngine;

namespace BricksBucket.Core
{
	// ReSharper disable CommentTypo
	/// <!-- CustomMaskAttribute -->
	///
	/// <summary>
	///
	/// <para>
	/// Shows an int field as a mask for enums.
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
	public class CustomMaskAttribute : PropertyAttribute
	{
		#region Class Members

		/// <summary> Reference to the type of enum. </summary>
		public readonly Type enumType;

		#endregion

        
		#region Constructor

		/// <summary> Creates a new instance of attribute. </summary>
		/// <param name="enumType"> Type of enum. </param>
		public CustomMaskAttribute (Type enumType)
		{
			this.enumType = enumType;
		}

		#endregion
	}
}