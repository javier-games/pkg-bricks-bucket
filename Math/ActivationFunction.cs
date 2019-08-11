using UnityEngine;

namespace BricksBucket.Math
{
    public static class ActivationFunction
    {
        /// <summary> Sign function. </summary>
        /// <param name="x"> X float value to evaluate. </param>
        /// <returns> Sign of x: 1, 0, -1 </returns>
        public static float Sign(float x) =>
            x > 0 ? 1 : x < 0 ? -1 : 0;

        /// <summary> Binary Step function. </summary>
        /// <param name="x"> X float value to evaluate. </param>
        /// <returns> Binary Step of x. </returns>
        public static int BinaryStep (float x) =>
            x < 0 ? 0 : 1;

        /// <summary> Sigmoid function. </summary>
        /// <param name="x"> X float value to evaluate. </param>
        /// <returns>  Sigmoid of x.  </returns>
        public static float Sigmoid (float x) =>
            (1 + Mathf.Exp (-x)).Invert ();

        /// <summary> Softsign function. </summary>
        /// <param name="x"> X float value to evaluate. </param>
        /// <returns> Softsign of x. </returns>
        public static float SoftSign(float x) =>
            x * (1 + x.Absolute()).Invert();

        /// <summary> Inverse Square Root Unit function. </summary>
        /// <param name="x"> X float value to evaluate. </param>
        /// <param name="alpha"></param>
        /// <returns> Inverse Square Root Unit of x. </returns>
        public static float ISRU(float x, float alpha) =>
            x * Mathf.Sqrt(1 + alpha + Mathf.Pow(x, 2)).Invert();

        /// <summary> Inverse Square Root Linear Unit function. </summary>
        /// <param name="x"> X float value to evaluate. </param>
        /// <param name="alpha"></param>
        /// <returns> Inverse Square Root Linear Unit of x. </returns>
        public static float ISRLU(float x, float alpha) =>
            x < 0 ? ISRU(x, alpha) : x;

        /// <summary> Rectified Linear Unit function. </summary>
        /// <param name="x"> X float value to evaluate. </param>
        /// <returns> Rectified Linear Unit  of x. </returns>
        public static float ReLU(float x) =>
            x > 0 ? x : 0;

        /// <summary> Sinc function. </summary>
        /// <param name="x"> X float value to evaluate. </param>
        /// <returns> Sinc of x. </returns>
        public static float Sinc(float x) =>
            x.Approximately(0) ? 1 : Mathf.Sin(x) * x.Invert();

        /// <summary> Gaussian function. </summary>
        /// <param name="x"> X float value to evaluate. </param>
        /// <returns> Gaussian of x. </returns>
        public static float Gaussian(float x) =>
            Mathf.Exp(-Mathf.Pow(x, 2));

        /// <summary> Hyperbolic Tangent  function. </summary>
        /// <param name="x"> X float value to evaluate. </param>
        /// <returns> Hyperbolic Tangent of x </returns>
        public static float Tanh (float x) =>
            Trigonometry.Tanh (x);

        /// <summary> Angle whose tangent function. </summary>
        /// <param name="x"> X float value </param>
        /// <returns> The angle whose tan is x. </returns>
        public static float Atan (float x) =>
            Trigonometry.Atan (x);

        /// <summary> Angle whose hyperbolic sin function. </summary>
        /// <param name="x"> X float value to evaluate. </param>
        /// <returns> The angle whose Sinh is x. </returns>
        public static float Asinh (float x) =>
            Trigonometry.Asinh (x);

    }
}
