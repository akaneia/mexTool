using CSCore.CoreAudioAPI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace mexTool
{
    public class ApplicationSettings
    {
        private static List<MMDevice> AudioDevices = new List<MMDevice>();

        public static string ExecutablePath { get; internal set; }

        public static MMDevice DefaultDevice { get; set; }

        private static PrivateFontCollection _privateFontCollection = new PrivateFontCollection();

        public static FontFamily GetFontFamilyByName(string name)
        {
            return _privateFontCollection.Families.FirstOrDefault(x => x.Name == name);
        }

        private static void AddFont(string fullFileName)
        {
            AddFont(File.ReadAllBytes(fullFileName));
        }

        private static void AddFont(byte[] fontBytes)
        {
            var handle = GCHandle.Alloc(fontBytes, GCHandleType.Pinned);
            IntPtr pointer = handle.AddrOfPinnedObject();
            try
            {
                _privateFontCollection.AddMemoryFont(pointer, fontBytes.Length);
            }
            finally
            {
                handle.Free();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Init()
        {
            ExecutablePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            using (var mmdeviceEnumerator = new MMDeviceEnumerator())
            {
                using (var mmdeviceCollection = mmdeviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active))
                {
                    foreach (var device in mmdeviceCollection)
                    {
                        AudioDevices.Add(device);
                    }
                }
                DefaultDevice = mmdeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            }

            AddFont(Path.Combine(ExecutablePath, "lib/A-OTF_Folk_Pro_H.otf"));
            AddFont(Path.Combine(ExecutablePath, "lib/Palatino Linotype.ttf"));
        }
    }
}
