#if BRICKSBUCKET_GOOGLE
using System;
using System.Threading;
using System.Collections.Generic;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Apis.Services;

using BricksBucket.Utils;

namespace BricksBucket.Google
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
            using (var stream = OAuth.ToStream())
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

    }
}
#endif