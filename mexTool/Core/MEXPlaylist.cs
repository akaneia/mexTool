﻿using HSDRaw.MEX.Sounds;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using YamlDotNet.Serialization;

namespace mexTool.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class MEXPlaylist
    {
        public List<MEXPlaylistEntry> Entries = new List<MEXPlaylistEntry>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="music"></param>
        public void RemoveMusic(MEXMusic music)
        {
            Entries.RemoveAll(e => e.Music == music);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playlist"></param>
        public void FromPlaylistStruct(MEX_Playlist playlist)
        {
            if (playlist.MenuPlaylist != null && playlist.MenuPlayListCount > 0)
                Entries.AddRange(playlist.MenuPlaylist.Array.Select(e => new MEXPlaylistEntry() { Music = MEX.BackgroundMusic[e.HPSID], PlayChance = e.ChanceToPlay }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MEX_Playlist ToPlaylistStruct()
        {
            MEX_Playlist pl = new MEX_Playlist();
            if(Entries.Count > 0)
            {
                pl.MenuPlaylist = new HSDRaw.HSDArrayAccessor<MEX_PlaylistItem>();
                pl.MenuPlayListCount = Entries.Count;
                pl.MenuPlaylist.Array = Entries.Select(e => new MEX_PlaylistItem() { HPSID = (ushort)MEX.BackgroundMusic.IndexOf(e.Music), ChanceToPlay = (short)e.PlayChance }).ToArray();
            }
            return pl;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MEXPlaylistEntry
    {
        [YamlIgnore]
        public MEXMusic Music { get; set; } = new MEXMusic();

        [Browsable(false)]
        public string MusicLabel { get => Music.Label; set => Music.Label = value; }

        [Browsable(false)]
        public string MusicFileName { get => Music.FileName; set => Music.FileName = value; }

        public int PlayChance{ get => _playChance; set => _playChance = Math.Max(0, Math.Min(value, 100)); }
        private int _playChance = 0;

    }
}
