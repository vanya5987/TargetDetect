using DirectShowLib;
using Emgu.CV;
using TestTask.Image;
using TestTask.RiperControl;
using TestTask.CheckValidate;
using TestTask.FrameControl;
using TestTask.PictureFilters;


namespace TestTask
{
    public partial class TestTask : Form
    {
        private const int MaxLandmarks = 4;
        private const int MinWidth = 40;
        private const int MinHeight = 60;

        private List<Point> _landmarks = new List<Point>();
        private int _selectedCameraId = 0;
        private int _draggingIndex = -1;
        private bool _cameraStartedMessageShow = false;

        private readonly Mat _mat;
        private readonly ImageGetter _imageGetter;
        private readonly RiperController _riperController;
        private readonly ValidateChecker _validateChecker;
        private readonly FrameController _frameController;
        private readonly SobelFilter _silverFilter;
        private readonly GrayscaleThresholdingDilateFilter _grayscaleThresholdingDilateFilter;


        private VideoCapture _capture;
        private DsDevice[] _cams;

        public TestTask()
        {
            InitializeComponent();

            _mat = new Mat() ?? throw new ArgumentNullException(nameof(_mat));
            _imageGetter = new ImageGetter() ?? throw new ArgumentNullException(nameof(_imageGetter));
            _riperController = new RiperController(_landmarks, _draggingIndex, MaxLandmarks) ?? throw new ArgumentNullException(nameof(_riperController));
            _validateChecker = new ValidateChecker(_landmarks, MaxLandmarks, MinWidth, MinHeight) ?? throw new ArgumentException(nameof(_validateChecker));
            _frameController = new FrameController(_landmarks, MaxLandmarks) ?? throw new ArgumentNullException(nameof(_frameController));
            _silverFilter = new SobelFilter() ?? throw new ArgumentNullException(nameof(_silverFilter));
            _grayscaleThresholdingDilateFilter = new GrayscaleThresholdingDilateFilter() ?? throw new ArgumentNullException(nameof(_grayscaleThresholdingDilateFilter));

            CallEvents();
        }

        private void TestTaskLoad(object sender, EventArgs e)
        {
            _cams = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            for (int i = 0; i < _cams.Length; i++)
                CameraChoice.Items.Add(_cams[i].Name);
        }

        private void CameraChoiceSelectedIndexChange(object sender, EventArgs e)
        {
            _selectedCameraId = CameraChoice.SelectedIndex;
        }

        private void VievVideoClick(object sender, EventArgs e)
        {
            _validateChecker.CheckValidateCams(CameraChoice, _cams);

            _capture = new VideoCapture(_selectedCameraId);

            _capture.ImageGrabbed += CaptureStandartImageGrabbed;

            _capture.Start();

            if (!_cameraStartedMessageShow)
            {
                MessageBox.Show("Для установки реперов нажмите ЛКМ. Так же вы можете переместить репер при " +
                    "помощи ЛКМ если вам это нужно=)", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _cameraStartedMessageShow = true;
            }
        }

        private void CaptureStandartImageGrabbed(object? sender, EventArgs e)
        {
            Bitmap grayscaleThresholdingFilter = _grayscaleThresholdingDilateFilter.ApplyGrayscaleThresholdAndDilate(_imageGetter.GetStandartImage(_capture, _mat), 128, 3);
            OriginalPictures.Image = grayscaleThresholdingFilter;
        }

        private void RipersRemoverClick(object sender, EventArgs e)
        {
            _landmarks.Clear();
            OriginalPictures.Invalidate();
        }

        private void ExitButtonClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FramePicturesClick(object sender, MouseEventArgs e)
        {
            _riperController.SetLandmarks(OriginalPictures, _validateChecker, e);
        }

        private void FramePicturesPaint(object sender, PaintEventArgs e)
        {
            _frameController.DrawRipers(e, PointCoordinateLabel.Text);
        }

        private void FramePicturesMouseDown(object sender, MouseEventArgs e)
        {
            _riperController.ClickToLandmarks(e);
        }

        private void FramePicturesMouseMove(object sender, MouseEventArgs e)
        {
            _riperController.MoveLandmarks(e, OriginalPictures);
        }

        private void FramePicturesMouseUp(object sender, MouseEventArgs e)
        {
            _riperController.ThrowLandmarks(e);
            _validateChecker.CheckValidateRectangle();
        }

        private void CallEvents()
        {
            OriginalPictures.MouseClick += FramePicturesClick;
            OriginalPictures.Paint += FramePicturesPaint;
            OriginalPictures.MouseDown += FramePicturesMouseDown;
            OriginalPictures.MouseMove += FramePicturesMouseMove;
            OriginalPictures.MouseUp += FramePicturesMouseUp;
        }
    }
}
