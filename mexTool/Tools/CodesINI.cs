using mexTool.GUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace mexTool.Tools
{
    internal class HexTools
    {
        private static Regex RegHEX = new Regex(@"[0-9a-fA-F]+");

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
    }

    public class AddCodeError
    {
        public string Description;

        public int LineIndex;
    }

    public class AddCode : ICheckable
    {
        public bool Enabled = false;

        public string Name = "";

        public string Creator = "";

        public string Description = "";

        public string Code = "";

        public bool IsChecked()
        {
            return Enabled;
        }

        public void SetCheckState(bool state)
        {
            Enabled = state;
        }

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Returns a list of used addresses
        /// </summary>
        /// <returns></returns>
        public IEnumerable<uint> UsedAddresses()
        {
            if (TryCompileCode(out byte[] code, out AddCodeError error))
            {
                for (int i = 0; i < code.Length;)
                {
                    switch (code[i])
                    {
                        case 0x04:
                            {
                                yield return (uint)(0x80000000 | ((((code[i + 1] & 0xFF) << 16)) | (((code[i + 2] & 0xFF) << 8)) | ((code[i + 3] & 0xFF))));
                                i += 8;
                            }
                            break;
                        case 0xC2:
                            {
                                yield return (uint)(0x80000000 | ((((code[i + 1] & 0xFF) << 16)) | (((code[i + 2] & 0xFF) << 8)) | ((code[i + 3] & 0xFF))));
                                i += 4;
                                i += ((((code[i] & 0xFF) << 24)) | (((code[i + 1] & 0xFF) << 16)) | (((code[i + 2] & 0xFF) << 8)) | (code[i + 3] & 0xFF)) * 8 + 4;
                            }
                            break;
                        //default:
                        //    throw new NotSupportedException($"Code type unknown: 0x{code[i].ToString("X2")}");
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool TryCompileCode(out byte[] code, out AddCodeError error)
        {
            code = null;
            error = new AddCodeError();
            error.Description = "";
            error.LineIndex = 0;

            var lines = Code.Split(
                new string[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
                );

            List<byte> data = new List<byte>();

            foreach (var l in lines)
            {
                if (string.IsNullOrEmpty(l))
                {
                    error.LineIndex += 1;
                    continue;
                }

                // remove spaces
                if (HexTools.TrimHexLine(l, out string hexline))
                {
                    data.AddRange(StringToByteArray(hexline));
                }
                else
                {
                    error.Description = "Invalid HEX Format";
                    return false;
                }
                error.LineIndex += 1;
            }

            code = CompressCode(data.ToArray());
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static byte[] CompressCode(byte[] code)
        {
            for (int i = 0; i < code.Length;)
            {
                switch (code[i])
                {
                    case 0x04:
                        {
                            i += 8;
                        }
                        break;
                    case 0xC2:
                        {
                            int start = i;
                            int count = (((code[i + 4] & 0xFF) << 24)) | (((code[i + 5] & 0xFF) << 16)) | (((code[i + 6] & 0xFF) << 8)) | (code[i + 7] & 0xFF);
                            if (count == 1)
                            {
                                // compress this code
                                var comp = new byte[]
                                    {
                                        0x04, code[i + 1], code[i + 2], code[i + 3],
                                        code[i + 8], code[i + 9], code[i + 10], code[i + 11]
                                    };
                                code = ReplaceRange(code, start, 8 + count * 8, comp);
                                i += 8;
                            }
                            else
                            {
                                i += 8 * (count + 1);
                            }
                        }
                        break;
                    default:
                        throw new NotSupportedException($"Code type unknown: 0x{code[i].ToString("X2")}");
                }
            }

            return code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalArray"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <param name="replacementArray"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static byte[] ReplaceRange(byte[] originalArray, int startIndex, int length, byte[] replacementArray)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static byte[] StringToByteArray(string hex)
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
        /// 
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static int GetHexVal(char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }
    }

    public class CodesINI
    {
        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<AddCode> LoadCodeINI(byte[] data)
        {
            using (MemoryStream s = new MemoryStream(data))
            using (StreamReader r = new StreamReader(s))
            {
                AddCode c = null;

                while (!r.EndOfStream)
                {
                    var line = r.ReadLine();

                    if (line.StartsWith("$"))
                    {
                        if (c != null && c.Code.Length > 0)
                            yield return c;

                        c = new AddCode();

                        var l = line.Substring(1);

                        if (l.StartsWith("!"))
                        {
                            c.Enabled = true;
                            l = l.Substring(1);
                        }

                        var name = Regex.Match(l, @"(?<=\[).+?(?=\]\s*$)");

                        if (name.Success)
                        {
                            c.Name = l.Substring(0, name.Groups[0].Index - 1).Trim();
                            c.Creator = name.Value;
                        }
                        else
                        {
                            c.Name = l;
                            c.Creator = "";
                        }
                    }
                    else
                    if (c != null)
                    {
                        if (line.StartsWith("*"))
                        {
                            c.Description += line.Substring(1).Trim() + Environment.NewLine;
                        }
                        else
                        {
                            if (HexTools.TrimHexLine(line, out string hexline))
                            {
                                c.Code += line + Environment.NewLine;
                            }
                        }
                    }
                }

                if (c != null && c.Code.Length > 0)
                    yield return c;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static byte[] GenerateCodeINI(IEnumerable<AddCode> codes)
        {
            using (MemoryStream s = new MemoryStream())
            using (StreamWriter w = new StreamWriter(s) { AutoFlush = true })
            {
                foreach (var c in codes)
                {
                    w.WriteLine($"${(c.Enabled ? "!" : "")}{c.Name}{(string.IsNullOrEmpty(c.Creator) ? "" : $" [{c.Creator}]")}");

                    var desc_lines = c.Description.Split(
                        new string[] { "\r\n", "\r", "\n" },
                        StringSplitOptions.None
                        );

                    foreach (var l in desc_lines)
                    {
                        if (string.IsNullOrEmpty(l))
                            continue;
                        w.WriteLine($"*{l}");
                    }

                    w.WriteLine(c.Code);
                }

                return s.ToArray();
            }
        }
    }
}
