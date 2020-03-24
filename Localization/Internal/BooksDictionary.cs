using BricksBucket.Collections;

namespace BricksBucket.Localization.Internal
{
	/// <summary>
	/// 
	/// Books Dictionary.
	///
	/// <para>
	/// Serializable dictionary of books.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class BooksDictionary : SerializableDictionary<string, Book> { }
}