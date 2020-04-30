using System.Collections;
using BricksBucket.Localization.Internal;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
using UnityEngine.Networking;

// ReSharper disable NotAccessedField.Local
namespace BricksBucket.Localization.Editor
{
	internal static class LocalizationsImporter
	{
		
		#region General
		
		private static TextGroup GetTextGroup (this string[][] array2D)
		{
			if (array2D == null) return null;

			if (array2D.Length <= 1) return null;

			//	Looking for the amount of valid cultures into the array.
			//	A valid culture is the one that is different from 0.
			var validCulturesCount = 0;
			for (int i = 1; i < array2D[0].Length; i++)
			{
				if (string.IsNullOrWhiteSpace (array2D[0][i])) break;
				validCulturesCount = i;
			}

			if (validCulturesCount == 0) return null;

			//	Validating cultures.
			var settings = LocalizationSettings.Instance;
			for (int i = 1; i < validCulturesCount; i++)
			{
				var culture = new Culture (array2D[0][i]);
				if (!settings.ContainsCulture (culture.Code))
					settings.AddCulture (culture);
				array2D[0][i] = culture.Code;
			}

			//	Looking for the amount of valid row of localizations.
			//	A valid row is the one that has at least one localization
			//	in it.
			var validRowsAmount = 0;
			for (int i = 1; i < array2D.Length; i++)
				if (array2D[i].Length <= 1)
				{
					validRowsAmount = i - 1;
					break;
				}

			if (validRowsAmount == 0) return null;

			//	Creating a group of texts with the localizations.
			var group = new TextGroup ();
			for (int i = 1; i <= validRowsAmount; i++)
			{
				var localizedText = new LocalizedText ();
				for (int j = 1; j <= validCulturesCount; j++)
					localizedText[array2D[0][j]] = j >= array2D[i].Length
						? string.Empty
						: string.IsNullOrEmpty (array2D[i][j])
							? string.Empty
							: array2D[i][j];

				group.Add (array2D[i][0], localizedText);
			}

			return group;
		}

		#endregion
		
		
		#region Google Sheet Importer
		
		public static void ImportFromGoogleSheet (
			this LocalizationsWindow owner, string token, string spreadsheet,
			string sheet, System.Action<TextGroup> callback
		)
		{
			const string googleSheetsUrl =
				"https://sheets.googleapis.com/v4" +
				"/spreadsheets/{0}/values/{1}!A1:ZZZ99999999?key={2}";
			
			var url =
				string.Format (googleSheetsUrl, spreadsheet, sheet, token);

			EditorCoroutineUtility.StartCoroutine (
				Get2DArrayFromSheet (
					url,
					array2D => callback?.Invoke (array2D.GetTextGroup ())
				),
				owner
			);
		}
		
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
				{
					Debug.LogError (request.error);
					callback.Invoke (null);
				}

				string text = request.downloadHandler.text
					.Replace ("[", "{ \"" + "array" + "\"" + ": [")
					.Replace ("]", "]}")
					.Replace ("\"values\": { \"array\": [", "\"values\" : [");
				if(text.Length > 0)
					text = text.Remove (text.Length - 2);
				
				SheetData sheetData;
				try
				{
					sheetData = JsonUtility.FromJson<SheetData> (text);
				}
				catch (System.Exception e)
				{
					Debug.LogError (text);
					Debug.LogException (e);
					yield break;
				}
				 
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
			#pragma warning disable CS0649
			// ReSharper disable InconsistentNaming
			
			[SerializeField]
			private string range;
			
			[SerializeField]
			private string majorDimension;
			
			[SerializeField]
			private StringArrayWrapper[] values;
			
			[System.Serializable]
			private class StringArrayWrapper
			{
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
			
			// ReSharper restore InconsistentNaming
			#pragma warning restore CS0649
		}
		
		#endregion
	}
}