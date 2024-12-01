using DirectShowLib;

namespace TestTask.Interfaces
{
    internal interface ICamsValidateChecker
    {
        void CheckValidateCams(ToolStripComboBox cameraChoice, DsDevice[] cams);
    }
}
