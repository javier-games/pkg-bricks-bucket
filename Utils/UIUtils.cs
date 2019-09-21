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
        /// <param name="label">Updates a label.</param>
        /// <param name="text">Text to add to the labels.</param>
        public static void UpdateText(this Text label, params string[] text) =>
            label.text = StringUtils.Concat(text);

        /// <summary> Sets the image alpha. </summary>
        /// <param name="image">Sets the alpha of an Image.</param>
        /// <param name="alpha">Value of the new alpha.</param>
        public static void SetAlpha(this Image image, float alpha) =>
            image.color.SetAlpha(alpha);

        /// <summary> Activate alpha, interactibity and rays. </summary>
        /// <param name="canvas">Canvas to modify.</param>
        /// <param name="active">Wether to active the canvas group.</param>
        public static void ActivateAll (this CanvasGroup canvas, bool active)
        {
            canvas.alpha = active ? 1 : 0;
            canvas.interactable = active;
            canvas.blocksRaycasts = active;
        }
    }
}
