using System;
using System.Net;
using System.IO;
using System.Collections;
using System.Runtime.CompilerServices;

namespace UnityEngine.Networking {
    public class FTPDebug : MonoBehaviour {


        #region Class Members

        [Header ("FTP Parameters")]
        [SerializeField]
        private string host = "ftp://files4.hostinger.mx/vikidz/debug/";
        [SerializeField]
        private string user = "u410284381";
        [SerializeField]
        private string pass = "mEQ-46H-enk-UxS";
        [SerializeField]
        private string file = "Test";
        [SerializeField]
        private string ext = ".log";
        [SerializeField]
        private string date = "yy-MM-dd - HH:mm:ss";

        [Header ("Log Parameters")]
        [SerializeField]
        private bool useTimeStamp = true;
        [SerializeField]
        private bool echoToConsole = true;

        private string localPath;
        private string storage;
        private StreamWriter writer;
        private FTPDebugState state;
        public Action<FTPDebugState> stateChanged;

        #endregion


        #region Singleton

        static FTPDebug instance;

        /// <summary> Gets a FTP instance. </summary>
        public static FTPDebug Instance {
            get {

                //  Return the instance
                if (instance != null)
                    return instance;

                //  Otherwise create a temporal gameobject.
                else {
                    GameObject temp = new GameObject (
                        "Temp WebLog",
                        typeof (FTPDebug)
                    );
                    return temp.GetComponent<FTPDebug> ();
                }
            }
        }

        #endregion


        #region MonoBehaviour Events

        //  Called on Awake.
        void Awake () {

            //  Destroy if this is not instance.
            if (instance != null && this != instance)
                Destroy (this.gameObject);

            //  Else assing this as instance.
            else {
                instance = this;
                DontDestroyOnLoad (this.gameObject);

                #if !FINAL

                ChangeState (FTPDebugState.Standby);
                localPath = Application.persistentDataPath + "/" + file + ext;

                if (File.Exists (localPath))
                    UploadLog ();

                #endif
            }
        }

        //  Called before destroy.
        void OnDestroy () {
            StopWriter ();
            stateChanged = null;
        }

        #endregion

        #region Class Implementation

        private void InitializeWriter () {
            #if !FINAL

            StopWriter ();

            bool fileExist = File.Exists (localPath);

            writer = new StreamWriter (localPath, fileExist);
            ChangeState (FTPDebugState.Writing);

            if (fileExist)
                writer.WriteLine ("\n\n");

            writer.WriteLine (String.Concat (
                "Log File\t|\t", string.Format ("{0:D}", DateTime.Now),
                "\t|\t", string.Format ("{0:HH:m:s tt}", DateTime.Now),

                "\n\nReal Time Since Startup: ", Time.realtimeSinceStartup, "s",
                "\n-------------------------------------\n",
                "\nSYSTEM INFO: ",
                "\n\tDevice: ", SystemInfo.deviceName, ".",
                "\n\tDevice Model: ", SystemInfo.deviceModel, ".",
                "\n\tOperating System: ", SystemInfo.operatingSystem, ".",
                "\n\tSystem Memory Size: ", SystemInfo.systemMemorySize, " MB.",
                "\n\tProcessor Type: ", SystemInfo.processorType, ".",
                "\n\tProcessor Count: ", SystemInfo.processorCount, " threads.",
                "\n\tProcessor Frequency: ", SystemInfo.processorFrequency, " MHz.",
                "\n\tGraphics Memory Size: ", SystemInfo.graphicsMemorySize, " MB.",
                "\n\tBattery Status: ", SystemInfo.batteryStatus, ".",
                "\n\tBattery Level: ", SystemInfo.batteryLevel * 100, "%",

                "\n\n-------------------------------------\n",
                "\nAPP INFO: ",
                "\n\tProduct Name: ", Application.productName, ".",
                "\n\tVersion: ", Application.version, ".",
                "\n\tPlatform: ", Application.platform, ".",
                "\n\tGenuine: ", Application.genuine, ".",
                "\n\tUnity Version: ", Application.unityVersion, ".",
                "\n\tiReachability: ", Application.internetReachability, ".",
                "\n\tLocal Log Path: ", Application.persistentDataPath,

                "\n\n-------------------------------------\n\n"
            ));
            
            writer.Flush ();
            WriteStorage ();

            #endif
        }

        private void Write (string message, LogType type = LogType.Log) {
            #if !FINAL

            if (writer == null)
                InitializeWriter ();

            if (useTimeStamp) {
                message = string.Format (
                    "[{0:HH:mm:ss}] {1}\n",
                    DateTime.Now,
                    message
                );
            }

            switch (type) {

                case LogType.Error:
                if (echoToConsole)
                    Debug.LogError (message);
                message = string.Concat ("[Error]\n", message);
                break;

                case LogType.Warning:
                if (echoToConsole)
                    Debug.LogWarning (message);
                message = string.Concat ("[Warning]\n ", message);
                break;

                case LogType.Log:
                if (echoToConsole)
                    Debug.Log (message);
                break;
            }

            if (state != FTPDebugState.Writing) {
                StorageMessage (message);
                return;
            }

            WriteStorage ();
            writer.WriteLine (message);
            writer.Flush ();
            #endif
        }

        private void StorageMessage (string message) {
            #if !FINAL
            storage = string.Concat (storage, message, "\n");
            #endif
        }

        private void WriteStorage () {
            #if !FINAL
            if (writer != null) {
                if (
                    !string.IsNullOrEmpty (storage) &&
                    state == FTPDebugState.Writing
                ) {
                    writer.WriteLine (storage);
                    writer.Flush ();
                    storage = string.Empty;
                }
            }
            else
                InitializeWriter ();
            #endif
        }

        private void StopWriter () {
            #if !FINAL
            if (writer != null) {
                writer.Close ();
                writer = null;
            }
            #endif
        }

        public void UploadLog () {
            #if !FINAL

            if (state == FTPDebugState.Uploading) {
                if (echoToConsole)
                    Debug.LogWarning ("Uploading a file!");
                return;
            }
                
            WriteStorage ();
            Write ("Start Uploading to " + host);
            ChangeState (FTPDebugState.Uploading);
            StopWriter ();
            StartCoroutine (Upload ());
            #endif
        }

        IEnumerator Upload () {

            yield return null;
            #if !FINAL
            WebClient client = new WebClient ();
            string fileDate = string.Format (
                string.Concat("{0} {1:", date ,"}"),
                string.Concat(Application.productName, " ", file, " - "),
                DateTime.Now
            );
            Uri uri = new Uri (host + fileDate + ext);

            client.UploadProgressChanged += UploadProgressChanged;
            client.UploadFileCompleted += UploadCompleted;
            client.Credentials = new NetworkCredential (user, pass);
            client.UploadFileAsync (uri, "STOR", localPath);
            #endif
        }


        void UploadProgressChanged (object sender, UploadProgressChangedEventArgs e) {
            if (echoToConsole)
                Debug.Log ("Uploading Progreess: " + e.ProgressPercentage);
        }

        void UploadCompleted (object sender, UploadFileCompletedEventArgs e) {
            if (echoToConsole)
                Debug.Log ("File Uploaded.");
            File.Delete (localPath);
            ChangeState (FTPDebugState.Standby);
        }

        void ChangeState (FTPDebugState newState) {
            state = newState;
            if (stateChanged != null)
                stateChanged.Invoke (state);
        }

        #endregion




        #region Static Methods

        //[Conditional ("DEBUG"), Conditional ("PROFILE")]
        public static void Log (
            string message,
            [CallerMemberName] string method = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            LogType type = LogType.Log
        ) {
            #if !FINAL

            FTPDebug refFTP = Instance;
            if (refFTP != null) {

                string path = string.Empty;

                if (!string.IsNullOrEmpty (filePath)) {
                    string[] words = filePath.Split ('/');
                    path = words[words.Length - 1];
                }

                if (!string.IsNullOrEmpty (method))
                    path = string.Concat (path, "\\", method);

                if (lineNumber != 0)
                    path = string.Concat (path, ":", lineNumber.ToString ());

                if (!string.IsNullOrEmpty (path))
                    message = string.Concat (message, "\n", path);

                //  Writing the message.
                refFTP.Write (message, type);
            }
            #endif
        }

        //[Conditional ("DEBUG"), Conditional ("PROFILE")]
        public static void LogError (
            string message,
            [CallerMemberName] string method = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0
        ) {
            #if !FINAL
            Log (message, method, filePath, lineNumber, LogType.Error);
            #endif
        }

        //[Conditional ("DEBUG"), Conditional ("PROFILE")]
        public static void LogWarning (
            string message,
            [CallerMemberName] string method = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0
        ) {
            #if !FINAL
            Log (message, method, filePath, lineNumber, LogType.Warning);
            #endif
        }

        #endregion


        #region Nested

        public enum FTPDebugState {
            Standby,
            Uploading,
            Writing
        }

        public enum LogType {
            Log,
            Warning,
            Error
        }
        
        #endregion
    }
}