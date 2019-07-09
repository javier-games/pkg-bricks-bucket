#if BRICKSBUCKET_TMPRO
using TMPro;

namespace BricksBucket.Utils
{
    public static class TextMeshProUtils
    {
        /// <summary> Updates TMP_Text Component Text. </summary>
        /// <param name="label"></param>
        /// <param name="text"></param>
        public static void UpdateText (this TMP_Text label, params string[] text)
        {
            label.text = StringUtils.Concat (text);
        }
    }
}
#endif