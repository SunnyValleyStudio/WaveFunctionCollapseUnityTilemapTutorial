using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

public static class HashCodeCalculator 
{
    //https://support.microsoft.com/da-dk/help/307020/how-to-compute-and-compare-hash-values-by-using-visual-c
		public static string CalculateHashCode(int[][] grid)
			=> ByteArrayToString(MD5.Create().ComputeHash(grid.SelectMany(GetByteArrayFromIntArray).ToArray()));

		private static IEnumerable<byte> GetByteArrayFromIntArray(int[] tArray)
			=> tArray.SelectMany(BitConverter.GetBytes);

		private static string ByteArrayToString(byte[] byteArray)
			=> string.Join(null, byteArray.Select(b => b.ToString("X2")));
}
