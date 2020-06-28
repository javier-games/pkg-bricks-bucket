using System;
using UnityEngine;

namespace BricksBucket.Core
{
	// ReSharper disable CommentTypo
	/// <!-- SearchableEnumAttribute -->
	///
	/// <summary>
	///
	/// <para>
	/// Adds a search menu to the enum dropdown.
	/// </para>
	/// 
	/// <para>
	/// Based in the <see href=
	/// "https://github.com/roboryantron/UnityEditorJunkie">UnityEditorJunkie
	/// project by @roboryantron</see>.
	/// </para>
	///
	/// </summary>
	///
	/// <seealso href="https://github.com/roboryantron/UnityEditorJunkie">
	/// roboryantron/UnityEditorJunkie</seealso>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	// ReSharper restore CommentTypo
	[AttributeUsage (AttributeTargets.Field)]
	public class SearchableEnumAttribute : PropertyAttribute { }
}