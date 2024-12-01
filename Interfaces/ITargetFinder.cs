namespace TestTask.Interfaces
{
    internal interface ITargetFinder
    {
        List<Rectangle> FindTarget(Bitmap input, int minWidth, int minHeight);
    }
}
