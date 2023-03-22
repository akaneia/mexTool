using CSCore;
using CSCore.Codecs.MP3;
using HSDRaw;
using MeleeMedia.Audio;
using mexTool.GUI;
using System;
using System.IO;

namespace mexTool.Tools
{
    public static class DSPExtensions
    {
        public readonly static string SupportedImportFormatFilter = "Supported Audio Formats|*.mp3;*.dsp;*.hps;*.wav;*.aiff;*.wma;*.m4a;*.brstm;";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public static DSP FromFile(string filePath)
        {
            var format = Path.GetExtension(filePath);

            format = format.Replace(".", "").ToLower();

            var dsp = new DSP();

            switch (format)
            {
                case "dsp":
                case "hps":
                case "wav":
                    dsp = new DSP(filePath);
                    break;
                case "brstm":
                    dsp.FromBRSTM(filePath);
                    break;
                case "mp3":
                    dsp.FromMP3(File.ReadAllBytes(filePath));
                    break;
                case "aiff":
                    dsp.FromAIFF(File.ReadAllBytes(filePath));
                    break;
                case "wma":
                    dsp.FromWMA(File.ReadAllBytes(filePath));
                    break;
                case "m4a":
                    dsp.FromM4A(File.ReadAllBytes(filePath));
                    break;
            }

            using (var d = new SoundEditorDialog())
            {
                d.SetSound(dsp);

                if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    return dsp;
                else
                    return null;
            }
        }

        public enum BRSTM_CODEC
        {
            PCM_8bit,
            PCM16bit,
            ADPCM_4bit,
        }

        private static void FromBRSTM(this DSP dsp, string filePath)
        {
            using (FileStream s = new FileStream(filePath, FileMode.Open))
            using (BinaryReaderExt r = new BinaryReaderExt(s))
            {
                if (new string(r.ReadChars(4)) != "RSTM")
                    throw new NotSupportedException("File is not a valid BRSTM file");

                r.BigEndian = true;
                r.BigEndian = r.ReadUInt16() == 0xFEFF;

                r.Skip(2); // 01 00 version
                r.Skip(4); // filesize

                r.Skip(2); // 00 40 - header length 
                r.Skip(2); // 00 02 - header version 

                var headOffset = r.ReadUInt32();
                var headSize = r.ReadUInt32();

                var adpcOffset = r.ReadUInt32();
                var adpcSize = r.ReadUInt32();

                var dataOffset = r.ReadUInt32();
                var dataSize = r.ReadUInt32();


                // can skip adpc section when reading because it just contains sample history


                // parse head section
                // --------------------------------------------------------------
                r.Position = headOffset;
                if (new string(r.ReadChars(4)) != "HEAD")
                    throw new NotSupportedException("BRSTM does not have a valid HEAD");
                r.Skip(4); // section size


                r.Skip(4); // 01 00 00 00 marker
                var chunk1Offset = r.ReadUInt32() + 8 + headOffset;

                r.Skip(4); // 01 00 00 00 marker
                var chunk2Offset = r.ReadUInt32() + 8 + headOffset;

                r.Skip(4); // 01 00 00 00 marker
                var chunk3Offset = r.ReadUInt32() + 8 + headOffset;


                // --------------------------------------------------------------
                r.Seek(chunk1Offset);
                var codec = (BRSTM_CODEC)r.ReadByte();
                var loopFlag = r.ReadByte();
                var channelCount = r.ReadByte();
                r.Skip(1); // padding

                if (codec != BRSTM_CODEC.ADPCM_4bit)
                    throw new NotSupportedException("only 4bit ADPCM files currently supported");

                var sampleRate = r.ReadUInt16();
                r.Skip(2); // padding

                dsp.Frequency = sampleRate;

                var loopStart = r.ReadUInt32();
                var totalSamples = r.ReadUInt32();

                var dataPointer = r.ReadUInt32(); // DATA offset
                int blockCount = r.ReadInt32();

                var blockSize = r.ReadUInt32();
                var samplesPerBlock = r.ReadInt32();

                var sizeOfFinalBlock = r.ReadUInt32();
                var samplesInFinalBlock = r.ReadInt32();

                var sizeOfFinalBlockWithPadding = r.ReadUInt32();

                var samplesPerEntry = r.ReadInt32();
                var bytesPerEntry = r.ReadInt32();

                // --------------------------------------------------------------
                r.Seek(chunk2Offset);
                var numOfTracks = r.ReadByte();
                var trackDescType = r.ReadByte();
                r.Skip(2); // padding

                for(uint i = 0; i < numOfTracks; i++)
                {
                    r.Seek(chunk1Offset + 4 + 8 * i);
                    r.Skip(1); // 01 padding
                    var descType = r.ReadByte();
                    r.Skip(2); // padding
                    var descOffset = r.ReadUInt32() + 8 + headOffset;

                    r.Seek(descOffset);
                    switch (descType)
                    {
                        case 0:
                            {
                                int channelsInTrack = r.ReadByte();
                                int leftChannelID = r.ReadByte();
                                int rightChannelID = r.ReadByte();
                                r.Skip(1); // padding
                            }
                            break;
                        case 1:
                            {
                                var volume = r.ReadByte();
                                var panning = r.ReadByte();
                                r.Skip(2); // padding
                                r.Skip(4); // padding
                                int channelsInTrack = r.ReadByte();
                                int leftChannelID = r.ReadByte();
                                int rightChannelID = r.ReadByte();
                                r.Skip(1); // 01 padding
                            }
                            break;
                    }
                }

                // --------------------------------------------------------------
                r.Seek(chunk3Offset);

                var channelCountAgain = r.ReadByte();
                r.Skip(3);

                for (uint i = 0; i < channelCountAgain; i++)
                {
                    r.Seek(chunk3Offset + 4 + 8 * i);

                    r.Skip(4); // 01000000 marker
                    var offset = r.ReadUInt32() + headOffset + 8;

                    r.Seek(offset);

                    // channel information
                    var channel = new DSPChannel();
                    dsp.Channels.Add(channel);
                    channel.LoopFlag = loopFlag;
                    channel.LoopStart = (int)loopStart;

                    r.Skip(4); // 01000000 marker
                    r.Skip(4); // offset to coefficients (they follow directly after)

                    for (int k = 0; k < 0x10; k++)
                        channel.COEF[k] = r.ReadInt16();
                    channel.Gain = r.ReadInt16();
                    channel.InitialPredictorScale = r.ReadInt16();
                    channel.InitialSampleHistory1 = r.ReadInt16();
                    channel.InitialSampleHistory2 = r.ReadInt16();
                    channel.LoopPredictorScale = r.ReadInt16();
                    channel.LoopSampleHistory1 = r.ReadInt16();
                    channel.LoopSampleHistory2 = r.ReadInt16();
                    r.Skip(2); // padding

                    // get channel data
                    using (MemoryStream channelStream = new MemoryStream())
                    {
                        for (uint j = 0; j < blockCount; j++)
                        {
                            var bs = blockSize;
                            var actualBlockSize = blockSize;

                            if (j == blockCount - 1)
                            {
                                bs = sizeOfFinalBlockWithPadding;
                                actualBlockSize = sizeOfFinalBlock;
                            }

                            channelStream.Write(r.GetSection(dataPointer + j * (blockSize * channelCountAgain) + bs * i, (int)actualBlockSize), 0, (int)actualBlockSize);
                        }

                        channel.Data = channelStream.ToArray();
                        channel.NibbleCount = channel.Data.Length * 2;
                    }
                }


                dsp.LoopPoint = TimeSpan.FromMilliseconds(loopStart / (double)sampleRate * 1000).ToString();
            }
        }

        private static void FromMP3(this DSP dsp, byte[] data)
        {
            using (MemoryStream s = new MemoryStream(data))
            using (IWaveSource soundSource = new DmoMp3Decoder(s))
            using (MemoryStream w = new MemoryStream())
            {
                soundSource.WriteToWaveStream(w);
                dsp.FromWAVE(w.ToArray());
            }
        }

        private static void FromM4A(this DSP dsp, byte[] data)
        {
            using (MemoryStream s = new MemoryStream(data))
            using (IWaveSource soundSource = new CSCore.Codecs.DDP.DDPDecoder(s))
            using (MemoryStream w = new MemoryStream())
            {
                soundSource.WriteToWaveStream(w);
                dsp.FromWAVE(w.ToArray());
            }
        }

        private static void FromWMA(this DSP dsp, byte[] data)
        {
            using (MemoryStream s = new MemoryStream(data))
            using (IWaveSource soundSource = new CSCore.Codecs.WMA.WmaDecoder(s))
            using (MemoryStream w = new MemoryStream())
            {
                soundSource.WriteToWaveStream(w);
                dsp.FromWAVE(w.ToArray());
            }
        }

        private static void FromAIFF(this DSP dsp, byte[] data)
        {
            using (MemoryStream s = new MemoryStream(data))
            using (IWaveSource soundSource = new CSCore.Codecs.AIFF.AiffReader(s))
            using (MemoryStream w = new MemoryStream())
            {
                soundSource.WriteToWaveStream(w);
                dsp.FromWAVE(w.ToArray());
            }
        }
    }
}
