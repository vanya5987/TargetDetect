using TestTask.Interfaces;

namespace TestTask.FrameControl
{
    public enum ShapeType
    {
        Cross,
        Square,
        Circle,
    }

    internal sealed class FiducialMarksDrawer : IFiducialMarksDrawler
    {
        private readonly List<Point> _landmarks;
        private readonly int _maxLandmarks;

        public FiducialMarksDrawer(List<Point> landmarks, int maxLandmarks)
        {
            _landmarks = landmarks ?? throw new ArgumentNullException(nameof(_landmarks));
            _maxLandmarks = maxLandmarks;

            if (_maxLandmarks < 0)
                throw new IndexOutOfRangeException(nameof(maxLandmarks));
        }

        public void DrawFiducialMarks(PaintEventArgs e, string coordinate, ShapeType shapeType)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            Graphics graphics = e.Graphics;
            Pen pen = Pens.Blue;
            int size = 10;

            foreach (var point in _landmarks)
            {
                DrawCross(graphics, pen, size, point, shapeType);
                DrawSquare(graphics, pen, size, point, shapeType);
                DrawCircle(graphics, pen, size, point, shapeType);
            }
        }

        private void DrawCross(Graphics graphics, Pen pen, int size, Point point, ShapeType shapeType)
        {
            if (shapeType == ShapeType.Cross)
            {
                graphics.DrawLine(pen, point.X - size, point.Y, point.X + size, point.Y);
                graphics.DrawLine(pen, point.X, point.Y - size, point.X, point.Y + size);
            }
        }

        private void DrawSquare(Graphics graphics, Pen pen, int size, Point point, ShapeType shapeType)
        {
            if (shapeType == ShapeType.Square)
                graphics.DrawRectangle(pen, point.X - size / 2, point.Y - size / 2, size, size);
        }

        private void DrawCircle(Graphics graphics, Pen pen, int size, Point point, ShapeType shapeType)
        {
            if (shapeType == ShapeType.Circle)
                graphics.DrawEllipse(pen, point.X - size / 2, point.Y - size / 2, size, size);
        }
    }
}
