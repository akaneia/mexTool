using HSDRaw.MEX;

namespace mexTool.Core.Updates
{
    public interface IMEXUpdate
    {
        byte GetUpdateMajor();

        byte GetUpdateMinor();

        void Update(MEX_Data mex, ImageResource resource);
    }
}
