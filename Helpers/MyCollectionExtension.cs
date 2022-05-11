using System;

#nullable enable
namespace Helpers
{
    public static class MyCollectionExtension
    {
        //https://stackoverflow.com/questions/1738990/initializing-jagged-arrays
		public static T? CreateJaggedArray<T>(params int[] lengths)
			=> (T?)InitializeJaggedArray(typeof(T).GetElementType(), 0, lengths);

        static object InitializeJaggedArray(Type type, int index, int[] lengths)
        {
            Array array = Array.CreateInstance(type, lengths[index]);
            Type elementType = type.GetElementType();

            if (elementType != null)
            {
                for (int i = 0; i < lengths[index]; i++)
                {
                    array.SetValue(
                        InitializeJaggedArray(elementType, index + 1, lengths), i);
                }
            }

            return array;
        }

		public static bool CheckJaggedArray2IfIndexIsValid<T>(this T[][] array, int x, int y)
			=> array != null && ValidateCoordinates(x, y, array[0].Length, array.Length);

		public static bool ValidateCoordinates(int x, int y, int width, int height)
			=> x >= 0 && x < width && y >= 0 && y < height;
    }
}
