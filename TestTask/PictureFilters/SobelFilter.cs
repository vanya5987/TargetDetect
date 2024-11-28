namespace TestTask.PictureFilters
{
    internal class SobelFilter
    {
        public  Bitmap ApplySobelEdgeDetection(Bitmap input)
        {
            Bitmap edgeImage = new Bitmap(input.Width, input.Height);

            int[,] sobelX = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] sobelY = new int[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

            for (int x = 1; x < input.Width - 1; x++)
            {
                for (int y = 1; y < input.Height - 1; y++)
                {
                    int gx = 0, gy = 0;

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int pixelValue = input.GetPixel(x + i, y + j).R;
                            gx += pixelValue * sobelX[i + 1, j + 1];
                            gy += pixelValue * sobelY[i + 1, j + 1];
                        }
                    }

                    int magnitude = (int)Math.Sqrt(gx * gx + gy * gy);
                    magnitude = Math.Min(255, Math.Max(0, magnitude));
                    edgeImage.SetPixel(x, y, Color.FromArgb(magnitude, magnitude, magnitude));
                }
            }

            return edgeImage;
        }
    }
}
