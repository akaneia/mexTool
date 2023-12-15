using HSDRaw;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace mexTool.Tools
{
    public class CodeLoader
    {
        /// <summary>
        /// Extracts Codes from INI file
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IEnumerable<Codes> FromINI(byte[] data)
        {
            using (MemoryStream s = new MemoryStream(data))
            using (StreamReader r = new StreamReader(s))
            {
                Codes c = null;
                StringBuilder src = null;

                while (!r.EndOfStream)
                {
                    var line = r.ReadLine();

                    if (line.StartsWith("$"))
                    {
                        if (c != null && src.Length > 0)
                        {
                            c.SetSource(src.ToString(), out CodesError error);
                            yield return c;
                        }

                        c = new Codes();
                        src = new StringBuilder();

                        var l = line.Substring(1);

                        if (l.StartsWith("!"))
                        {
                            c.SetCheckState(true);
                            l = l.Substring(1);
                        }
                        else
                        {
                            c.SetCheckState(false);
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
                            if (Hex.TrimHexLine(line, out string hexline))
                            {
                                src.AppendLine(line);
                            }
                        }
                    }
                }

                if (c != null && src.Length > 0)
                {
                    c.SetSource(src.ToString(), out CodesError error);
                    yield return c;
                }
            }
        }

        /// <summary>
        /// Packs codes into INI file
        /// </summary>
        /// <returns></returns>
        public static byte[] ToINI(IEnumerable<Codes> codes)
        {
            using (MemoryStream s = new MemoryStream())
            using (StreamWriter w = new StreamWriter(s) { AutoFlush = true })
            {
                foreach (var c in codes)
                {
                    w.WriteLine($"${(c.IsChecked() ? "!" : "")}{c.Name}{(string.IsNullOrEmpty(c.Creator) ? "" : $" [{c.Creator}]")}");

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

                    w.WriteLine(c.GetSource());
                }

                return s.ToArray();
            }
        }


        /// <summary>
        /// Extracts Code from GCT file
        /// </summary>
        /// <param name="data"></param>
        /// <param name="codes"></param>
        /// <param name="is_gct"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static Codes FromGCT(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            using (BinaryReaderExt r = new BinaryReaderExt(stream))
            {
                r.BigEndian = true;

                // check header
                if (r.ReadUInt32() != 0x00D0C0DE || r.ReadUInt32() != 0x00D0C0DE)
                    return null;

                // create new code
                Codes c = new Codes()
                {
                    Name = "gct",
                };

                // parse file
                c.SetCompiled(r.ReadBytes((int)(r.Length - r.Position - 8)));

                return c;
            }
        }
        
        /// <summary>
        /// Packs given codes into a GCT file
        /// </summary>
        /// <param name="additional"></param>
        /// <returns></returns>
        public static byte[] ToGCT(IEnumerable<Codes> codes)
        {
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriterExt r = new BinaryWriterExt(stream))
            {
                r.BigEndian = true;

                // write header
                r.Write(0x00D0C0DE);
                r.Write(0x00D0C0DE);

                foreach (var c in codes)
                {
                    var comp = c.GetCompiled();
                    if (comp != null)
                        r.Write(comp);
                }

                // write footer
                r.Write(0xF0000000);
                r.Write(0);

                return stream.ToArray();
            }
        }
    }
}
