using System.Collections;
using BricksBucket.Localization.Internal;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
using UnityEngine.Networking;

//- ReSharper disable NotAccessedField.Local
//- ReSharper disable InconsistentNaming
namespace BricksBucket.Localization.Editor
{
	internal static class LocalizationsImporter
	{
		
		
		
		
		
		



		private static TextGroup GetTextGroup (this string[][] array2D)
		{
			return null;
		}



		public static void ImportFromGoogleSheet (
			this LocalizationsWindow owner, string token, string spreadsheet,
			string sheet, System.Action<TextGroup> callback
		)
		{
			var url =
				string.Format (GoogleSheetsUrl, token, spreadsheet, sheet);

			EditorCoroutineUtility.StartCoroutine (
				Get2DArrayFromSheet (
					url,
					array2D => callback?.Invoke (array2D.GetTextGroup ())
				),
				owner
			);
		}


		private const string GoogleSheetsUrl =
			"https://sheets.googleapis.com/v4" +
			"/spreadsheets/{0}/values/{1}!A1:ZZZ99999999?key={2}";
		
		private static IEnumerator Get2DArrayFromSheet (
			string url, System.Action<string[][]> callback
		)
		{
			using (var request = UnityWebRequest.Get (url))
			{
				if(callback == null) yield break;
				request.SendWebRequest ();
				while (!request.isDone) yield return null;
				if (request.isHttpError || request.isNetworkError)
					callback.Invoke (null);
				
				string text = request.downloadHandler.text
					.Replace ("[", "{ \"" + "array" + "\"" + ": [")
					.Replace ("]", "]}")
					.Replace ("\"values\": { \"array\": [", "\"values\" : [");
				if(text.Length > 0)
					text = text.Remove (text.Length - 2);
				
				var sheetData = JsonUtility.FromJson<SheetData> (text);
				if (sheetData == null)
				{
					callback.Invoke (null);
					yield break;
				}
				callback.Invoke (sheetData.ToArray2D ());
			}
		}
		
		[System.Serializable]
		private class SheetData
		{
			// ReSharper disable once InconsistentNaming
			[SerializeField]
			private string range;
			
			// ReSharper disable once InconsistentNaming
			[SerializeField]
			private string majorDimension;
			
			// ReSharper disable once InconsistentNaming
			[SerializeField]
			private StringArrayWrapper[] values;
			
			[System.Serializable]
			private class StringArrayWrapper
			{
				// ReSharper disable once InconsistentNaming
				public string[] array;
			}

			public string[][] ToArray2D ()
			{
				string[][] array2d = new string[values.Length][];
				for (int i = 0; i < values.Length; i++)
				{
					array2d[i] = new string[values[i].array.Length];
					for (int j = 0; j < values[i].array.Length; j++)
					{
						array2d[i][j] = values[i].array[j];
					}
				}
				return array2d;
			}
		}
	}
}