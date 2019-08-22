using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using Exception = System.Exception;
using Array = System.Array;

namespace BricksBucket.Collections
{
    public static class CollectionUtils
    {
        #region Array Extensions

        /// <summary> Verify wether the index is valid. </summary>
        /// <typeparam name="T"> Type of the array. </typeparam>
        /// <param name="array"> Array Collection. </param>
        /// <param name="index"> Index to validate. </param>
        /// <returns> Wether the index is valid. </returns>
        public static bool HasIndex<T> (this T[] array, int index) =>
            index < array.Length && index >= 0;

        /// <summary> Wether is a null or empty array. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="array"> Array Collection </param>
        /// <returns> Wether is a null or empty array. </returns>
        public static bool IsNullOrEmpty<T> (this T[] array)
        {
            if (array == null)
                return true;
            return array.Length == 0;
        }

        /// <summary> Swaps the values of index A and index B </summary>
        /// <typeparam name="T"> Type of the array. </typeparam>
        /// <param name="array"> Array Collection. </param>
        /// <param name="a"> Index A. </param>
        /// <param name="b"> Index B. </param>
        public static void Swap<T> (this T[] array, int a, int b)
        {
            if (!array.HasIndex (a))
                throw IndexOutOfRangeException (array, a);

            if (!array.HasIndex (b))
                throw IndexOutOfRangeException (array, b);

            MathUtils.Swap (ref array[a], ref array[b]);
        }

        /// <summary> Screamble the elements in this array. </summary>
        /// <typeparam name="T"> Type of the array. </typeparam>
        /// <param name="array"> Array Collection. </param>
        public static void Shuffle<T> (this T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int r = Random.Range (0, i);
                array.Swap (r, i);
            }
        }

        /// <summary> Returns a random element from array. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="array"> Array Collection. </param>
        /// <returns> Random element in array. </returns>
        public static T GetRandom<T> (this T[] array) =>
            array[Random.Range (0, array.Length)];

        /// <summary> Returns a sample of indexes in the array. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="array"> Array Collection. </param>
        /// <param name="size"> Size of the sample. </param>
        /// <param name="random"> Wether to select random elements. </param>
        /// <returns> Sample of indexes. </returns>
        public static int[]
        GetIndexesSample<T> (this T[] array, int size, bool random = false)
        {
            if (size < 0)
                throw IndexOutOfRangeException (array, size);

            if (array.IsNullOrEmpty ())
                return null;

            int[] sample = new int[size];

            if (random)
                for (int i = 0; i < size; i++)
                    sample[i] = Random.Range (0, array.Length);
            else
                for (int i = 0; i < size; i++)
                {
                    int index = i;
                    if (index >= array.Length)
                        index %= array.Length;
                    sample[i] = index;
                }

            return sample;
        }

        /// <summary> Returns a sample of elements in the array. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="array"> Array Collection. </param>
        /// <param name="size"> Size of the sample. </param>
        /// <param name="random"> Wether to select random elements. </param>
        /// <returns> Sample of elements. </returns>
        public static T[]
        GetSample<T> (this T[] array, int size, bool random = false)
        {
            if (size < 0)
                throw IndexOutOfRangeException (array, size);

            if (array.IsNullOrEmpty ())
                return null;

            T[] sample = new T[size];

            if (random)
                for (int i = 0; i < size; i++)
                    sample[i] = array.GetRandom ();
            else
                for (int i = 0; i < size; i++)
                {
                    int index = i;
                    if (index >= array.Length)
                        index %= array.Length;
                    sample[i] = array[index];
                }

            return sample;
        }

        /// <summary> Loops the array with the specified increment. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="array"> Array Collection. </param>
        /// <param name="index"> Index to loop. </param>
        /// <param name="increment"> Increment to apply. </param>
        /// <returns> Next element in the loop. </returns>
        public static T
        Loop <T>(this T[] array, ref int index, int increment = 1)
        {
            if (array.IsNullOrEmpty ())
                throw NullOrEmptyException (array);

            index = MathUtils.Loop (index, 0, array.Length - 1, increment);
            return array[index];
        }

        #endregion



        #region List Extensions

        /// <summary> Verify wether the index is valid. </summary>
        /// <typeparam name="T"> Type of the list. </typeparam>
        /// <param name="list"> List Collection. </param>
        /// <param name="index"> Index to validate. </param>
        /// <returns> Wether the index is valid. </returns>
        public static bool HasIndex<T> (this IList<T> list, int index) =>
            index < list.Count && index >= 0;

        /// <summary> Wether is a null or empty list. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="list"> List Collection </param>
        /// <returns> Wether is a null or empty list. </returns>
        public static bool IsNullOrEmpty<T> (this IList<T> list)
        {
            if (list == null)
                return true;
            return list.Count == 0;
        }

        /// <summary> Swaps the values of index A and index B </summary>
        /// <typeparam name="T"> Type of the list. </typeparam>
        /// <param name="list"> List Collection. </param>
        /// <param name="a"> Index A. </param>
        /// <param name="b"> Index B. </param>
        public static void Swap<T> (this IList<T> list, int a, int b)
        {
            if (!list.HasIndex (a))
                throw IndexOutOfRangeException (list, a);

            if (!list.HasIndex (b))
                throw IndexOutOfRangeException (list, b);

            T value = list[a];
            list[a] = list[b];
            list[b] = value;
        }

        /// <summary> Screamble the elements in this list. </summary>
        /// <typeparam name="T"> Type of list. </typeparam>
        /// <param name="list"> List Collection. </param>
        public static void Shuffle<T> (this IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int r = Random.Range (0, i);
                list.Swap (i, r);
            }
        }

        /// <summary> Returns a random element from collection. </summary>
        /// <typeparam name="T"> Type of list. </typeparam>
        /// <param name="list"> List Collection. </param>
        /// <returns> Random element in list. </returns>
        public static T GetRandom<T> (this IList<T> list) =>
            list[Random.Range (0, list.Count)];

        /// <summary> Returns a sample of indexes in the list. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="list"> List Collection. </param>
        /// <param name="size"> Size of the sample. </param>
        /// <param name="random"> Wether to select random elements. </param>
        /// <returns> Sample of indexes. </returns>
        public static int[]
        GetIndexesSample<T> (this IList<T> list, int size, bool random = false)
        {
            if (size < 0)
                throw IndexOutOfRangeException (list, size);

            if (list.IsNullOrEmpty ())
                return null;

            int[] sample = new int[size];

            if (random)
                for (int i = 0; i < size; i++)
                    sample[i] = Random.Range (0, list.Count);
            else
                for (int i = 0; i < size; i++)
                {
                    int index = i;
                    if (index >= list.Count)
                        index %= list.Count;
                    sample[i] = index;
                }

            return sample;
        }

        /// <summary> Returns a sample of indexes in the list. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="list"> List Collection. </param>
        /// <param name="size"> Size of the sample. </param>
        /// <param name="random"> Wether to select random elements. </param>
        /// <returns> Sample of indexes. </returns>
        public static T[]
        GetSample<T> (this IList<T> list, int size, bool random = false)
        {
            if (size < 0)
                throw IndexOutOfRangeException (list, size);

            if (list.IsNullOrEmpty ())
                return null;

            T[] sample = new T[size];

            if (random)
                for (int i = 0; i < size; i++)
                    sample[i] = list.GetRandom ();
            else
                for (int i = 0; i < size; i++)
                {
                    int index = i;
                    if (index >= list.Count)
                        index %= list.Count;
                    sample[i] = list[index];
                }

            return sample;
        }

        /// <summary> Loops the list with the specified increment. </summary>
        /// <typeparam name="T"> Type of list. </typeparam>
        /// <param name="list"> List Collection. </param>
        /// <param name="index"> Index to loop. </param>
        /// <param name="increment"> Increment to apply. </param>
        /// <returns> Next element in the loop. </returns>
        public static T
        Loop<T> (this List<T> list, ref int index, int increment = 1)
        {
            if (list.IsNullOrEmpty ())
                throw NullOrEmptyException (list);

            index = MathUtils.Loop (index, 0, list.Count - 1, increment);
            return list[index];
        }

        #endregion



        #region Enumerable Extensions

        /// <summary> Verify wether the index is valid. </summary>
        /// <typeparam name="T"> Type of the enumerable. </typeparam>
        /// <param name="enumerable"> Enumerable Collection. </param>
        /// <param name="index"> Index to validate. </param>
        /// <returns> Wether the index is valid. </returns>
        public static bool
        HasIndex<T> (this IEnumerable<T> enumerable, int index) =>
            index < enumerable.Count () && index >= 0;

        /// <summary>
        /// Wether is a null or empty enumerable.
        /// <para>
        /// IEnumerable is relatively slow. Use Array or List as possible.
        /// </para>
        /// </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="enumerable"> List Collection </param>
        /// <returns> Wether is a null or empty list. </returns>
        public static bool IsNullOrEmpty<T> (this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return true;
            return !enumerable.Any ();
        }

        /// <summary> Returns random element from collection </summary>
        /// <typeparam name="T"> Type of enumerable. </typeparam>
        /// <param name="enumerable"> Enumerable Collection. </param>
        /// <returns> Random element in enumerable. </returns>
        public static T GetRandom<T> (this IEnumerable<T> enumerable) =>
            enumerable.ElementAt (Random.Range (0, enumerable.Count ()));

        /// <summary> Returns the index of the item. </summary>
        /// <typeparam name="T"> Type of enumerable. </typeparam>
        /// <param name="enumerable"> Enumerable Collection. </param>
        /// <param name="item"> Item to find. </param>
        /// <returns> Index of item. -1 if none found </returns>
        public static int IndexOf<T> (this IEnumerable<T> enumerable, T item)
        {
            if (enumerable.IsNullOrEmpty ())
                return -1;

            var index = 0;
            foreach (var i in enumerable)
            {
                if (Equals (i, item))
                    return index;
                ++index;
            }

            return -1;
        }

        /// <summary> Compares an enumerable with other. </summary>
        /// <typeparam name="T"> Type of the collection. </typeparam>
        /// <param name="enumerable"> Enumerable collection. </param>
        /// <param name="other"> Enumerable to compare. </param>
        /// <returns> Wether two collections are the same. </returns>
        public static bool
        ContentsMatch<T> (this IEnumerable<T> enumerable, IEnumerable<T> other)
        {
            if (enumerable.IsNullOrEmpty () && other.IsNullOrEmpty ())
                return true;
            if (enumerable.IsNullOrEmpty () || other.IsNullOrEmpty ())
                return false;

            var firstCount = enumerable.Count ();
            var secondCount = other.Count ();

            if (firstCount != secondCount)
                return false;

            foreach (var element in enumerable)
                if (!other.Contains (element))
                    return false;
            return true;
        }

        #endregion



        #region Public Static Methods

        /// <summary> Returns a sequence of numbers in a range. </summary>
        /// <param name="from"> First number of the range. </param>
        /// <param name="to"> End number of the range. </param>
        /// <param name="random"> Wether the sequence must be shuffle. </param>
        /// <returns> Sequence of numbers. </returns>
        public static int[] GetSequence (int from, int to, bool random = false)
        {
            if (to < from)
                MathUtils.Swap (ref from, ref to);

            int[] sequense = new int[to - from];

            for (int i = 0; i < sequense.Length; i++)
                sequense[i] = from + i;

            if (random)
                sequense.Shuffle ();

            return sequense;
        }

        /// <summary> Removes an element at the specified index. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="array"> Array collection. </param>
        /// <param name="index"> Index to remove from collection. </param>
        public static void RemoveAt<T> (ref T[] array, int index)
        {
            if (array.IsNullOrEmpty ())
                throw NullOrEmptyException (array);

            if (!array.HasIndex (index))
                throw IndexOutOfRangeException (array, index);

            for (int i = index; i < array.Length - 1; i++)
                array.Swap (i, i + 1);

            Array.Resize (ref array, array.Length - 1);
        }

        /// <summary> Add a new element at specified index. </summary>
        /// <typeparam name="T"> Type of collection. </typeparam>
        /// <param name="array"> Array where to add the element. </param>
        /// <param name="index"> Index where to colocate element. </param>
        /// <param name="element"> Element to add. </param>
		public static void AddAt<T>(ref T[] array, int index, T element)
		{
			if (array.IsNullOrEmpty())
				throw NullOrEmptyException(array);

			if (!array.HasIndex(index))
				throw IndexOutOfRangeException(array, index);

			Array.Resize(ref array, array.Length + 1);
			array[array.Length - 1] = element;

			for (int i = array.Length - 1; i > index; i--)
				array.Swap(i, i - 1);
		}

        #endregion



        #region Exceptions

        /// <summary> Value out of range. </summary>
        /// <param name="collection"></param>
        /// <param name="index"></param>
        /// <returns> Exception. </returns>
        private static Exception
        IndexOutOfRangeException (object collection, int index)
        {
            return new Exception (
                StringUtils.ConcatFormat (
                    format: "The collection {0} does'nt contain the index {1}.",
                    array: new object[] {
                        collection,
                        index
                    }
                )
            );
        }

        /// <summary> Null or empty collection. </summary>
        /// <param name="collection"></param>
        /// <returns> Exception. </returns>
        private static Exception
        NullOrEmptyException (object collection)
        {
            return new Exception (
                StringUtils.ConcatFormat (
                    format: "The collection {0} is null or empty.",
                    array: collection
                )
            );
        }

        #endregion
    }
}
