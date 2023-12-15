using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mexTool.Tools
{
    public class Hex
    {
        private static Regex RegHEX = new Regex(@"[0-9a-fA-F]+");

        /// <summary>
        /// For uppercase A-F letters:
        /// return val - (val < 58 ? 48 : 55);
        /// For lowercase a-f letters:
        /// return val - (val < 58 ? 48 : 87);
        /// Or the two combined, but a bit slower:
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static int GetHexVal(char hex)
        {
            int val = hex;
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        /// <summary>
        /// Take a hex string and converts to a byte array
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static byte[] StringToByteArray(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        /// <summary>
        /// Trims out ini comments and white spaces from a line of text
        /// </summary>
        /// <param name="line"></param>
        /// <param name="hexline"></param>
        /// <returns></returns>
        public static bool TrimHexLine(string line, out string hexline)
        {
            hexline = null;

            // check for empty line
            if (string.IsNullOrEmpty(line))
                return false;

            // trim comments and spaces
            var trimmed = Regex.Replace(Regex.Replace(line, "#.*", ""), @"\s+", "");

            // check valid length
            if (trimmed.Length != 16)
                return false;

            // check if valid code line
            if (!RegHEX.Match(trimmed).Success)
                return false;

            hexline = trimmed;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="bytesPerLine"></param>
        /// <returns></returns>
        public static string FormatByteArrayToHexLines(byte[] byteArray)
        {
            int bytesPerLine = 8;

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < byteArray.Length; i++)
            {
                if (i > 0 && i % bytesPerLine == 0)
                    result.Append(Environment.NewLine);

                result.Append(byteArray[i].ToString("X2"));

                // Add a space in the middle of the line
                if (i % bytesPerLine == (bytesPerLine / 2) - 1 && i % bytesPerLine != 0 && i != byteArray.Length - 1)
                    result.Append(" ");
            }

            return result.ToString();
        }
    }
}
