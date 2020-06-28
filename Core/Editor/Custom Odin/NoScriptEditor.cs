#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;

namespace BricksBucket
{
	/// <summary>
	///
	/// No Script Editor.
	///
	/// <para> Custom editor with out showing script. </para>
	///
	/// <para> By Javier García | @jvrgms | 2019 </para>
	///
	/// </summary>
	public class NoScriptEditor : OdinEditor
	{
		public override void OnInspectorGUI()
		{
			ForceHideMonoScriptInEditor = true;
			base.OnInspectorGUI();
		}
	}
}
#endif
