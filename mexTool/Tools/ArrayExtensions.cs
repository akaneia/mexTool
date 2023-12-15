using System;

namespace mexTool.Tools
{
    public class ArrayExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalArray"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <param name="replacementArray"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] ReplaceRange(byte[] originalArray, int startIndex, int length, byte[] replacementArray)
        {
            if (startIndex < 0 || startIndex >= originalArray.Length || length < 0 || startIndex + length > originalArray.Length)
            {
                throw new ArgumentException("Invalid start index or length");
            }

            byte[] result = new byte[originalArray.Length - length + replacementArray.Length];

            // Copy the bytes before the range to be replaced
            Array.Copy(originalArray, 0, result, 0, startIndex);

            // Copy the replacement bytes
            Array.Copy(replacementArray, 0, result, startIndex, replacementArray.Length);

            // Copy the bytes after the replaced range
            Array.Copy(originalArray, startIndex + length, result, startIndex + replacementArray.Length, originalArray.Length - (startIndex + length));

            return result;
        }
    }
}
