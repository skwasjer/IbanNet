namespace IbanNet.Extensions
{
    internal static class ArrayExtensions
    {
        /// <summary>
        /// Fills each <paramref name="array"/> element with the value specified in <paramref name="fillWith"/>
        /// </summary>
        /// <typeparam name="T">The array element type.</typeparam>
        /// <param name="array">The array to fill.</param>
        /// <param name="fillWith">The element to fill the array with.</param>
        /// <returns>The filled array.</returns>
        public static T[] Fill<T>(this T[] array, T fillWith)
        {
#if NETSTANDARD2_1 || NET5_0_OR_GREATER
            Array.Fill(array, fillWith);
            return array;
#else
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = fillWith;
            }

            return array;
#endif
        }
    }
}
