#if BRICKSBUCKET_GOOGLE && BRICKSBUCKET_GOOGLE_SHEETS
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using BricksBucket.Google;

namespace BricksBucket.Google.Sheets
{
    /// <summary>
    /// 
    /// GoogleUtils.
    /// 
    /// <para>
    /// Usefull utilities to work with Google Apis. | The Jun 24th, 2019
    /// version contains:
    /// 
    /// 1.- Authetication.
    /// 2.- Google Sheets Api.
    /// 
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// </summary>
    public static class GoogleUtils
    {

        /// <summary>
        /// Return the scope for Spread Sheets.
        /// </summary>
        public static string ServiceScope
        {
            get
            {
                return SheetsService.Scope.SpreadsheetsReadonly;
            }
        }

        /// <summary>
        /// Return the scope for Spread Sheets as an array.
        /// </summary>
        public static string[] ServiceScopes
        {
            get
            {
                string[] scopes = {
                SheetsService.Scope.SpreadsheetsReadonly
            };
                return scopes;
            }
        }

        /// <summary>
        /// Returns the Spread Sheet vía callback as ValueRange.
        /// </summary>
        /// <param name="credential"></param>
        /// <param name="sheetID"></param>
        /// <param name="range"></param>
        /// <param name="callback"></param>
        public static void GetSheet (UserCredential credential, string sheetID, string range, Action<ValueRange> callback)
        {

            ValueRange valueRange = GetSheet (
                credential: credential,
                sheetID: sheetID,
                range: range
            );

            if (callback != null)
                callback.Invoke (valueRange);
        }

        /// <summary>
        /// Returns the Spread Sheet vía callback as IList.
        /// </summary>
        /// <param name="credential"></param>
        /// <param name="sheetID"></param>
        /// <param name="range"></param>
        /// <param name="callback"></param>
        public static void GetSheet (UserCredential credential, string sheetID, string range, Action<IList<IList<object>>> callback)
        {

            ValueRange valueRange = GetSheet (
                credential: credential,
                sheetID: sheetID,
                range: range
            );

            if (callback != null)
                callback.Invoke (valueRange.Values);
        }

        /// <summary>
        /// Returns the Spread Sheet as ValueRange.
        /// </summary>
        /// <param name="credential"></param>
        /// <param name="sheetID"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static ValueRange GetSheet (UserCredential credential, string sheetID, string range)
        {

            SheetsService service = new SheetsService (
                new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential
                }
            );

            SpreadsheetsResource resource;
            SpreadsheetsResource.ValuesResource values;
            SpreadsheetsResource.ValuesResource.GetRequest request;

            resource = service.Spreadsheets;
            values = resource.Values;
            request = values.Get (sheetID, range);

            try
            {
                return request.Execute ();
            }
            catch (Exception e)
            {
                DebugUtils.LogDevException (e);
                return null;
            }
        }
    }
}
#endif