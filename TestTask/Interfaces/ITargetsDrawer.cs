namespace TestTask.Interfaces
{
    internal interface ITargetsDrawer
    {
        Bitmap DrawTargets(Bitmap inputBitmap, List<Rectangle> rectangles);
    }
}
