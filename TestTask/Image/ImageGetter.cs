using Emgu.CV;
using Emgu.CV.Structure;
using TestTask.Interfaces;

namespace TestTask.Image
{
    internal sealed class ImageGetter : IStandartImageGetter
    {
        public Bitmap GetStandartImage(VideoCapture capture, Mat mat)
        {
            capture.Retrieve(mat);

            return mat.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal).ToBitmap();
        }
    }
}
