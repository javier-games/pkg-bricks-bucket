using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.UI {

    /// <summary>
    /// 
    /// iOS Home Button.
    /// 
    /// <para>
    /// Helps to modify the iOS home button parameters for each new scene or
    /// canvas on awake, you may consider add just one instance per scene.
    /// </para>
    /// 
    /// <para> By Javier García, 2019. </para>
    /// </summary>
    [RequireComponent (typeof (Canvas))]
    public class IOSHomeButton: MonoBehaviour {



        #region Class Members

        [SerializeField]
        private bool _hideHomeButton;       //  Flag to hide home button.

        [SerializeField]
        private bool _attenuateHomeButton;  //  Flag to attenuate home button.

        [SerializeField]
        private bool _usePadding;           //  Flag to create a padding.

        [SerializeField]
        private Sprite _bezelSprite;        //  Bezel Sprite for padding.

        //  References for Padding, Background and Bazel here created.
        private GameObject _padding, _background, _bezel;

        #endregion



        #region Static Accessors

        public static IOSHomeButton Instance { get; private set; }

        /// <summary> Gets or sets a value indicating whether HomeButton
        /// is hide. </summary>
        /// <value><c>true</c> if hide; otherwise, <c>false</c>.</value>
        public static bool Hide {

            #if UNITY_IOS
            get {
                return UnityEngine.iOS.Device.hideHomeButton;
            }
            set {
                UnityEngine.iOS.Device.hideHomeButton = value;
                if (Instance)
                    Instance._hideHomeButton = value;
            }

            #else
            get { return true; }
            //  Warns about property or indexer setters and event adders or
            //  removers that do not use the value parameter
            #pragma warning disable RECS0029
            set {
            #pragma warning restore RECS0029
            }
            #endif
        }

        /// <summary> Gets or sets a value indicating whether Home Button
        /// is attenuate. </summary>
        /// <value><c>true</c> if attenuate; otherwise, <c>false</c>.</value>
        public static bool Attenuate {

            #if UNITY_IOS
            get {
                return
                    UnityEngine.iOS.Device.deferSystemGesturesMode !=
                    UnityEngine.iOS.SystemGestureDeferMode.None;
            }
            set {
                UnityEngine.iOS.Device.deferSystemGesturesMode = value ?
                UnityEngine.iOS.SystemGestureDeferMode.BottomEdge :
                UnityEngine.iOS.SystemGestureDeferMode.None;
                if (Instance)
                    Instance._attenuateHomeButton = value;
            }

            #else
            get { return true; }
            //  Warns about property or indexer setters and event adders or
            //  removers that do not use the value parameter
            #pragma warning disable RECS0029
            set {
            #pragma warning restore RECS0029
            }
            #endif
        }

        #endregion



        #region Private Accessors
        //  These private accessors are used to avoid read only warning on
        //  visual studio, also you can add #pragma warning disable IDE0044
        //  and  #pragma warning restore RECS0029 before and after the 
        //  declaration of the variable.

        //  Flag to create a padding.
        private bool UsePadding {
            get { return _usePadding; }
            set { _usePadding = value; }
        }

        //  Bezel Sprite for padding.
        private Sprite BezelSprite {
            get { return _bezelSprite; }
            set { _bezelSprite = value; }
        }

        #endregion



        #region MonoBehaviour Overrides

        //  Called on awake.
        private void Awake () {

            Instance = this;

            #if UNITY_IOS

            Hide = _hideHomeButton;
            Attenuate = _attenuateHomeButton;

            if (UsePadding) {

                //  Padding Creation. Container for current canvas content.
                _padding = new GameObject (
                    name: "Padding",
                    components: typeof (RectTransform)
                );
                _padding.transform.SetParent (transform, false);

                RectTransform paddingTransform = _padding.GetComponent<RectTransform> ();
                paddingTransform.anchorMin = Vector2.zero;
                paddingTransform.anchorMax = Vector2.one;
                paddingTransform.offsetMax = Vector2.zero;
                paddingTransform.offsetMin = Vector2.up * 40f;

                List<Transform> childs = new List<Transform> ();
                for (int i = 0; i < transform.childCount; i++)
                    childs.Add (transform.GetChild (i));
                childs.ForEach (t => t.SetParent (_padding.transform, false));


                //  Padding Background. Avoid to show camera background.
                _background = new GameObject (
                    name: "Padding Background",
                    components: typeof (RectTransform)
                );
                _background.transform.SetParent (transform, false);

                RectTransform backgroundTransform = _background.GetComponent<RectTransform> ();
                backgroundTransform.offsetMin = Vector2.zero;
                backgroundTransform.offsetMax = Vector2.zero;
                backgroundTransform.anchorMin = Vector2.zero;
                backgroundTransform.anchorMax = Vector2.right;
                backgroundTransform.sizeDelta = Vector2.up * 40f;
                backgroundTransform.pivot = new Vector2 (0.5f, 1f);

                Image backgroundImage = _background.AddComponent<Image> ();
                backgroundImage.color = Color.black;

                //  Bezel to simulate the bezel iPad.
                _bezel = new GameObject (
                    name: "Bezel",
                    components: typeof (RectTransform)
                );
                _bezel.transform.SetParent (transform, false);

                RectTransform bezelTransform = _bezel.GetComponent<RectTransform> ();
                bezelTransform.anchorMin = Vector2.zero;
                bezelTransform.anchorMax = Vector2.one;
                bezelTransform.offsetMax = Vector2.zero;
                bezelTransform.offsetMin = Vector2.up * 40f;

                Image bezelImage = _bezel.AddComponent<Image> ();
                bezelImage.sprite = BezelSprite;
                bezelImage.color = Color.black;
                bezelImage.type = Image.Type.Sliced;
                bezelImage.raycastTarget = false;

            }

            #endif
        }

        //  Called on destroy. Claen the Instance reference.
        private void OnDestroy () {
            if (Instance == this)
                Instance = null;
        }

        #endregion
    }
}
