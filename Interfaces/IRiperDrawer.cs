using TestTask.FrameControl;

namespace TestTask.Interfaces
{
    internal interface IRiperDrawer
    {
        void DrawRipers(PaintEventArgs e,string coordinate, ShapeType shapeType);
    }
}
