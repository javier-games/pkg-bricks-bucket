using System.Collections.Generic;
using UnityEngine;

namespace BricksBucket.Localization
{
	[System.Serializable]
	public struct BookSettings
	{
		[SerializeField]
		private List<Book> _books;
	}
}