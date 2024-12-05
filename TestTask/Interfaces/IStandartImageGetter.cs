using Emgu.CV;

namespace TestTask.Interfaces
{
    internal interface IStandartImageGetter
    {
        Bitmap GetStandartImage(VideoCapture capture, Mat mat);
    }
}
