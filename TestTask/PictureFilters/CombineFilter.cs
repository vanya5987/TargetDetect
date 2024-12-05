using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using TestTask.Interfaces;

namespace TestTask.PictureFilters
{
    internal sealed class CombineFilter : ICombineFilterApplyer
    {
        public Bitmap ApplyCombineFilter(Bitmap input, int threshold, int kernelSize)
        {
            Bitmap result = new Bitmap(input.Width, input.Height);

            Rectangle rect = new Rectangle(0, 0, input.Width, input.Height);
            BitmapData inputData = input.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData resultData = result.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytesPerPixel = 4;
            int step = inputData.Stride;
            int byteCount = step * input.Height;

            byte[] pixels = new byte[byteCount];
            byte[] resultPixels = new byte[byteCount];

            Marshal.Copy(inputData.Scan0, pixels, 0, pixels.Length);

            int[,] sobelX = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] sobelY = new int[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

            for (int y = 1; y < input.Height - 1; y++)
            {
                for (int x = 1; x < input.Width - 1; x++)
                {
                    int gradientX = 0;
                    int gradientY = 0;

                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            int nx = x + kx;
                            int ny = y + ky;
                            int index = (ny * step) + (nx * bytesPerPixel);

                            int r = pixels[index + 2];
                            int g = pixels[index + 1];
                            int b = pixels[index];
                            int grayValue = (int)(0.3 * r + 0.59 * g + 0.11 * b);

                            gradientX += sobelX[ky + 1, kx + 1] * grayValue;
                            gradientY += sobelY[ky + 1, kx + 1] * grayValue;
                        }
                    }

                    int gradient = (int)Math.Sqrt(gradientX * gradientX + gradientY * gradientY);

                    byte finalValue = (byte)(gradient > threshold ? 255 : 0);
                    byte invertedValue = (byte)(255 - finalValue);

                    int outputIndex = (y * step) + (x * bytesPerPixel);
                    resultPixels[outputIndex] = invertedValue;
                    resultPixels[outputIndex + 1] = invertedValue;
                    resultPixels[outputIndex + 2] = invertedValue;
                    resultPixels[outputIndex + 3] = 255;
                }
            }

            Marshal.Copy(resultPixels, 0, resultData.Scan0, resultPixels.Length);
            input.UnlockBits(inputData);
            result.UnlockBits(resultData);

            return result;
        }
    }
}
