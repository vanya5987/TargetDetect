using TestTask.FrameControl;

namespace TestTask.Interfaces
{
    internal interface IFiducialMarksDrawler
    {
        void DrawFiducialMarks(PaintEventArgs e,string coordinate, ShapeType shapeType);
    }
}
