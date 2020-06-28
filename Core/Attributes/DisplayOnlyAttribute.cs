using System;
using UnityEngine;

namespace BricksBucket.Core
{
	// ReSharper disable CommentTypo
	/// <!-- DisplayOnlyAttribute -->
	///
	/// <summary>
	///
	/// <para>
	/// Constrins the value to be positive only.
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
	public class DisplayOnlyAttribute : PropertyAttribute { }
}