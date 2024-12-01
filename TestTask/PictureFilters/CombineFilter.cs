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

            int offset = kernelSize / 2;

            for (int y = 0; y < input.Height; y++)
            {
                for (int x = 0; x < input.Width; x++)
                {
                    int index = (y * step) + (x * bytesPerPixel);

                    int r = pixels[index + 2];
                    int g = pixels[index + 1];
                    int b = pixels[index];
                    int grayValue = (int)(0.3 * r + 0.59 * g + 0.11 * b);

                    byte binaryValue = (byte)(grayValue > threshold ? 255 : 0);

                    bool isWhite = binaryValue == 255;
                    if (!isWhite)
                    {
                        for (int y1 = -offset; y1 <= offset; y1++)
                        {
                            for (int x1 = -offset; x1 <= offset; x1++)
                            {
                                int nx = x + x1;
                                int ny = y + y1;

                                if (nx >= 0 && ny >= 0 && nx < input.Width && ny < input.Height)
                                {
                                    int neighborIndex = (ny * step) + (nx * bytesPerPixel);
                                    if (pixels[neighborIndex] > threshold)
                                    {
                                        isWhite = true;
                                        break;
                                    }
                                }
                            }
                            if (isWhite) break;
                        }
                    }

                    byte finalValue = (byte)(isWhite ? 255 : 0);
                    resultPixels[index] = finalValue;
                    resultPixels[index + 1] = finalValue;
                    resultPixels[index + 2] = finalValue;
                    resultPixels[index + 3] = 255;
                }
            }

            Marshal.Copy(resultPixels, 0, resultData.Scan0, resultPixels.Length);
            input.UnlockBits(inputData);
            result.UnlockBits(resultData);

            return result;
        }
    }
}
