using UnityEngine;

using SystemMath = System.Math;

namespace BricksBucket.Math
{
    public static class Trigonometry
    {
        /// <summary>
        /// Returns the sine of x.
        /// </summary>
        /// <param name="x">The input angle, in radians.</param>
        /// <returns>Sine value.</returns>
        public static float Sin(float x) => Mathf.Sin(x);

        /// <summary>
        /// Returns the cosine of x.
        /// </summary>
        /// <param name="x">The input angle, in radians.</param>
        /// <returns>Cosine value.</returns>
        public static float Cos (float x) => Mathf.Cos (x);

        /// <summary>
        /// Returns the tangent of x.
        /// </summary>
        /// <param name="x">The input angle, in radians.</param>
        /// <returns>Tangent value.</returns>
        public static float Tan (float x) => Mathf.Tan (x);

        /// <summary>
        /// Returns the angle in radians whose sin is x.
        /// </summary>
        /// <param name="x">Value to evaluate.</param>
        /// <returns>Angle in radians.</returns>
        public static float Asin (float x) => Mathf.Asin (x);

        /// <summary>
        /// Returns the angle in radians whose cos is x.
        /// </summary>
        /// <param name="x">Value to evaluate.</param>
        /// <returns>Angle in radians.</returns>
        public static float Acos (float x) => Mathf.Acos (x);

        /// <summary>
        /// Returns the angle in radians whose tan is x.
        /// </summary>
        /// <param name="x">Value to evaluate.</param>
        /// <returns>Angle in radians.</returns>
        public static float Atan (float x) => Mathf.Atan (x);

        /// <summary>
        /// Returns the angle in radians whose tan is y/x.
        /// </summary>
        /// <param name="y">Y value to evaluate.</param>
        /// <param name="x">X value to evaluate.</param>
        /// <returns>Angle in radians.</returns>
        public static float Atan2(float y, float x) => Mathf.Atan2(y, x);

        /// <summary>
        /// Returns the Hyperbolic Sine of x.
        /// </summary>
        /// <param name="x">The input angle, in radians.</param>
        /// <returns>Hyperbolic Sine Value.</returns>
        public static float Sinh(float x) => (float)SystemMath.Sinh(x);

        /// <summary>
        /// Returns the tan Hyperbolic Cosine of x.
        /// </summary>
        /// <param name="x">The input angle, in radians.</param>
        /// <returns>Hyperbolic Cosine Value.</returns>
        public static float Cosh(float x) => (float)SystemMath.Cosh(x);

        /// <summary>
        /// Returns the Hyperbolic Tangent of x.
        /// </summary>
        /// <param name="x">The input angle, in radians.</param>
        /// <returns>Hyperbolic Tangent Value.</returns>
        public static float Tanh(float x) => (float)SystemMath.Tanh(x);

        /// <summary>
        /// Returns the angle whose Inverse SinH is x.
        /// </summary>
        /// <param name="x">Value to evaluate.</param>
        /// <returns>Angle in radians.</returns>
        public static float Asinh (float x)
        {
            float sqrt = Mathf.Sqrt (Mathf.Pow (x, 2) + 1);
            return Mathf.Log (x + sqrt, Mathf.Exp (1));
        }

        /// <summary>
        /// Returns the angle whose Inverse CosH is x.
        /// </summary>
        /// <param name="x">Value to evaluate.</param>
        /// <returns>Angle in radians.</returns>
        public static float Acosh (float x)
        {
            float sqrt = Mathf.Sqrt (Mathf.Pow (x, 2) + 1);
            return Mathf.Log (x - sqrt, Mathf.Exp (1));
        }

        /// <summary>
        /// Returns the angle whose Inverse TanH is x.
        /// </summary>
        /// <param name="x">Value to evaluate.</param>
        /// <returns>Angle in radians.</returns>
        public static float Atanh (float x)
        {
            float param = (1 + x) * (1 - x).Invert();
            return 0.5f * Mathf.Log (param);
        }

    }
}
