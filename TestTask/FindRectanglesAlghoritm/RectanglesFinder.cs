namespace TestTask.FindRectanglesAlghoritm
{
    internal class RectanglesFinder
    {
        public static List<Rectangle> FindRectangles(Bitmap input)
        {
            List<Rectangle> rectangles = new List<Rectangle>();

            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    if (input.GetPixel(x, y).R == 255)
                    {
                        int x1 = x, y1 = y;

                        while (x1 < input.Width && input.GetPixel(x1, y).R == 255) x1++;
                        while (y1 < input.Height && input.GetPixel(x, y1).R == 255) y1++;

                        rectangles.Add(new Rectangle(x, y, x1 - x, y1 - y));

                        x = x1;
                    }
                }
            }

            return rectangles;
        }
    }
}
