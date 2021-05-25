using mexTool.Tools;
using System;
using System.IO;
using System.IO.Compression;

namespace mexTool.Core.Installer
{
    public class MEXDolPatcher
    {
        /*
         * Patch Format
         * int offset
         * int length
         * byte[] data
         * until eof
         * 
         */

        // TODO: there is no error checking or anything
        // Assumes dol is 1.02


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vanilla"></param>
        /// <param name="patched"></param>
        public static void CreatePatch(byte[] vanilla, byte[] patched, string filePath)
        {
            using (FileStream s = new FileStream(filePath, FileMode.Create))
            using (DeflateStream w = new DeflateStream(s, CompressionLevel.Optimal))
            {
                w.Write(new byte[] { 0x44, 0x4F, 0x4C, 0x50 }, 0, 4);

                for (int i = 0; i < vanilla.Length; i++)
                {
                    int diff_end = i;

                    // check if diff stating
                    if (vanilla[i] != patched[i])
                    {
                        // linear future search
                        // if the data is the same for at least 0x20 bytes, then call it a day
                        int same = 0;
                        for (int j = i; j < vanilla.Length; j++)
                        {
                            if (vanilla[j] == patched[j])
                                same += 1;
                            else
                                same = 0;

                            if (same > 0x20)
                            {
                                diff_end = j - 0x20;
                                break;
                            }
                        }
                    }

                    // write diff patch
                    if (i != diff_end)
                    {
                        w.Write(BitConverter.GetBytes(i), 0, 4);
                        w.Write(BitConverter.GetBytes((diff_end - i)), 0, 4);
                        w.Write(patched, i, diff_end - i);
                    }

                    // set i to end of diff section
                    i = diff_end;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static byte[] ApplyPatch(byte[] dol, string filePath)
        {
            using (FileStream s = new FileStream(filePath, FileMode.Open))
            using (DeflateStream r = new DeflateStream(s, CompressionMode.Decompress))
            {
                // header check
                if (r.ReadByte() != 0x44 || r.ReadByte() != 0x4F || r.ReadByte() != 0x4C || r.ReadByte() != 0x50)
                    return dol;

                while (true)
                {
                    var offset = (r.ReadByte() & 0xFF) | ((r.ReadByte() & 0xFF) << 8) | ((r.ReadByte() & 0xFF) << 16) | ((r.ReadByte() & 0xFF) << 24);
                    var length = (r.ReadByte() & 0xFF) | ((r.ReadByte() & 0xFF) << 8) | ((r.ReadByte() & 0xFF) << 16) | ((r.ReadByte() & 0xFF) << 24);

                    if (offset == -1 && length == -1)
                        break;

                    for (int i = 0; i < length; i++)
                        dol[offset + i] = (byte)r.ReadByte();
                }
            }
            return dol;
        }

        /// <summary>
        /// Returns true if patch is already applied to dol
        /// </summary>
        /// <param name="dol"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool CheckPatchApplied(byte[] dol, string filePath)
        {
            using (FileStream s = new FileStream(filePath, FileMode.Open))
            using (DeflateStream r = new DeflateStream(s, CompressionMode.Decompress))
            {
                // header check
                if (r.ReadByte() != 0x44 || r.ReadByte() != 0x4F || r.ReadByte() != 0x4C || r.ReadByte() != 0x50)
                    return true;

                while (true)
                {
                    var offset = (r.ReadByte() & 0xFF) | ((r.ReadByte() & 0xFF) << 8) | ((r.ReadByte() & 0xFF) << 16) | ((r.ReadByte() & 0xFF) << 24);
                    var length = (r.ReadByte() & 0xFF) | ((r.ReadByte() & 0xFF) << 8) | ((r.ReadByte() & 0xFF) << 16) | ((r.ReadByte() & 0xFF) << 24);

                    if (offset == -1 && length == -1)
                        break;

                    for (int i = 0; i < length; i++)
                        if (dol[offset + i] != r.ReadByte())
                            return false;
                }
            }
            return true;
        }

    }
}
