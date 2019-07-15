using UnityEngine;

namespace BricksBucket.Utils
{
    public static class MathUtils
    {

        #region Class Memebers

        /// <summary> Infinity Value </summary>
        public const float Infinity = Mathf.Infinity;

        #endregion


        public static void Swap<T> (ref T a, ref T b)
        {
            T x = a;
            a = b;
            b = x;
        }


        #region Float Extensions

        /// <summary> Pow to -1. </summary>
        /// <param name="x"></param>
        /// <returns> Inverse of the float. </returns>
        public static float Invert (this float x)
        {
            return x.Approximately(0) ? Infinity : Mathf.Pow (x, -1);
        }

        /// <summary> Absolute Value. </summary>
        /// <param name="x"></param>
        /// <returns> Absolute value. </returns>
        public static float Absolute (this float x)
        {
            return Mathf.Abs (x);
        }

        /// <summary> Return if the value aproximates to zero. </summary>
        /// <param name="x"></param>
        /// <param name="compare"></param>
        /// <returns> Wether aproximates to zero or not. </returns>
        public static bool Approximately (this float x, float compare)
        {
            return Mathf.Approximately (x, compare);
        }

        /// <summary> Evaluates if value is between zero and one. </summary>
        /// <param name="x"></param>
        /// <returns> Whether value is in range. </returns>
        public static bool InRange01 (this float x)
        {
            return InRange (x, 0, 1);
        }

        /// <summary> Evaluates if value is between min and max. </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns> Whether value is in range. </returns>
        public static bool InRange (this float x, float min, float max)
        {
            return x >= min && x < max;
        }

        #endregion



        #region Activation Functions

        /// <summary> Sign of x. </summary>
        /// <param name="x"></param>
        /// <returns> Evaluation: 1, 0, -1 </returns>
        public static float Sign (float x)
        {
            return x > 0 ? 1 : x < 0 ? -1 : 0;
        }

        /// <summary> Binary Step of x. </summary>
        /// <param name="x"></param>
        /// <returns> Evaluation. </returns>
        public static int BinaryStep(float x)
        {
            return x < 0 ? 0 : 1;
        }

        /// <summary> Sigmoid of x. </summary>
        /// <param name="x"></param>
        /// <returns>  Evaluation.  </returns>
        public static float Sigmoid(float x)
        {
            return Invert (1 + Mathf.Exp(-x));
        }

        /// <summary> Softsign of x. </summary>
        /// <param name="x"></param>
        /// <returns> Evaluation. </returns>
        public static float SoftSign (float x)
        {
            return x * Invert (1 + x.Absolute());
        }

        /// <summary> Inverse Square Root Unit of x. </summary>
        /// <param name="x"></param>
        /// <param name="alpha"></param>
        /// <returns> Evaluation. </returns>
        public static float ISRU (float x, float alpha)
        {
            float sqrt = Mathf.Sqrt (1 + alpha + Mathf.Pow (x, 2));
            return x * Invert (sqrt);
        }

        /// <summary> Inverse Square Root Linear Unit of x.</summary>
        /// <param name="x"></param>
        /// <param name="alpha"></param>
        /// <returns> Evaluation. </returns>
        public static float ISRLU (float x, float alpha)
        {
            return x < 0 ? ISRU (x, alpha) : x;
        }

        /// <summary> Rectified Linear Unit  of x. </summary>
        /// <param name="x"></param>
        /// <returns> Evaluation. </returns>
        public static float ReLU (float x)
        {
            return x > 0 ? x : 0;
        }

        /// <summary> Sinc of x. </summary>
        /// <param name="x"></param>
        /// <returns> Evaluation. </returns>
        public static float Sinc(float x)
        {
            return x.Approximately (0) ? 1 : Sin (x) * Invert (x);
        }

        /// <summary> Gaussian of x. </summary>
        /// <param name="x"></param>
        /// <returns> Evaluation. </returns>
        public static float Gaussian (float x)
        {
            return Mathf.Exp (- Mathf.Pow (x, 2));
        }


        #endregion



        #region Trigonometry Functions

        /// <summary> Returns the sine of x. </summary>
        /// <param name="x"></param>
        /// <returns> Sine value. </returns>
        public static float Sin (float x)
        {
            return Mathf.Sin (x);
        }

        /// <summary> Returns the cosine of x.. </summary>
        /// <param name="x"></param>
        /// <returns> Cosine value. </returns>
        public static float Cos (float x)
        {
            return Mathf.Cos (x);
        }

        /// <summary> Returns the tangent of x. </summary>
        /// <param name="x"></param>
        /// <returns> Tangent value. </returns>
        public static float Tan (float x)
        {
            return Mathf.Tan (x);
        }

        /// <summary> Returns the angle in radians whose sin is x. </summary>
        /// <param name="x"></param>
        /// <returns> Angle in radians. </returns>
        public static float Asin (float x)
        {
            return Mathf.Asin (x);
        }

        /// <summary> Returns the angle in radians whose cos is x. </summary>
        /// <param name="x"></param>
        /// <returns> Angle in radians. </returns>
        public static float Acos (float x)
        {
            return Mathf.Acos (x);
        }

        /// <summary> Returns the angle in radians whose tan is x. </summary>
        /// <param name="x"></param>
        /// <returns> Angle in radians. </returns>
        public static float Atan (float x)
        {
            return Mathf.Atan (x);
        }

        /// <summary> Returns the angle in radians whose tan is y/x </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns> Angle in radians. </returns>
        public static float Atan2 (float y, float x)
        {
            return Mathf.Atan2 (y, x);
        }

        /// <summary> Returns the Hyperbolic Sine of x. </summary>
        /// <param name="x"></param>
        /// <returns> Hyperbolic Sine Value. </returns>
        public static float SinH (float x)
        {
            float expX = Mathf.Exp (x);
            float expNX = Mathf.Exp (-x);
            return (expX - expNX) * 0.5f;
        }

        /// <summary> Returns the tan Hyperbolic Cosine of x. </summary>
        /// <param name="x"></param>
        /// <returns> Hyperbolic Cosine Value. </returns>
        public static float CosH (float x)
        {
            float expX = Mathf.Exp (x);
            float expNX = Mathf.Exp (-x);
            return (expX + expNX) * 0.5f;
        }

        /// <summary> Returns the Hyperbolic Tangent of x. </summary>
        /// <param name="x"></param>
        /// <returns> Hyperbolic Tangent Value. </returns>
        public static float TanH (float x)
        {
            float expX = Mathf.Exp (x);
            float expNX = Mathf.Exp (-x);
            return (expX - expNX) * Invert (expX + expNX);
        }

        /// <summary> Returns the angle whose Inverse SinH is x. </summary>
        /// <param name="x"></param>
        /// <returns> Angle in radians. </returns>
        public static float AsinH (float x)
        {
            float sqrt = Mathf.Sqrt (Mathf.Pow (x, 2) + 1);
            return Mathf.Log (x + sqrt, Mathf.Exp (1));
        }

        /// <summary> Returns the angle whose Inverse CosH is x. </summary>
        /// <param name="x"></param>
        /// <returns> Angle in radians. </returns>
        public static float AcosH (float x)
        {
            float sqrt = Mathf.Sqrt (Mathf.Pow (x, 2) + 1);
            return Mathf.Log (x - sqrt, Mathf.Exp (1));
        }

        /// <summary> Returns the angle whose Inverse TanH is x. </summary>
        /// <param name="x"></param>
        /// <returns> Angle in radians. </returns>
        public static float AtanH (float x)
        {
            float param = (1 + x) * Invert(1 - x);
            return 0.5f * Mathf.Log (param);
        }

        #endregion



        #region Vector2

        /// <summary> Set the value of X. </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        public static void SetX (this ref Vector2Int v, int x)
        {
            v.x = x;
        }

        /// <summary> Set the value of Y. </summary>
        /// <param name="v"></param>
        /// <param name="y"></param>
        public static void SetY (this ref Vector2Int v, int y)
        {
            v.y = y;
        }

        /// <summary> Add value to X. </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        public static void AddToX (this ref Vector2Int v, int x)
        {
            v.x += x;
        }

        /// <summary> Add value to y. </summary>
        /// <param name="v"></param>
        /// <param name="y"></param>
        public static void AddToY (this ref Vector2Int v, int y)
        {
            v.y += y;
        }

        /// <summary> Set the value of X. </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        public static void SetX (this ref Vector2 v, float x)
        {
            v.x = x;
        }

        /// <summary> Set the value of Y. </summary>
        /// <param name="v"></param>
        /// <param name="y"></param>
        public static void SetY (this ref Vector2 v, float y)
        {
            v.y = y;
        }

        /// <summary> Add value to X. </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        public static void AddToX (this ref Vector2 v, float x)
        {
            v.x += x;
        }

        /// <summary> Add value to y. </summary>
        /// <param name="v"></param>
        /// <param name="y"></param>
        public static void AddToY (this ref Vector2 v, float y)
        {
            v.y += y;
        }

        /// <summary> Converts to Vector2Int </summary>
        /// <param name="v"></param>
        /// <param name="type"></param>
        /// <returns></returns>
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

        #endregion



        #region Vector3

        /// <summary> Set the value of X. </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        public static void SetX (this ref Vector3Int v, int x)
        {
            v.x = x;
        }

        /// <summary> Set the value of Y. </summary>
        /// <param name="v"></param>
        /// <param name="y"></param>
        public static void SetY (this ref Vector3Int v, int y)
        {
            v.y = y;
        }

        /// <summary> Set the value of Z. </summary>
        /// <param name="v"></param>
        /// <param name="z"></param>
        public static void SetZ (this ref Vector3Int v, int z)
        {
            v.z = z;
        }

        /// <summary> Set the value of X. </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        public static void SetX (this ref Vector3 v, float x)
        {
            v.x = x;
        }

        /// <summary> Set the value of Y. </summary>
        /// <param name="v"></param>
        /// <param name="y"></param>
        public static void SetY (this ref Vector3 v, float y)
        {
            v.y = y;
        }

        /// <summary> Set the value of Z. </summary>
        /// <param name="v"></param>
        /// <param name="z"></param>
        public static void SetZ (this ref Vector3 v, float z)
        {
            v.z = z;
        }

        /// <summary> Add value to X. </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        public static void AddToX (this ref Vector3Int v, int x)
        {
            v.x += x;
        }

        /// <summary> Add value to Y. </summary>
        /// <param name="v"></param>
        /// <param name="y"></param>
        public static void AddToY (this ref Vector3Int v, int y)
        {
            v.y += y;
        }

        /// <summary> Add value to Z. </summary>
        /// <param name="v"></param>
        /// <param name="z"></param>
        public static void AddToZ (this ref Vector3Int v, int z)
        {
            v.z += z;
        }

        /// <summary> Add value to X. </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        public static void AddToX (this ref Vector3 v, float x)
        {
            v.x += x;
        }

        /// <summary> Add value to Y. </summary>
        /// <param name="v"></param>
        /// <param name="y"></param>
        public static void AddToY (this ref Vector3 v, float y)
        {
            v.y += y;
        }

        /// <summary> Add value to Z. </summary>
        /// <param name="v"></param>
        /// <param name="z"></param>
        public static void AddToZ (this ref Vector3 v, float z)
        {
            v.z += z;
        }

        /// <summary> Converts to Vector3Int </summary>
        /// <param name="v"></param>
        /// <param name="type"></param>
        /// <returns></returns>
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

        #endregion



        #region Transform

        /// <summary> Set the value of X. </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        public static void SetX (this Transform transform, float x)
        {
            Vector3 position = transform.position;
            transform.position = new Vector3 (x, position.y, position.z);
        }

        /// <summary> Set the value of Y. </summary>
        /// <param name="transform"></param>
        /// <param name="y"></param>
        public static void SetY (this Transform transform, float y)
        {
            Vector3 position = transform.position;
            transform.position = new Vector3 (position.x, y, position.z);
        }

        /// <summary> Set the value of Z. </summary>
        /// <param name="transform"></param>
        /// <param name="z"></param>
        public static void SetZ (this Transform transform, float z)
        {
            Vector3 position = transform.position;
            transform.position = new Vector3 (position.x, position.y, z);
        }

        /// <summary> Add value to X. </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        public static void AddToX (this Transform transform, float x)
        {
            transform.SetX (transform.position.x + x);
        }

        /// <summary> Add value to Y. </summary>
        /// <param name="transform"></param>
        /// <param name="y"></param>
        public static void AddToY (this Transform transform, float y)
        {
            transform.SetY (transform.position.y + y);
        }

        /// <summary> Add value to Z. </summary>
        /// <param name="transform"></param>
        /// <param name="z"></param>
        public static void AddToZ (this Transform transform, float z)
        {
            transform.SetZ (transform.position.z + z);
        }

        #endregion
    }
}
