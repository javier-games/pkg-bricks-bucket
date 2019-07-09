#if BRICKSBUCKET_GOOGLE
using System;
using System.Threading;
using System.Collections.Generic;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Apis.Services;

using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace BricksBucket.Utils
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



        #region Authentication

        /// <summary>
        /// Get a User Credential.
        /// </summary>
        /// <param name="OAuth"></param>
        /// <param name="path"></param>
        /// <param name="scopes"></param>
        /// <returns></returns>
        public static UserCredential GetCredential (string OAuth, string path, string[] scopes)
        {

            UserCredential credential;
            using (var stream = StringUtils.StreamFromString (OAuth))
            {

                ClientSecrets secrets = GoogleClientSecrets.Load (stream).Secrets;
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync (
                    clientSecrets: secrets,
                    scopes: scopes,
                    user: secrets.ClientId,
                    taskCancellationToken: CancellationToken.None,
                    dataStore: new FileDataStore (path, true)
                ).Result;
            }

            return credential;
        }

        #endregion



        #region Google Sheets Api

        /// <summary>
        /// Return the scope for Spread Sheets.
        /// </summary>
        public static string SheetsServiceScope
        {
            get
            {
                return SheetsService.Scope.SpreadsheetsReadonly;
            }
        }

        /// <summary>
        /// Return the scope for Spread Sheets as an array.
        /// </summary>
        public static string[] SheetsServiceScopes
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

        #endregion
    }
}
#endif