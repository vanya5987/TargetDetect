namespace TestTask.Interfaces
{
    internal interface IFiducialMarkMoveController
    {
        void ClickToLandmarks(MouseEventArgs e);
        void MoveLandmarks(MouseEventArgs e, PictureBox pictureBox);
        void ThrowLandmarks(MouseEventArgs e);
        void SetLandmarks(PictureBox pictureBox, ITargetValidateChecker validateChecker, MouseEventArgs e);
    }
}
