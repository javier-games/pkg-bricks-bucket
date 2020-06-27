namespace BricksBucket.Core
{
	public static partial class Utils
	{
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
	}
}