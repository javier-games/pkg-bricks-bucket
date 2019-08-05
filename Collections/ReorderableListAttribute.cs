using System;
using UnityEngine;
using BricksBucket.Collections;

namespace BricksBucket
{
    /// <summary>
    /// 
    /// Reorderable List Attribute.
    /// 
    /// <para>
    /// Attribute to show list as reorderable.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the UnityExtensions.ArrayDrawer by @garettbass.
    /// https://github.com/garettbass/UnityExtensions.ArrayDrawer
    /// </para>
    /// 
    /// </summary>
    [AttributeUsage (AttributeTargets.Field)]
    public class ReorderableListAttribute : PropertyAttribute
    {
        #region Class Members

        //  Consts to parse list.
        public const string SingularPluralBlockBegin = "{{";
        public const string SingularPluralBlockSeparator = "|";
        public const string SingularPluralBlockEnd = "}}";

        public float r, g, b;               //  Color.
        public bool disableAdding;          //  Flag to disable Add button.
        public bool disableRemoving;        //  Flag to disable Remove button.
        public bool disableDragging;        //  Flag to disable Drag and Drop.
        public bool elementsAreSubassets;   //  Flag to show better subassets.
        public bool hideFooterButtons;      //  Flag to disable footer buttons.
        public string elementHeaderFormat;  //  Formatt of header.
        public string listHeaderFormat;     //  Format of List header.
        public string[] parallelListNames;  //  List of titles.
        public ParallelListLayout parallelListLayout;   // List layout.

        #endregion



        #region Constructor

        /// <summary> Constructor. </summary>
        public ReorderableListAttribute () { }

        /// <summary> Constructor with list titles. </summary>
        /// <param name="parallelListNames"></param>
        public ReorderableListAttribute (params string[] parallelListNames)
        {
            this.parallelListNames = parallelListNames;
        }

        #endregion



        #region Accesors

        /// <summary> Modifies disable adding and removing at once. </summary>
        public bool DisableAddingAndRemoving
        {
            get { return disableAdding && disableRemoving; }
            set { disableAdding = disableRemoving = value; }
        }

        /// <summary> Returns header format for singular list. </summary>
        public string SingularListHeaderFormat
        {
            get
            {
                if (listHeaderFormat == null)
                    return null;
                var value = listHeaderFormat;
                while (value.Contains (SingularPluralBlockBegin))
                {
                    int begin = value.IndexOf (
                        value: SingularPluralBlockBegin,
                        comparisonType: StringComparison.Ordinal
                    );

                    int end = value.IndexOf (
                        value: SingularPluralBlockEnd,
                        startIndex: begin,
                        comparisonType: StringComparison.Ordinal
                    );

                    if (end < 0)
                        break;

                    end += SingularPluralBlockEnd.Length;
                    int blockLenght = end - begin;
                    var block = value.Substring (begin, blockLenght);
                    int separation = value.IndexOf (
                        value: SingularPluralBlockSeparator,
                        startIndex: begin,
                        comparisonType: StringComparison.Ordinal
                    );

                    if (separation < 0)
                        value = value.Replace (block, string.Empty);
                    else
                    {
                        begin += SingularPluralBlockBegin.Length;
                        int singularLenght = separation - begin;
                        var singular = value.Substring (begin, singularLenght);
                        value = value.Replace (block, singular);
                    }
                }
                return value;
            }
        }

        /// <summary> Returns header format for plural list. </summary>
        public string PluralListHeaderFormat
        {
            get
            {
                if (listHeaderFormat == null)
                    return null;

                var value = listHeaderFormat;
                while (value.Contains (SingularPluralBlockBegin))
                {
                    int begin = value.IndexOf (
                        value: SingularPluralBlockBegin,
                        comparisonType: StringComparison.Ordinal
                    );

                    int end = value.IndexOf (
                        value: SingularPluralBlockEnd,
                        startIndex: begin,
                        comparisonType: StringComparison.Ordinal
                    );

                    if (end < 0)
                        break;

                    end += SingularPluralBlockEnd.Length;
                    int blockLenght = end - begin;
                    var block = value.Substring (begin, blockLenght);
                    int separation = value.IndexOf (
                        value: SingularPluralBlockSeparator,
                        startIndex: begin,
                        comparisonType: StringComparison.Ordinal
                    );

                    if (separation < 0)
                    {
                        begin += SingularPluralBlockBegin.Length;
                        end -= SingularPluralBlockEnd.Length;
                        int pluralLenght = (end - begin);
                        var plural = value.Substring (begin, pluralLenght);
                        value = value.Replace (block, plural);
                    }
                    else
                    {
                        separation += SingularPluralBlockSeparator.Length;
                        end -= SingularPluralBlockEnd.Length;
                        int pluralLenght = (end - separation);
                        var plural = value.Substring (begin, pluralLenght);
                        value = value.Replace (block, plural);
                    }
                }
                return value;
            }
        }

        #endregion
    }
}
