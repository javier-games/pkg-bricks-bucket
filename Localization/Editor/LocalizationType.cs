namespace BricksBucket.Localization.Editor
{
	/// <summary>
	///
	/// Localization Type.
	/// 
	/// <para>
	/// Definition of types for a localization.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	public enum LocalizationType
	{
		/// <summary>
		/// String value.
		/// </summary>
		TEXT = 0,

		/// <summary>
		/// Texture reference.
		/// </summary>
		TEXTURE = 1,

		/// <summary>
		/// Sprite reference..
		/// </summary>
		SPRITE = 2,

		/// <summary>
		/// Audio Clip reference.
		/// </summary>
		AUDIO = 3,

		/// <summary>
		/// Video Clip reference.
		/// </summary>
		VIDEO = 4,

		/// <summary>
		/// Generic System.Object.
		/// </summary>
		OBJECT = 5
	}
}