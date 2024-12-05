using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using TestTask.Interfaces;

namespace TestTask.FindRectanglesAlgorithm
{
    internal sealed class TargetFinder : ITargetFinder
    {
        private readonly List<Point> _landmarks;

        public TargetFinder(List<Point> landmarks)
        {
            _landmarks = landmarks ?? throw new ArgumentNullException(nameof(landmarks));
        }

        public List<Rectangle> FindTarget(Bitmap input, int minWidth, int minHeight)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (_landmarks.Count < 2)
                throw new InvalidOperationException("Для определения зоны поиска необходимы как минимум два ориентира.");

            int leftBoundary = Math.Max(0, _landmarks.Min(p => p.X));
            int rightBoundary = Math.Min(input.Width, _landmarks.Max(p => p.X));
            int topBoundary = Math.Max(0, _landmarks.Min(p => p.Y));
            int bottomBoundary = Math.Min(input.Height, _landmarks.Max(p => p.Y));

            Rectangle searchArea = new Rectangle(leftBoundary, topBoundary, rightBoundary - leftBoundary, bottomBoundary - topBoundary);
            List<Rectangle> rectangles = new List<Rectangle>();
            BitmapData inputData = input.LockBits(new Rectangle(0, 0, input.Width, input.Height),
                                                  ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int bytesPerPixel = 4;
            int stride = inputData.Stride;
            IntPtr scan0 = inputData.Scan0;

            byte[] pixels = new byte[stride * input.Height];
            Marshal.Copy(scan0, pixels, 0, pixels.Length);

            bool[,] visited = new bool[input.Width, input.Height];

            for (int y = searchArea.Y; y < searchArea.Y + searchArea.Height; y++)
            {
                for (int x = searchArea.X; x < searchArea.X + searchArea.Width; x++)
                {
                    int index = (y * stride) + (x * bytesPerPixel);
                    if (pixels[index] == 255 && !visited[x, y])
                    {
                        int x1 = x, y1 = y;

                        while (x1 < searchArea.X + searchArea.Width && pixels[(y * stride) + (x1 * bytesPerPixel)] == 255) x1++;
                        while (y1 < searchArea.Y + searchArea.Height && pixels[(y1 * stride) + (x * bytesPerPixel)] == 255) y1++;

                        if ((x1 - x) >= minWidth && (y1 - y) >= minHeight)
                        {
                            Rectangle candidateRectangle = new Rectangle(x, y, x1 - x, y1 - y);
                            if (IsRectangleValid(candidateRectangle))
                            {
                                rectangles.Add(candidateRectangle);
                            }

                            for (int i = x; i < x1; i++)
                            {
                                for (int j = y; j < y1; j++)
                                {
                                    visited[i, j] = true;
                                }
                            }
                        }
                    }
                }
            }

            input.UnlockBits(inputData);

            if (rectangles.Count > 0)
            {
                Rectangle largestRectangle = rectangles.OrderByDescending(r => r.Width * r.Height).First();
                return new List<Rectangle> { largestRectangle };
            }

            return new List<Rectangle>();
        }

        private bool IsRectangleValid(Rectangle rectangle)
        {
            double aspectRatio = (double)rectangle.Width / rectangle.Height;

            if (aspectRatio < 0.8 || aspectRatio > 1.2)
                return false;

            var topLeft = new Point(rectangle.Left, rectangle.Top);
            var topRight = new Point(rectangle.Right, rectangle.Top);
            var bottomLeft = new Point(rectangle.Left, rectangle.Bottom);
            var bottomRight = new Point(rectangle.Right, rectangle.Bottom);

            double angle1 = CalculateAngle(topLeft, topRight, bottomRight);
            double angle2 = CalculateAngle(topRight, bottomRight, bottomLeft);
            double angle3 = CalculateAngle(bottomRight, bottomLeft, topLeft);
            double angle4 = CalculateAngle(bottomLeft, topLeft, topRight);

            if (Math.Abs(angle1 - 90) > 5 || Math.Abs(angle2 - 90) > 5 ||
                Math.Abs(angle3 - 90) > 5 || Math.Abs(angle4 - 90) > 5)
                return false;

            return true;
        }

        private double CalculateAngle(Point firstPoint, Point secondPoint, Point thirdPoint)
        {
            double dx1 = secondPoint.X - firstPoint.X;
            double dy1 = secondPoint.Y - firstPoint.Y;
            double dx2 = thirdPoint.X - secondPoint.X;
            double dy2 = thirdPoint.Y - secondPoint.Y;

            double dotProduct = dx1 * dx2 + dy1 * dy2;
            double magnitude1 = Math.Sqrt(dx1 * dx1 + dy1 * dy1);
            double magnitude2 = Math.Sqrt(dx2 * dx2 + dy2 * dy2);

            return Math.Acos(dotProduct / (magnitude1 * magnitude2)) * (180 / Math.PI);
        }

        private List<Rectangle> MergeOverlappingTargetsToSmallest(List<Rectangle> rectangles)
        {
            bool merged = true;
            while (merged)
            {
                merged = false;
                for (int i = 0; i < rectangles.Count; i++)
                {
                    for (int j = i + 1; j < rectangles.Count; j++)
                    {
                        if (rectangles[i].IntersectsWith(rectangles[j]))
                        {
                            rectangles[i] = GetSmallestEnclosingRectangle(rectangles[i], rectangles[j]);
                            rectangles.RemoveAt(j);
                            merged = true;
                            break;
                        }
                    }

                    if (merged)
                        break;
                }
            }

            return rectangles;
        }

        private Rectangle GetSmallestEnclosingRectangle(Rectangle firstRectangle, Rectangle secondRectangle)
        {
            int left = Math.Max(firstRectangle.Left, secondRectangle.Left);
            int top = Math.Max(firstRectangle.Top, secondRectangle.Top);
            int right = Math.Min(firstRectangle.Right, secondRectangle.Right);
            int bottom = Math.Min(firstRectangle.Bottom, secondRectangle.Bottom);

            if (right > left && bottom > top)
            {
                return new Rectangle(left, top, right - left, bottom - top);
            }

            return Rectangle.Empty;
        }
    }
}
