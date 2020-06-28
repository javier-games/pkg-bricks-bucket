using System;
using UnityEngine;

namespace BricksBucket.Core
{
	// ReSharper disable CommentTypo
	/// <!-- TagAttribute -->
	///
	/// <summary>
	///
	/// <para>
	/// Shows an int field as LayerMask.
	/// </para>
	/// 
	/// <para>
	/// Based in the <see href="https://github.com/Deadcows/MyBox">MyBox
	/// project by @deadcows</see> and the original version by <see creh="
	/// https://github.com/Kaynn-Cahya">@Kaynn-Cahya</see>.
	/// </para>
	///
	/// </summary>
	///
	/// <seealso href="https://github.com/Deadcows/MyBox">
	/// Deadcows/MyBox</seealso>
	/// <seealso href="https://github.com/Kaynn-Cahya">
	/// @Kaynn-Cahya</seealso>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	// ReSharper restore CommentTypo
	[AttributeUsage (AttributeTargets.Field)]
	public class TagAttribute : PropertyAttribute { }
}