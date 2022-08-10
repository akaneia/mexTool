using HSDRaw.MEX;
using mexTool.Core.Installer;

namespace mexTool.Core.Updates
{
    public class UpdateV_1_1 : IMEXUpdate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte GetUpdateMajor()
        {
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte GetUpdateMinor()
        {
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mex"></param>
        /// <param name="_imageResource"></param>
        public void Update(MEX_Data mex, ImageResource _imageResource)
        {
            // resize and append meta data
            mex.MetaData._s.Resize(mex.MetaData.TrimmedSize);
            mex.MetaData.TrophyCount = 0x125;
            mex.MetaData.TrophySDOffset = 302;

            // generate trophy lookup tables
            using (MEXDOLScrubber dol = new MEXDOLScrubber(_imageResource.GetDOL()))
            {
                MEX_FighterData fd = new MEX_FighterData();
                dol.ExtractDataFromMap(fd);
                mex.FighterData._s.Resize(mex.FighterData.TrimmedSize);
                mex.FighterData.AllStarTrophyLookup = fd.AllStarTrophyLookup;
                mex.FighterData.ClassicTrophyLookup = fd.ClassicTrophyLookup;
                mex.FighterData.AdventureTrophyLookup = fd.AdventureTrophyLookup;
                mex.FighterData.EndingFallScale = fd.EndingFallScale;
            }
        }
    }
}
