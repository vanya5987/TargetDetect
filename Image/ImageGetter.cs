using Emgu.CV;
using Emgu.CV.Structure;

namespace TestTask.Image
{
    internal class ImageGetter
    {
        public Bitmap GetStandartImage(VideoCapture capture, Mat mat)
        {
            capture.Retrieve(mat);

            return mat.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal).ToBitmap();
        }
    }
}
