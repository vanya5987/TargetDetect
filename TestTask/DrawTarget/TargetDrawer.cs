using TestTask.Interfaces;

namespace TestTask.DrawTarget
{
    internal sealed class TargetDrawer : ITargetsDrawer
    {
        private ToolStripLabel _coordinateLabel;

        public TargetDrawer(ToolStripLabel coordinateLabel)
        {
            _coordinateLabel = coordinateLabel ?? throw new ArgumentNullException(nameof(_coordinateLabel));
        }

        public Bitmap DrawTargets(Bitmap inputBitmap, List<Rectangle> rectangles)
        {
            if (inputBitmap == null)
                throw new ArgumentNullException(nameof(inputBitmap));

            Bitmap resultBitmap = new Bitmap(inputBitmap);

            using (Graphics graphics = Graphics.FromImage(resultBitmap))
            {
                using (Pen redPen = new Pen(Color.Red, 2))
                {
                    foreach (var rectangle in rectangles)
                    {
                        graphics.DrawRectangle(redPen, rectangle);
                        _coordinateLabel.Text = DrawCenterPoint(graphics, rectangle);
                    }
                }
            }

            return resultBitmap;
        }

        private string DrawCenterPoint(Graphics graphics, Rectangle rectangle)
        {
            int centerX = rectangle.X + rectangle.Width / 2;
            int centerY = rectangle.Y + rectangle.Height / 2;
            int pointSize = 5;

            Rectangle pointRect = new Rectangle(centerX - pointSize / 2, centerY - pointSize / 2, pointSize, pointSize);

            using (Brush redBrush = new SolidBrush(Color.Red))
            {
                graphics.FillRectangle(redBrush, pointRect);
            }

            return $"X = {centerX}; Y = {centerY}";
        }
    }
}
