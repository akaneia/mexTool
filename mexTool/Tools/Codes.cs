using mexTool.GUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace mexTool.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public class CodesError
    {
        public string Description;

        public int LineIndex;
    }

    /// <summary>
    /// 
    /// </summary>
    public class Codes : ICheckable
    {
        private bool _enabled = true;

        public string Name { get; set; } = "";

        public string Creator { get; set; } = "";

        public string Description { get; set; } = "";

        private string _source;

        private byte[] _compiled;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public bool SetSource(string source, out CodesError error)
        {
            _source = source;
            return TryCompileCode(out error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSource()
        {
            return _source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void SetCompiled(byte[] data)
        {
            _source = Hex.FormatByteArrayToHexLines(data);
            TryCompileCode(out CodesError error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetCompiled()
        {
            if (_compiled == null)
                TryCompileCode(out CodesError error);

            return _compiled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsChecked()
        {
            return _enabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void SetCheckState(bool state)
        {
            _enabled = state;
        }

        /// <summary>
        /// Returns a list of used addresses
        /// </summary>
        /// <returns></returns>
        public IEnumerable<uint> UsedAddresses()
        {
            var code = GetCompiled();

            if (code == null)
                yield break;

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
                    default:
                        throw new NotSupportedException($"Code type unknown: 0x{code[i].ToString("X2")}");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool TryCompileCode(out CodesError error)
        {
            _compiled = null;
            error = new CodesError();
            error.Description = "";
            error.LineIndex = 0;

            if (_source == null)
                return false;

            var lines = _source.Split(
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
                if (Hex.TrimHexLine(l, out string hexline))
                {
                    data.AddRange(Hex.StringToByteArray(hexline));
                }
                else
                {
                    error.Description = "Invalid HEX Format";
                    return false;
                }
                error.LineIndex += 1;
            }

            try
            {
                _compiled = CompressCode(data.ToArray());
                return true;
            } catch(Exception e)
            {
                _compiled = null;
                error.Description = e.ToString();
                return false;
            }
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
                                code = ArrayExtensions.ReplaceRange(code, start, 8 + count * 8, comp);
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
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
