using UnityEngine;
using UnityEngine.UI;

namespace BricksBucket
{
    /// <summary>
    ///
    /// UI Utils.
    ///
    /// <para>
    /// Usefull tools for UI.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// </summary>
    public static class UIUtils
    {
        /// <summary> Updates Unity Text Component Text. </summary>
        /// <param name="label"></param>
        /// <param name="text"></param>
        public static void UpdateText (this Text label, params string[] text)
        {
            label.text = StringUtils.Concat (text);
        }

        /// <summary> Sets the image alpha. </summary>
        /// <param name="image"></param>
        /// <param name="alpha"></param>
        public static void SetAlpha (this Image image, float alpha)
        {
            var color = image.color;
            color = new Color (color.r, color.g, color.b, alpha);
            image.color = color;
        }

        /// <summary> Activate alpha, interactibity and rays. </summary>
        /// <param name="canvas"></param>
        /// <param name="active"></param>
        public static void ActivateAll (this CanvasGroup canvas, bool active)
        {
            canvas.alpha = active ? 1 : 0;
            canvas.interactable = active;
            canvas.blocksRaycasts = active;
        }
    }
}
