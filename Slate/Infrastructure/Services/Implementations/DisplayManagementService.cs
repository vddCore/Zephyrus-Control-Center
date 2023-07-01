using Slate.Infrastructure.Native;

namespace Slate.Infrastructure.Services.Implementations
{
    public class DisplayManagementService : IDisplayManagementService
    {
        public bool SetInternalDisplayRefreshRate(uint refreshRate)
        {
            if (User32.QueryDisplayConfig(
                    User32.QDC.ONLY_ACTIVE_PATHS,
                    out var pathInfos,
                    out var modeInfos,
                    out _
                ) != 0)
            {
                return false;
            }

            for (var i = 0; i < pathInfos.Length; i++)
            {
                var path = pathInfos[i];

                if ((path.flags & 1) != 0
                    && (path.targetInfo.outputTechnology & User32.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.INTERNAL) != 0)
                {
                    for (var j = 0; j < modeInfos.Length; j++)
                    {
                        var mode = modeInfos[j];

                        if (mode.infoType == User32.DISPLAYCONFIG_MODE_INFO_TYPE.TARGET &&
                            mode.id == path.targetInfo.id)
                        {
                            modeInfos[j].targetMode.targetVideoSignalInfo.vSyncFreq.Numerator = refreshRate;
                        }
                    }
                }
            }

            return User32.SetDisplayConfig(
                (uint)pathInfos.Length,
                pathInfos,
                (uint)modeInfos.Length,
                modeInfos,
                User32.SDC.APPLY
                | User32.SDC.USE_SUPPLIED_DISPLAY_CONFIG
                | User32.SDC.SAVE_TO_DATABASE
            ) == 0;
        }

        public uint GetInternalDisplayRefreshRate()
        {
            if (User32.QueryDisplayConfig(
                    User32.QDC.ONLY_ACTIVE_PATHS,
                    out var pathInfos,
                    out var modeInfos,
                    out _
                ) == 0)
            {
                for (var i = 0; i < pathInfos.Length; i++)
                {
                    var path = pathInfos[i];

                    if ((path.flags & 1) != 0
                        && (path.targetInfo.outputTechnology
                            & User32.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.INTERNAL) != 0)
                    {
                        for (var j = 0; j < modeInfos.Length; j++)
                        {
                            var mode = modeInfos[j];

                            if (mode.infoType == User32.DISPLAYCONFIG_MODE_INFO_TYPE.TARGET &&
                                mode.id == path.targetInfo.id)
                            {
                                return modeInfos[j].targetMode.targetVideoSignalInfo.vSyncFreq.Numerator;
                            }
                        }
                    }
                }
            }

            return 0;
        }
    }
}