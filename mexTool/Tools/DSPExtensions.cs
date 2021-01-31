using CSCore;
using CSCore.Codecs.MP3;
using MeleeMedia.Audio;
using mexTool.GUI;
using System.IO;

namespace mexTool.Tools
{
    public static class DSPExtensions
    {
        public readonly static string SupportedImportFormatFilter = "Supported Audio Formats|*.mp3;*.dsp;*.hps;*.wav;*.aiff;*.wma;*.m4a";

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

                d.ShowDialog();
            }

            return dsp;
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
