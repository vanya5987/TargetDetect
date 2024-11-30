using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TestTask.FindRectanglesAlgorithm
{
    internal class RectanglesFinder
    {
        private readonly List<Point> _landmarks;

        public RectanglesFinder(List<Point> landmarks)
        {
            _landmarks = landmarks ?? throw new ArgumentNullException(nameof(landmarks));
        }

        public List<Rectangle> FindRectangles(Bitmap input, int minWidth, int minHeight)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (_landmarks.Count < 2)
                throw new InvalidOperationException("Для определения зоны поиска необходимы как минимум два ориентира.");

            int leftBoundary = _landmarks.Min(p => p.X);
            int rightBoundary = _landmarks.Max(p => p.X);
            int topBoundary = _landmarks.Min(p => p.Y);
            int bottomBoundary = _landmarks.Max(p => p.Y);

            leftBoundary = Math.Max(0, leftBoundary);
            topBoundary = Math.Max(0, topBoundary);
            rightBoundary = Math.Min(input.Width, rightBoundary);
            bottomBoundary = Math.Min(input.Height, bottomBoundary);

            Rectangle searchArea = new Rectangle(
                leftBoundary,
                topBoundary,
                rightBoundary - leftBoundary,
                bottomBoundary - topBoundary
            );

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
                            rectangles.Add(new Rectangle(x, y, x1 - x, y1 - y));

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

            rectangles = MergeOverlappingRectangles(rectangles);

            Rectangle largestRectangle = rectangles.OrderByDescending(r => r.Width * r.Height).FirstOrDefault();

            return largestRectangle == Rectangle.Empty ? new List<Rectangle>() : new List<Rectangle> { largestRectangle };
        }

        private List<Rectangle> MergeOverlappingRectangles(List<Rectangle> rectangles)
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
                            rectangles[i] = Rectangle.Union(rectangles[i], rectangles[j]);
                            rectangles.RemoveAt(j);
                            merged = true;
                            break;
                        }
                    }
                    if (merged) break;
                }
            }

            return rectangles;
        }

        public Bitmap DrawRectanglesOnBitmap(Bitmap inputBitmap, List<Rectangle> rectangles)
        {
            if (inputBitmap == null)
                throw new ArgumentNullException(nameof(inputBitmap), "Input bitmap cannot be null.");

            Bitmap resultBitmap = new Bitmap(inputBitmap);

            using (Graphics g = Graphics.FromImage(resultBitmap))
            {
                using (Pen redPen = new Pen(Color.Red, 2))
                {
                    foreach (var rect in rectangles)
                    {
                        g.DrawRectangle(redPen, rect);
                    }
                }
            }

            return resultBitmap;
        }
    }
}
