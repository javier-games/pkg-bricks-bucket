using UnityEngine;

namespace BricksBucket
{
    /// <summary>
    ///
    /// Unity Utils.
    ///
    /// <para>
    /// Usefull general unity tools.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// </summary>
    public static class UnityUtils
    {


        #region Color

        /// <summary> Add Brightness to color. </summary>
        /// <param name="color"></param>
        /// <param name="brightness"></param>
        /// <returns> Brightness color. </returns>
        public static Color Brightness (this Color color, float brightness)
        {
            return new Color (
                color.r + brightness,
                color.g + brightness,
                color.b + brightness,
                color.a
            );
        }

        /// <summary> Convert string HEX color to Color. </summary>
        /// <param name="stringToConvert"></param>
        /// <returns> Color. </returns>
        public static Color ToColor (this string stringToConvert)
        {
            ColorUtility.TryParseHtmlString (stringToConvert, out Color color);
            return color;
        }

        /// <summary> Convert Color to HEX string color.  </summary>
        /// <param name="color"></param>
        /// <returns> HEX code of a color. </returns>
        public static string HEX (this Color color)
        {
            return StringUtils.Concat ("#", ColorUtility.ToHtmlStringRGBA (color));
        }

        #endregion



        #region Texture

        /// <summary> Creates a new sprite form texture. </summary>
        /// <param name="texture"></param>
        /// <returns> New Sprite. </returns>
        public static Sprite ToSprite (this Texture2D texture)
        {
            var rect = new Rect (0, 0, texture.width, texture.height);
            var pivot = Vector2.one * 0.5f;
            return Sprite.Create (texture, rect, pivot);
        }

        /// <summary> Generates a new sample texture resized. </summary>
        /// <param name="source"></param>
        /// <param name="targetWidth"></param>
        /// <param name="targetHeight"></param>
        /// <returns> New texture resized. </returns>
        public static Texture2D GetSample (this Texture2D source, int targetWidth, int targetHeight)
        {
            var width = source.width;
            var height = source.height;
            var aspect = (float) width / height;
            var targetAspect = (float) targetWidth / targetHeight;

            //  Determinates the factor and offset by aspect ratio.
            float factor;
            var offset = Vector2.zero;
            if (aspect > targetAspect)
            {
                // Crop width.
                factor = (float) targetHeight / height;
                offset.x = (int) ((width - height * targetAspect) * 0.5f);
            }
            else
            {
                // Crop height.
                factor = (float) targetWidth / width;
                offset.y = (int) ((height - width / targetAspect) * 0.5f);
            }

            //  Convertion.
            var data = source.GetPixels32 ();
            var targetData = new Color32[targetWidth * targetHeight];
            for (int y = 0; y < targetHeight; y++)
            {
                for (int x = 0; x < targetWidth; x++)
                {
                    var p = new Vector2 (
                        x: Mathf.Clamp (offset.x + x / factor, 0, width - 1),
                        y: Mathf.Clamp (offset.y + y / factor, 0, height - 1)
                    );

                    Vector2Int floorToInt = new Vector2Int (
                        x: Mathf.FloorToInt (p.x),
                        y: Mathf.FloorToInt (p.y)
                    );
                    Vector2Int ceilToInt = new Vector2Int (
                        x: Mathf.CeilToInt (p.x),
                        y: Mathf.CeilToInt (p.y)
                    );
                    // Bilinear filtering.
                    var c11 = data[floorToInt.x + width * floorToInt.y];
                    var c12 = data[floorToInt.x + width * ceilToInt.y];
                    var c21 = data[ceilToInt.x + width * floorToInt.y];
                    var c22 = data[ceilToInt.x + width * ceilToInt.y];

                    targetData[x + y * targetWidth] = Color.Lerp (
                        a: Color.Lerp (c11, c12, p.y),
                        b: Color.Lerp (c21, c22, p.y),
                        t: p.x
                    );
                }
            }

            //  Configure the new texture.
            var texture = new Texture2D (targetWidth, targetHeight);
            texture.SetPixels32 (targetData);
            texture.Apply (true);
            return texture;
        }

        /// <summary> Crops the current texture. </summary>
        /// <param name="original"></param>
        /// <param name="offset"></param>
        /// <returns> Crop the current text. </returns>
        public static Texture2D Crop (this Texture2D original, RectOffset offset)
        {
            int x = offset.left + offset.right;
            int y = offset.top + offset.bottom;
            int diffWidth = original.width - x;
            int diffHeight = original.height - y;

            var pixels = original.GetPixels (
                x: offset.left,
                y: offset.bottom,
                blockWidth: diffWidth,
                blockHeight: diffHeight
            );

            Texture2D result = new Texture2D (
                width: diffWidth,
                height: diffHeight,
                textureFormat: TextureFormat.RGB24,
                mipChain: false
            );
            result.SetPixels (pixels);
            result.Apply ();

            return result;
        }

        #endregion
    }
}
