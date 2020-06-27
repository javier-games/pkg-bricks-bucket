using UnityEngine;

using Enum = System.Enum;

namespace BricksBucket
{
    /// <summary>
    ///
    /// Math Utils.
    ///
    /// <para>
    /// Usefull math tools.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// 
    /// </summary>
    public static class MathUtils
    {

        #region Class Memebers

        /// <summary> Infinity Value </summary>
        public const float Infinity = Mathf.Infinity;

        #endregion



        #region Generic Methods

        /// <summary>
        /// Swaps the value between references A and B.
        /// </summary>
        /// <typeparam name="T">Type of references.</typeparam>
        /// <param name="a">Reference A.</param>
        /// <param name="b">Reference B.</param>
        public static void Swap<T> (ref T a, ref T b)
        {
            T x = a;
            a = b;
            b = x;
        }

        /// <summary>
        /// Loops the number with the specified increment.
        /// </summary>
        /// <param name="current">Current value to loop.</param>
        /// <param name="from">Lowest value to take.</param>
        /// <param name="to">Highest value to take.</param>
        /// <param name="increment">Increment to apply.</param>
        /// <returns>Next value on the loop.</returns>
        public static int
        Loop (int current, int from, int to, int increment = 1)
        {
            if (from > to)
                Swap (ref from, ref to);

            int range = to - from + 1;

            if (range == 1)
                return from;

            if (current < from || current > to)
                current = from;

            if (increment.Absolute () > range)
                increment %= range;

            current += increment;

            if (current > to)
                return current - range;

            if (current < from)
                return range + current;

            return current;
        }

        /// <summary>
        /// Converts an Enum Constant to its int value.
        /// </summary>
        /// <param name="enum">Enum Constant to convert.</param>
        /// <returns>Returns int value of an Enum constant.</returns>
        public static int GetIntFromEnum (Enum @enum)
        {
            return (int) Enum.Parse (@enum.GetType (), @enum.ToString ());
        }

        #endregion



        #region Int Extensions

        /// <summary>
        /// Absolute Value.
        /// </summary>
        /// <param name="x">Numver to absolute.</param>
        /// <returns>Absolute value.</returns>
        public static int Absolute (this int x)
        {
            return Mathf.Abs (x);
        }

        /// <summary>
        /// Evaluates if value is between min and max.
        /// </summary>
        /// <param name="x">Number to evaluate.</param>
        /// <param name="min">Min value to compare.</param>
        /// <param name="max">Max value to compare.</param>
        /// <returns>Whether value is in range.</returns>
        public static bool InRange (this int x, int min, int max)
        {
            return (x > min && x < max) ||
                (x >= min && x > max) ||
                (x > min && x <= max);
        }

        /// <summary>
        /// Evaluates wether x is between min and max.
        /// </summary>
        /// <param name="x">Number to evaluate.</param>
        /// <param name="range">Range to use to compare.</param>
        /// <returns>Wether x is between min and max.</returns>
        public static bool InRange (this int x, RangeIntSerialized range)
        {
            return x.InRange (range.Min, range.Max);
        }

        /// <summary>
        /// Add as layer to this numbers an int value.
        /// </summary>
        /// <param name="mask">Int value as mask.</param>
        /// <param name="layer">Int value as Layer.</param>
        public static void AddLayerToMask (this ref int mask, Enum layer)
        {
            mask.AddLayerToMask (GetIntFromEnum (layer));
        }

        /// <summary>
        /// Add as layer to this numbers an int value.
        /// </summary>
        /// <param name="mask">Int value as mask.</param>
        /// <param name="layer">Int value as Layer.</param>
        public static void AddLayerToMask (this ref int mask, int layer)
        {
            mask |= 1 << layer;
        }

        /// <summary>
        /// Wether an int value layer is in mask.
        /// </summary>
        /// <param name="mask">Int value as mask.</param>
        /// <param name="layer">Int value as Layer.</param>
        public static bool MaskHasLayer (this int mask, Enum layer)
        {
            return mask.MaskHasLayer(GetIntFromEnum(layer));
        }

        /// <summary>
        /// Wether an int value layer is in mask.
        /// </summary>
        /// <param name="mask">Int value as mask.</param>
        /// <param name="layer">Int value as Layer.</param>
        public static bool MaskHasLayer (this int mask, int layer)
        {
            return (mask & (1 << layer)) > 0;
        }

        /// <summary>
        /// Removes an int layer reference from mask.
        /// </summary>
        /// <param name="mask">Int value as mask.</param>
        /// <param name="layer">Int value as Layer.</param>
        public static void RemoveLayerFromMask (this ref int mask, Enum layer)
        {
            mask.RemoveLayerFromMask (GetIntFromEnum(layer));
        }

        /// <summary>
        /// Removes an int layer reference from mask.
        /// </summary>
        /// <param name="mask">Int value as mask.</param>
        /// <param name="layer">Int value as Layer.</param>
        public static void RemoveLayerFromMask (this ref int mask, int layer)
        {
            if (mask.MaskHasLayer (layer))
                mask &= ~(1 << layer);
        }

        #endregion



        #region Float Extensions

        /// <summary>
        /// Pow to -1.
        /// </summary>
        /// <param name="x">Numver to invert.</param>
        /// <returns>Inverse of the float.</returns>
        public static float Invert (this float x)
        {
            return x.Approximately (0) ? Infinity : Mathf.Pow (x, -1);
        }

        /// <summary>
        /// Absolute Value.
        /// </summary>
        /// <param name="x">Number to absolute.</param>
        /// <returns>Absolute value.</returns>
        public static float Absolute (this float x)
        {
            return Mathf.Abs (x);
        }

        /// <summary>
        /// Return if the value aproximates to zero.
        /// </summary>
        /// <param name="x">Number to validate.</param>
        /// <param name="compare">Value to use as comparation.</param>
        /// <returns>Wether aproximates to zero or not.</returns>
        public static bool Approximately (this float x, float compare)
        {
            return x.CompareTo(compare) == 1;
        }

        /// <summary>
        /// Evaluates if value is between zero and one.
        /// </summary>
        /// <param name="x">Number to validate.</param>
        /// <returns>Whether value is in range.</returns>
        public static bool InRange01 (this float x)
        {
            return InRange (x, 0, 1);
        }

        /// <summary>
        /// Evaluates if value is between min and max.
        /// </summary>
        /// <param name="x">Number to validate.</param>
        /// <param name="min">Minimum value in range.</param>
        /// <param name="max">Maximum value in range.</param>
        /// <returns>Whether value is in range.</returns>
        public static bool InRange (this float x, float min, float max)
        {
            return (x > min && x < max) ||
                (x >= min && x > max) ||
                (x > min && x <= max);
        }

        /// <summary>
        /// Evaluates wether x is between min and max.
        /// </summary>
        /// <param name="x">Number to evaluate.</param>
        /// <param name="range">Range to use to compare.</param>
        /// <returns>Wether x is between min and max.</returns>
        public static bool InRange (this float x, RangeIntSerialized range)
        {
            return x.InRange (range.Min, range.Max);
        }

        /// <summary>
        /// Rounds a float with the specified method.
        /// </summary>
        /// <param name="x">Float to convert.</param>
        /// <param name="roundType">Round method to use.</param>
        /// <returns>Rounded value as int.</returns>
        public static int
        RoundToInt (this float x, RoundType roundType = RoundType.Round)
        {
            switch (roundType)
            {
                case RoundType.Ceil:
                return Mathf.CeilToInt (x);

                case RoundType.Floor:
                return Mathf.FloorToInt (x);

                default:
                return Mathf.RoundToInt (x);
            }
        }

        /// <summary>
        /// Rounds a float with the specified method.
        /// </summary>
        /// <param name="x">Float to convert.</param>
        /// <param name="roundType">Round method to use.</param>
        /// <returns>Rounded value.</returns>
        public static float
        Round (this float x, RoundType roundType = RoundType.Round)
        {
            switch (roundType)
            {
                case RoundType.Ceil:
                return Mathf.Ceil (x);

                case RoundType.Floor:
                return Mathf.Floor (x);

                default:
                return Mathf.Round (x);
            }
        }

        #endregion



        #region Double Extensions

        
        /// <summary>
        /// Pow to -1.
        /// </summary>
        /// <param name="x">Number to invert.</param>
        /// <returns>Inverse of the float.</returns>
        public static double Invert (this double x)
        {
            return x.Approximately (0) ? Infinity : System.Math.Pow (x, -1);
        }

        /// <summary>
        /// Absolute Value.
        /// </summary>
        /// <param name="x">Number to absolute.</param>
        /// <returns>Absolute value.</returns>
        public static double Absolute (this double x)
        {
            return System.Math.Abs (x);
        }

        /// <summary>
        /// Return if the value aproximates to zero.
        /// </summary>
        /// <param name="x">Number to approximate.</param>
        /// <param name="compare">Compare value.</param>
        /// <returns>Wether aproximates to zero or not.</returns>
        public static bool Approximately (this double x, double compare)
        {
            return x.CompareTo(compare) == 1;
        }

        /// <summary>
        /// Evaluates if value is between zero and one.
        /// </summary>
        /// <param name="x">Number to validate.</param>
        /// <returns>Whether value is in range.</returns>
        public static bool InRange01 (this double x)
        {
            return InRange (x, 0, 1);
        }

        /// <summary>
        /// Evaluates if value is between min and max.
        /// </summary>
        /// <param name="x">Number to validate.</param>
        /// <param name="min">Minimum value in range.</param>
        /// <param name="max">Maximum value in range.</param>
        /// <returns>Whether value is in range.</returns>
        public static bool InRange (this double x, double min, double max)
        {
            return (x > min && x < max) ||
                (x >= min && x > max) ||
                (x > min && x <= max);
        }

        /// <summary>
        /// Evaluates wether x is between min and max.
        /// </summary>
        /// <param name="x">Number to evaluate.</param>
        /// <param name="range">Range to use to compare.</param>
        /// <returns>Wether x is between min and max.</returns>
        public static bool InRange (this double x, RangeIntSerialized range)
        {
            return x.InRange (range.Min, range.Max);
        }

        /// <summary>
        /// Rounds a float with the specified method.
        /// </summary>
        /// <param name="x">Float to convert.</param>
        /// <param name="roundType">Round method to use.</param>
        /// <returns>Rounded value as int.</returns>
        public static int
        RoundToInt (this double x, RoundType roundType = RoundType.Round)
        {
            switch (roundType)
            {
                case RoundType.Ceil:
                return (int) System.Math.Ceiling (x);

                case RoundType.Floor:
                return (int) System.Math.Floor (x);

                default:
                return (int) System.Math.Round (x);
            }
        }

        /// <summary>
        /// Rounds a float with the specified method.
        /// </summary>
        /// <param name="x">Float to convert.</param>
        /// <param name="roundType">Round method to use.</param>
        /// <returns>Rounded value.</returns>
        public static double
        Round (this double x, RoundType roundType = RoundType.Round)
        {
            switch (roundType)
            {
                case RoundType.Ceil:
                return System.Math.Ceiling (x);

                case RoundType.Floor:
                return System.Math.Floor (x);

                default:
                return System.Math.Round (x);
            }
        }

        #endregion



        #region Vector2

        /// <summary>
        /// Set the value of X.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="x">New value for X.</param>
        public static void SetX (this ref Vector2Int v, int x)
        {
            v.x = x;
        }

        /// <summary>
        /// Set the value of Y.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="y">New value for Y.</param>
        public static void SetY (this ref Vector2Int v, int y)
        {
            v.y = y;
        }

        /// <summary>
        /// Add value to X.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="x">Value to add at X.</param>
        public static void AddToX (this ref Vector2Int v, int x)
        {
            v.x += x;
        }

        /// <summary>
        /// Add value to y.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="y">Value to add at Y.</param>
        public static void AddToY (this ref Vector2Int v, int y)
        {
            v.y += y;
        }

        /// <summary>
        /// Set the value of X.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="x">New value for X.</param>
        public static void SetX (this ref Vector2 v, float x)
        {
            v.x = x;
        }

        /// <summary>
        /// Set the value of Y.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="y">New value for Y.</param>
        public static void SetY (this ref Vector2 v, float y)
        {
            v.y = y;
        }

        /// <summary>
        /// Add value to X.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="x">Value to add at X.</param>
        public static void AddToX (this ref Vector2 v, float x)
        {
            v.x += x;
        }

        /// <summary>
        /// Add value to y.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="y">Value to add at Y.</param>
        public static void AddToY (this ref Vector2 v, float y)
        {
            v.y += y;
        }

        /// <summary>
        /// Converts to Vector2Int
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="type">Rounding method to use.</param>
        /// <returns>Rouded Vector.</returns>
        public static Vector2Int Vector2Int (this Vector2 v, RoundType type = 0)
        {
            int x = default;
            int y = default;

            switch (type)
            {
                case RoundType.Round:
                x = Mathf.RoundToInt (v.x);
                y = Mathf.RoundToInt (v.y);
                break;
                case RoundType.Ceil:
                x = Mathf.CeilToInt (v.x);
                y = Mathf.CeilToInt (v.y);
                break;
                case RoundType.Floor:
                x = Mathf.FloorToInt (v.x);
                y = Mathf.FloorToInt (v.y);
                break;
            }
            return new Vector2Int (x, y);
        }

        /// <summary>
        /// Returns a promedy of an array of vectors.
        /// </summary>
        /// <param name="collection">Collection of vectors.</param>
        /// <returns>Promedy vector in an array.</returns>
        public static Vector2 Average (this Vector2[] collection)
        {
            var promedy = Vector2.zero;
            var values = collection;
            for (int i = 0; i < values.Length; i++)
                promedy += values[i];

            promedy /= values.Length;

            return promedy;
        }

        #endregion



        #region Vector3

        /// <summary>
        /// Set the value of X.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="x">New value for X.</param>
        public static void SetX (this ref Vector3Int v, int x) => v.x = x;

        /// <summary>
        /// Set the value of Y.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="y">New value for Y.</param>
        public static void SetY (this ref Vector3Int v, int y) => v.y = y;

        /// <summary>
        /// Set the value of Z.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="z">New value for Z.</param>
        public static void SetZ(this ref Vector3Int v, int z) => v.z = z;

        /// <summary>
        /// Set the value of X.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="x">New value for X.</param>
        public static void SetX(this ref Vector3 v, float x) => v.x = x;

        /// <summary>
        /// Set the value of Y.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="y">New value for Y.</param>
        public static void SetY(this ref Vector3 v, float y) => v.y = y;

        /// <summary>
        /// Set the value of Z.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="z">New value for Z.</param>
        public static void SetZ(this ref Vector3 v, float z) => v.z = z;

        /// <summary>
        /// Add value to X.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="x">Value to add at X.</param>
        public static void AddToX(this ref Vector3Int v, int x) => v.x += x;

        /// <summary>
        /// Add value to Y.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="y">Value to add at Y.</param>
        public static void AddToY(this ref Vector3Int v, int y) => v.y += y;

        /// <summary>
        /// Add value to Z.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="z">Value to add at Z.</param>
        public static void AddToZ(this ref Vector3Int v, int z) => v.z += z;

        /// <summary>
        /// Add value to X.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="x">Value to add at X.</param>
        public static void AddToX(this ref Vector3 v, float x) => v.x += x;

        /// <summary>
        /// Add value to Y.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="y">Value to add at Y.</param>
        public static void AddToY(this ref Vector3 v, float y) => v.y += y;

        /// <summary>
        /// Add value to Z.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="z">Value to add at Z.</param>
        public static void AddToZ (this ref Vector3 v, float z) => v.z += z;

        /// <summary>
        /// Converts to Vector3Int.
        /// </summary>
        /// <param name="v">Vector to modify.</param>
        /// <param name="type">Rounding method to use.</param>
        /// <returns>Rouded Vector.</returns>
        public static Vector3Int Vector3Int (this Vector3 v, RoundType type = 0)
        {
            int x = default;
            int y = default;
            int z = default;

            switch (type)
            {
                case RoundType.Round:
                x = Mathf.RoundToInt (v.x);
                y = Mathf.RoundToInt (v.y);
                z = Mathf.RoundToInt (v.z);
                break;
                case RoundType.Ceil:
                x = Mathf.CeilToInt (v.x);
                y = Mathf.CeilToInt (v.y);
                z = Mathf.RoundToInt (v.z);
                break;
                case RoundType.Floor:
                x = Mathf.FloorToInt (v.x);
                y = Mathf.FloorToInt (v.y);
                z = Mathf.RoundToInt (v.z);
                break;
            }
            return new Vector3Int (x, y, z);
        }

        /// <summary>
        /// Returns a promedy of an array of vectors.
        /// </summary>
        /// <param name="collection">Collection of vectors.</param>
        /// <returns>Promedy vector in an array.</returns>
        public static Vector3 Average (this Vector3[] collection)
        {
            var promedy = Vector3.zero;
            var values = collection;
            for (int i = 0; i < values.Length; i++)
                promedy += values[i];

            promedy /= values.Length;

            return promedy;
        }

        #endregion



        #region Transform

        /// <summary>
        /// Set the value of X.
        /// </summary>
        /// <param name="transform">Transform of position to set.</param>
        /// <param name="x">New value for X.</param>
        public static void PositionSetX (this Transform transform, float x)
        {
            Vector3 position = transform.position;
            transform.position = new Vector3 (x, position.y, position.z);
        }

        /// <summary>
        /// Set the value of Y.
        /// </summary>
        /// <param name="transform">Transform of position to set.</param>
        /// <param name="y">New value for Y.</param>
        public static void PositionSetY (this Transform transform, float y)
        {
            Vector3 position = transform.position;
            transform.position = new Vector3 (position.x, y, position.z);
        }

        /// <summary>
        /// Set the value of Z.
        /// </summary>
        /// <param name="transform">Transform of position to set.</param>
        /// <param name="z">New value for Z.</param>
        public static void PositionSetZ (this Transform transform, float z)
        {
            Vector3 position = transform.position;
            transform.position = new Vector3 (position.x, position.y, z);
        }

        /// <summary>
        /// Add value to X.
        /// </summary>
        /// <param name="transform">Transform of position to set.</param>
        /// <param name="x">Value to add at X.</param>
        public static void PositionAddToX (this Transform transform, float x)
        {
            transform.PositionSetX (transform.position.x + x);
        }

        /// <summary>
        /// Add value to Y.
        /// </summary>
        /// <param name="transform">Transform of position to set.</param>
        /// <param name="y">Value to add at Y.</param>
        public static void PositionAddToY (this Transform transform, float y)
        {
            transform.PositionSetY (transform.position.y + y);
        }

        /// <summary>
        /// Add value to Z.
        /// </summary>
        /// <param name="transform">Transform of position to set.</param>
        /// <param name="z">Value to add at Z.</param>
        public static void PositionAddToZ (this Transform transform, float z)
        {
            transform.PositionSetZ (transform.position.z + z);
        }

        #endregion
    }
}
