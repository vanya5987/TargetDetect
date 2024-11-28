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

            if (_landmarks.Count == _maxLandmarks)
            {
                int centerX = (_landmarks[0].X + _landmarks[1].X + _landmarks[2].X + _landmarks[3].X) / 4;
                int centerY = (_landmarks[0].Y + _landmarks[1].Y + _landmarks[2].Y + _landmarks[3].Y) / 4;

                Brush brush = Brushes.Red;
                graphics.FillEllipse(brush, centerX - 5, centerY - 5, size, size);

                pointCoordinateLabel = $"X = {centerX}; Y = {centerY}";
            }
        }

        //Ищет центр найденого прямоугольника
        public static Point GetRectangleCenter(Rectangle rect)
        {
            int centerX = rect.Left + rect.Width / 2;
            int centerY = rect.Top + rect.Height / 2;
            return new Point(centerX, centerY);
        }
    }
}
