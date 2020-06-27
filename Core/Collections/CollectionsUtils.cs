using System;
using System.Linq;
using System.Collections.Generic;

using Exception = System.Exception;
using Array = System.Array;
using Random = UnityEngine.Random;
using BricksBucket.Core;

namespace BricksBucket.Collections
{
    /// <summary>
    ///
    /// CollectionsUtils.
    ///
    /// <para>
    /// Useful extensions and methods for generic collections.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// </summary>
    public static class CollectionUtils
    {

        #region Array Extensions and Methods

        /// <summary> Verify whether the index is valid. </summary>
        /// <typeparam name="T"> Type of the array. </typeparam>
        /// <param name="array"> Array Collection. </param>
        /// <param name="index"> Index to validate. </param>
        /// <returns> Whether the index is valid. </returns>
        public static bool HasIndex<T> (this T[] array, int index) =>
            index < array.Length && index >= 0;

        /// <summary> Whether is a null or empty array. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="array"> Array Collection </param>
        /// <returns> Whether is a null or empty array. </returns>
        public static bool IsNullOrEmpty<T> (this T[] array)
        {
            if (array == null) return true;
            return array.Length == 0;
        }

        /// <summary> Whether an array has the specified element. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="array"> Array collection. </param>
        /// <param name="element"> Element to validate. </param>
        /// <returns> -1 if an elements does not found. </returns>
        public static bool Contains<T> (this T[] array, T element) =>
            array.Find (element) >= 0;

        public static bool Exists<T> (this T[] array, Predicate<T> predicate)
        {
            return Array.Exists (array, predicate);
        }

        /// <summary> Finds an element in an array. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="array"> Array collection. </param>
        /// <param name="element"> Element to find. </param>
        /// <returns> -1 if an elements does not found. </returns>
        public static int Find<T> (this T[] array, T element)
        {
            if (array.IsNullOrEmpty ()) return -1;

            for (int i = 0; i < array.Length; i++)
                if (EqualityComparer<T>.Default.Equals (array[i], element))
                    return i;

            return -1;
        }

        /// <summary>
        /// Finds an element in array.
        /// </summary>
        /// <param name="array">Array collection.</param>
        /// <param name="predicate">Predicate to find an element.</param>
        /// <typeparam name="T">Type of array.</typeparam>
        /// <returns>-1 if an elements does not found.</returns>
        public static int Find<T> (this T[] array, Predicate<T> predicate)
        {
            return array.Find (Array.Find (array, predicate));
        }

        /// <summary> Swaps the values of index A and index B </summary>
        /// <typeparam name="T"> Type of the array. </typeparam>
        /// <param name="array"> Array Collection. </param>
        /// <param name="a"> Index A. </param>
        /// <param name="b"> Index B. </param>
        public static void Swap<T> (this T[] array, int a, int b)
        {
            if (!array.HasIndex (a)) throw IndexOutOfRangeException (array, a);

            if (!array.HasIndex (b)) throw IndexOutOfRangeException (array, b);

            Utils.Swap (ref array[a], ref array[b]);
        }

        /// <summary> Scramble the elements in this array. </summary>
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
        /// <param name="random"> Whether to select random elements. </param>
        /// <returns> Sample of indexes. </returns>
        public static int[] GetIndexesSample<T> (
            this T[] array,
            int size,
            bool random = false
        )
        {
            if (size < 0) throw IndexOutOfRangeException (array, size);

            if (array.IsNullOrEmpty ()) return null;

            var sample = new int[size];

            if (random)
                for (int i = 0; i < size; i++)
                    sample[i] = Random.Range (0, array.Length);
            else
                for (int i = 0; i < size; i++)
                {
                    int index = i;
                    if (index >= array.Length) index %= array.Length;
                    sample[i] = index;
                }

            return sample;
        }

        /// <summary> Returns a sample of elements in the array. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="array"> Array Collection. </param>
        /// <param name="size"> Size of the sample. </param>
        /// <param name="random"> Whether to select random elements. </param>
        /// <returns> Sample of elements. </returns>
        public static T[] GetSample<T> (
            this T[] array,
            int size,
            bool random = false
        )
        {
            if (size < 0) throw IndexOutOfRangeException (array, size);

            if (array.IsNullOrEmpty ()) return null;

            T[] sample = new T[size];

            if (random)
                for (int i = 0; i < size; i++)
                    sample[i] = array.GetRandom ();
            else
                for (int i = 0; i < size; i++)
                {
                    int index = i;
                    if (index >= array.Length) index %= array.Length;
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
        public static T Loop<T> (
            this T[] array,
            ref int index,
            int increment = 1
        )
        {
            if (array.IsNullOrEmpty ()) throw NullOrEmptyException (array);

            index = index.Loop (0, array.Length - 1, increment);
            return array[index];
        }

        /// <summary> Returns a sequence of numbers in a range. </summary>
        /// <param name="from"> First number of the range. </param>
        /// <param name="to"> End number of the range. </param>
        /// <param name="random"> Whether the sequence must be shuffle. </param>
        /// <returns> Sequence of numbers. </returns>
        public static int[] GetSequence (int from, int to, bool random = false)
        {
            if (to < from) Utils.Swap (ref from, ref to);

            int[] sequence = new int[to - from];

            for (int i = 0; i < sequence.Length; i++) sequence[i] = from + i;

            if (random) sequence.Shuffle ();

            return sequence;
        }

        /// <summary> Removes an element at the specified index. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="array"> Array collection. </param>
        /// <param name="index"> Index to remove from collection. </param>
        public static void RemoveAt<T> (ref T[] array, int index)
        {
            if (array.IsNullOrEmpty ()) throw NullOrEmptyException (array);

            if (!array.HasIndex (index))
                throw IndexOutOfRangeException (array, index);

            for (int i = index; i < array.Length - 1; i++)
                array.Swap (i, i + 1);

            Array.Resize (ref array, array.Length - 1);
        }

        /// <summary> Removes the specified element. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="array"> Array collection. </param>
        /// <param name="element"> Element remove from collection. </param>
        /// <returns> Whether the value is not any more in the array. </returns>
        public static void Remove<T> (ref T[] array, T element)
        {
            if (array.IsNullOrEmpty ()) throw NullOrEmptyException (array);

            int index = array.Find (element);
            if (index >= 0)
                RemoveAt (ref array, index);
            else
                throw ElementNotFoundException (array, element);
        }

        /// <summary> Add a new element at specified index. </summary>
        /// <typeparam name="T"> Type of collection. </typeparam>
        /// <param name="array"> Array where to add the element. </param>
        /// <param name="index"> Index where to colocate element. </param>
        /// <param name="element"> Element to add. </param>
        public static void AddAt<T> (ref T[] array, int index, T element)
        {
            if (array.IsNullOrEmpty ()) throw NullOrEmptyException (array);

            if (!array.HasIndex (index))
                throw IndexOutOfRangeException (array, index);

            Add (ref array, element);

            for (int i = array.Length - 1; i > index; i--)
                array.Swap (i, i - 1);
        }

        /// <summary> Add a new element at the last position. </summary>
        /// <typeparam name="T"> Type of collection. </typeparam>
        /// <param name="array"> Array where to add the element. </param>
        /// <param name="element"> Element to add. </param>
        public static void Add<T> (ref T[] array, T element)
        {
            if (array.IsNullOrEmpty ()) throw NullOrEmptyException (array);

            Array.Resize (ref array, array.Length + 1);
            array[array.Length - 1] = element;
        }

        #endregion



        #region List Extensions

        /// <summary> Verify whether the index is valid. </summary>
        /// <typeparam name="T"> Type of the list. </typeparam>
        /// <param name="list"> List Collection. </param>
        /// <param name="index"> Index to validate. </param>
        /// <returns> Whether the index is valid. </returns>
        public static bool HasIndex<T> (this IList<T> list, int index) =>
            index < list.Count && index >= 0;

        /// <summary> Whether is a null or empty list. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="list"> List Collection </param>
        /// <returns> Whether is a null or empty list. </returns>
        public static bool IsNullOrEmpty<T> (this IList<T> list)
        {
            if (list == null) return true;
            return list.Count == 0;
        }

        /// <summary> Swaps the values of index A and index B </summary>
        /// <typeparam name="T"> Type of the list. </typeparam>
        /// <param name="list"> List Collection. </param>
        /// <param name="a"> Index A. </param>
        /// <param name="b"> Index B. </param>
        public static void Swap<T> (this IList<T> list, int a, int b)
        {
            if (!list.HasIndex (a)) throw IndexOutOfRangeException (list, a);

            if (!list.HasIndex (b)) throw IndexOutOfRangeException (list, b);

            T value = list[a];
            list[a] = list[b];
            list[b] = value;
        }

        /// <summary> Scramble the elements in this list. </summary>
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
        /// <param name="random"> Whether to select random elements. </param>
        /// <returns> Sample of indexes. </returns>
        public static int[] GetIndexesSample<T> (
            this IList<T> list,
            int size,
            bool random = false
        )
        {
            if (size < 0) throw IndexOutOfRangeException (list, size);

            if (list.IsNullOrEmpty ()) return null;

            int[] sample = new int[size];

            if (random)
                for (int i = 0; i < size; i++)
                    sample[i] = Random.Range (0, list.Count);
            else
                for (int i = 0; i < size; i++)
                {
                    int index = i;
                    if (index >= list.Count) index %= list.Count;
                    sample[i] = index;
                }

            return sample;
        }

        /// <summary> Returns a sample of indexes in the list. </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="list"> List Collection. </param>
        /// <param name="size"> Size of the sample. </param>
        /// <param name="random"> Whether to select random elements. </param>
        /// <returns> Sample of indexes. </returns>
        public static T[]
            GetSample<T> (this IList<T> list, int size, bool random = false)
        {
            if (size < 0) throw IndexOutOfRangeException (list, size);

            if (list.IsNullOrEmpty ()) return null;

            T[] sample = new T[size];

            if (random)
                for (int i = 0; i < size; i++)
                    sample[i] = list.GetRandom ();
            else
                for (int i = 0; i < size; i++)
                {
                    int index = i;
                    if (index >= list.Count) index %= list.Count;
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
            if (list.IsNullOrEmpty ()) throw NullOrEmptyException (list);

            index = index.Loop (0, list.Count - 1, increment);
            return list[index];
        }

        #endregion



        #region Enumerable Extensions

        /// <summary> Verify whether the index is valid. </summary>
        /// <typeparam name="T"> Type of the enumerable. </typeparam>
        /// <param name="enumerable"> Enumerable Collection. </param>
        /// <param name="index"> Index to validate. </param>
        /// <returns> Whether the index is valid. </returns>
        public static bool
            HasIndex<T> (this IEnumerable<T> enumerable, int index) =>
            index < enumerable.Count () && index >= 0;

        /// <summary>
        /// Whether is a null or empty enumerable.
        /// <para>
        /// IEnumerable is relatively slow. Use Array or List as possible.
        /// </para>
        /// </summary>
        /// <typeparam name="T"> Type of array. </typeparam>
        /// <param name="enumerable"> List Collection </param>
        /// <returns> Whether is a null or empty list. </returns>
        public static bool IsNullOrEmpty<T> (this IEnumerable<T> enumerable)
        {
            if (enumerable == null) return true;
            return !enumerable.Any ();
        }

        /// <summary>
        /// Returns the element at the specified index.
        /// </summary>
        /// <typeparam name="T">Type of Collection.</typeparam>
        /// <param name="enumerable">Enumerable Collection.</param>
        /// <param name="index">Index of element to change.</param>
        /// <returns>The element at the specified index.</returns>
        public static T
            GetElement<T> (this IEnumerable<T> enumerable, int index) =>
            enumerable.ElementAt (index);

        /// <summary> Returns random element from collection </summary>
        /// <typeparam name="T"> Type of enumerable. </typeparam>
        /// <param name="enumerable"> Enumerable Collection. </param>
        /// <returns> Random element in enumerable. </returns>
        public static T GetRandom<T> (this IEnumerable<T> enumerable)
        {
            var enumerable1 = enumerable as T[] ?? enumerable.ToArray ();
            return enumerable1.ElementAt (
                Random.Range (0, enumerable1.Count ()));
        }

        /// <summary> Returns the index of the item. </summary>
        /// <typeparam name="T"> Type of enumerable. </typeparam>
        /// <param name="enumerable"> Enumerable Collection. </param>
        /// <param name="item"> Item to find. </param>
        /// <returns> Index of item. -1 if none found </returns>
        public static int IndexOf<T> (this IEnumerable<T> enumerable, T item)
        {
            var @is = enumerable as T[] ?? enumerable.ToArray ();
            if (@is.IsNullOrEmpty ()) return -1;

            var index = 0;
            foreach (var i in @is)
            {
                if (Equals (i, item)) return index;
                ++index;
            }

            return -1;
        }

        /// <summary> Compares an enumerable with other. </summary>
        /// <typeparam name="T"> Type of the collection. </typeparam>
        /// <param name="enumerable"> Enumerable collection. </param>
        /// <param name="other"> Enumerable to compare. </param>
        /// <returns> Whether two collections are the same. </returns>
        public static bool ContentsMatch<T> (
            this IEnumerable<T> enumerable,
            IEnumerable<T> other
        )
        {
            var elements = enumerable as T[] ?? enumerable.ToArray ();
            var enumerable1 = other as T[] ?? other.ToArray ();

            if (elements.IsNullOrEmpty () && enumerable1.IsNullOrEmpty ())
                return true;

            if (elements.IsNullOrEmpty () || enumerable1.IsNullOrEmpty ())
                return false;

            var firstCount = elements.Count ();
            var secondCount = enumerable1.Count ();

            return firstCount == secondCount && elements.All (
                element => enumerable1.Contains (element)
            );
        }

        #endregion



        #region Stack Utils

        /// <summary>
        /// Removes an element from the stack.
        /// </summary>
        /// <typeparam name="T">Type of the stack.</typeparam>
        /// <param name="stack">Stack collection.</param>
        /// <param name="index">Index to remove.</param>
        public static void RemoveAt<T> (this Stack<T> stack, int index)
        {
            if (stack.IsNullOrEmpty ()) throw NullOrEmptyException (stack);

            if (!stack.HasIndex (index))
                throw IndexOutOfRangeException (stack, index);

            Stack<T> aux = new Stack<T> ();
            int times = stack.Count - index - 1;

            for (int i = 0; i < times; i++) aux.Push (stack.Pop ());

            stack.Pop ();

            for (int i = 0; i < times; i++) stack.Push (aux.Pop ());
        }

        /// <summary>
        /// Removes the specified element from stack.
        /// </summary>
        /// <typeparam name="T">Type of stack.</typeparam>
        /// <param name="stack">Stack Collection.</param>
        /// <param name="element">Element to remove.</param>
        public static void Remove<T> (this Stack<T> stack, T element)
        {
            if (stack.IsNullOrEmpty ()) throw NullOrEmptyException (stack);

            int index = stack.Find (element);
            if (index >= 0)
                stack.RemoveAt (index);
            else
                throw ElementNotFoundException (stack, element);
        }

        /// <summary>
        /// Finds the index of an element on stack.
        /// </summary>
        /// <typeparam name="T">Type of stack.</typeparam>
        /// <param name="stack">Stack Collection.</param>
        /// <param name="element">Element to find.</param>
        /// <returns> -1 if an elements does not found. </returns>
        public static int Find<T> (this Stack<T> stack, T element)
        {
            return stack.ToArray ().Find (element);
        }

        #endregion



        #region Exceptions

        /// <summary> Value out of range. </summary>
        /// <param name="collection"> Collection with exception. </param>
        /// <param name="index"> Index out of range. </param>
        /// <returns> Exception. </returns>
        public static Exception IndexOutOfRangeException (
            object collection,
            int index
        )
        {
            return new Exception (
                string.Format (
                    format: "The collection {0} does not contains index {1}.",
                     new[] {collection, index}
                )
            );
        }

        /// <summary> Value out of range. </summary>
        /// <param name="collection"> Collection with exception. </param>
        /// <param name="element"> Element not found. </param>
        /// <returns> Exception. </returns>
        public static Exception
            ElementNotFoundException<T> (object collection, T element)
        {
            return new Exception (
                string.Format (
                    format: "The collection {0} does not contains element {1}.",
                    new[] {collection, element}
                )
            );
        }

        /// <summary>Null or empty collection.</summary>
        /// <param name="collection"> Collection null or empty. </param>
        /// <returns>Exception.</returns>
        public static Exception NullOrEmptyException (object collection)
        {
            return new Exception (
                string.Format (
                    format: "The collection {0} is null or empty.",
                    collection
                )
            );
        }

        /// <summary>Prefab is a null reference.</summary>
        /// <returns>Exception.</returns>
        public static Exception NullPrefabException () =>
            new Exception ("Empty prefab reference.");

        /// <summary>Instance is a null reference.</summary>
        /// <returns>Exception.</returns>
        public static Exception NullInstanceException () =>
            new Exception ("Empty instance reference.");

        #endregion
    }
}