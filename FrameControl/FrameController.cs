namespace TestTask.FrameControl
{
    internal class FrameController
    {
        private readonly List<Point> _landmarks;
        private int _maxLandmarks;

        public FrameController(List<Point> landmarks, int maxLandmarks)
        {
            _landmarks = landmarks ?? throw new ArgumentNullException(nameof(_landmarks));
            _maxLandmarks = maxLandmarks;

            if (_maxLandmarks < 0)
                throw new IndexOutOfRangeException(nameof(maxLandmarks));
        }

        public void DrawRipers(PaintEventArgs e, string pointCoordinateLabel)
        {
            Graphics graphics = e.Graphics;
            int size = 10;

            foreach (var point in _landmarks)
            {
                Pen pen = Pens.Blue;

                graphics.DrawLine(pen, point.X - size, point.Y, point.X + size, point.Y);
                graphics.DrawLine(pen, point.X, point.Y - size, point.X, point.Y + size);
            }
        }
    }
}
