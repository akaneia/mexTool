using HSDRaw;
using System;
using System.Collections.Generic;
using System.IO;

namespace mexTool.Core
{
    public class CodesManager
    {

        private List<Code> Codes = new List<Code>();

        public byte[] GenerateCodes(List<Code> additional)
        {
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriterExt r = new BinaryWriterExt(stream))
            {
                r.BigEndian = true;

                // write header
                r.Write(0x00D0C0DE);
                r.Write(0x00D0C0DE);

                // write mex codes
                WriteCodes(r, Codes);

                // write additional codes
                WriteCodes(r, additional);

                // write footer
                r.Write(0xF0000000);
                r.Write(0);

                return stream.ToArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="codes"></param>
        private void WriteCodes(BinaryWriterExt r, IEnumerable<Code> codes)
        {
            foreach (var c in codes)
            {
                // write code header
                r.Write(((uint)(c.OpCode & 0xFF) << 24) | (c.Offset & 0xFFFFFF));

                switch (c.OpCode)
                {

                    case 0x00: // 8 bits Write & Fill
                    case 0x02: // 16 bits Write & Fill
                    case 0x04: // 32 bits Write
                        {
                            r.Write(c.Data);
                        }
                        break;
                    case 0xC2: // asm
                        {
                            r.Write(c.Data.Length / 8);
                            r.Write(c.Data);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// returns true if code conflicts are found
        /// </summary>
        /// <param name="data"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool CheckCodeConflicts(byte[] data, out string error)
        {
            List<Code> codes = new List<Code>();

            error = "";

            if (TryLoadCodes(data, codes, false, out error))
            {
                // check for code conflicts with m-ex codes
                foreach (var c in Codes)
                {
                    foreach (var n in codes)
                    {
                        // offset conflict found
                        if (n.Offset == c.Offset)
                        {
                            error = $"MEX injection point conflict found with address {n.Offset.ToString("X")}";
                            return true;
                        }
                    }
                }

                // no conflict found
                return false;
            }

            // invalid code
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool LoadCodesGCT(byte[] data)
        {
            Codes.Clear();
            return TryLoadCodes(data, Codes, true, out string error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="codes"></param>
        /// <param name="is_gct"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool TryLoadCodes(byte[] data, IList<Code> codes, bool is_gct, out string error)
        {
            error = "";
            using (MemoryStream stream = new MemoryStream(data))
            using (BinaryReaderExt r = new BinaryReaderExt(stream))
            {
                r.BigEndian = true;

                // check header
                if (is_gct)
                    if (r.ReadUInt32() != 0x00D0C0DE || r.ReadUInt32() != 0x00D0C0DE)
                        return false;

                // parse file
                while (r.Position < r.Length)
                {
                    Code c = new Code();

                    var off = r.ReadUInt32();
                    c.OpCode = (byte)((off >> 24) & 0xFF);
                    c.Offset = 0x80000000 + (off & 0xFFFFFF);

                    if (c.OpCode == 0xF0)
                        break;

                    switch (c.OpCode)
                    {
                        case 0x00: // 8 bits Write & Fill
                        case 0x02: // 16 bits Write & Fill
                        case 0x04: // 32 bits Write
                            {
                                // length check
                                if (r.Position + 4 > r.Length)
                                {
                                    error = $"Op Code {c.OpCode.ToString("X2")} has incorrect length";
                                    return false;
                                }

                                c.Data = r.ReadBytes(4);
                            }
                            break;
                        //case 0x06: // String Write (Patch Code)
                        //case 0x08: // Slider/Multi Skip (Serial)
                        case 0xC2: // asm
                            {
                                // length check
                                if (r.Position + 4 > r.Length)
                                {
                                    error = $"Op Code {c.OpCode.ToString("X2")} has incorrect length";
                                    return false;
                                }

                                var length = r.ReadInt32();

                                // length check 2
                                if (r.Position + length * 8 > r.Length)
                                {
                                    error = $"Op Code {c.OpCode.ToString("X2")} has incorrect length";
                                    return false;
                                }

                                c.Data = r.ReadBytes(length * 8);
                            }
                            break;
                        default:
                            {
                                error = $"Warning: op code {c.OpCode.ToString("X2")} is not defined";
                                return false;
                            }
                    }

                    codes.Add(c);
                }

            }

            return true;
        }
    }

    public class Code
    {
        public byte OpCode;
        public uint Offset;
        public byte[] Data;

        public static bool TryParse(string input, out Code code)
        {
            code = null;
            return false;
        }
    }
}
