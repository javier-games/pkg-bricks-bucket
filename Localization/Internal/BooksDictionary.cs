using BricksBucket.Core.Collections;

namespace BricksBucket.Localization.Internal
{
	/// <!-- BooksDictionary -->
	/// 
	/// <summary>
	/// Serializable dictionary of books.
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	internal class BooksDictionary : SerializableDictionary<string, Book> { }
}