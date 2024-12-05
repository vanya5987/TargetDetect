using DirectShowLib;
using Emgu.CV;
using TestTask.Image;
using TestTask.RiperControl;
using TestTask.CheckValidate;
using TestTask.FrameControl;
using TestTask.PictureFilters;
using TestTask.FindRectanglesAlgorithm;
using TestTask.DrawTarget;
using TestTask.Interfaces;


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

        private readonly IStandartImageGetter _imageGetter;
        private readonly IFiducialMarkMoveController _ripersMoveController;
        private readonly ITargetValidateChecker _targetValidateChecker;
        private readonly ICamsValidateChecker _camsValidateChecker;
        private readonly IFiducialMarksDrawler _riperDrawer;
        private readonly ICombineFilterApplyer _combineFilter;
        private readonly ITargetFinder _targetFinder;
        private readonly ITargetsDrawer _targetDrawer;
        private readonly Mat _mat;


        private ShapeType _shapeType;
        private VideoCapture _capture;
        private DsDevice[] _cams;

        public TestTask()
        {
            InitializeComponent();

            _mat = new Mat() ?? throw new ArgumentNullException(nameof(_mat));
            _imageGetter = new ImageGetter() ?? throw new ArgumentNullException(nameof(_imageGetter));
            _ripersMoveController = new FiducialMarkMoveController(_landmarks, _draggingIndex, MaxLandmarks) ?? throw new ArgumentNullException(nameof(_ripersMoveController));
            _targetValidateChecker = new ValidateChecker(_landmarks, MaxLandmarks, MinWidth, MinHeight) ?? throw new ArgumentException(nameof(_targetValidateChecker));
            _camsValidateChecker = new ValidateChecker(_landmarks, MaxLandmarks, MinWidth, MinHeight) ?? throw new ArgumentException(nameof(_camsValidateChecker));
            _riperDrawer = new FiducialMarksDrawer(_landmarks, MaxLandmarks) ?? throw new ArgumentNullException(nameof(_riperDrawer));
            _combineFilter = new CombineFilter() ?? throw new ArgumentNullException(nameof(_combineFilter));
            _targetFinder = new TargetFinder(_landmarks) ?? throw new ArgumentNullException(nameof(_targetFinder));
            _targetDrawer = new TargetDrawer(CoordinatesLabel) ?? throw new ArgumentNullException(nameof(_targetDrawer));

            CallFrameEvents();
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
            OriginalPictures.Enabled = true;

            _camsValidateChecker.CheckValidateCams(CameraChoice, _cams);
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
            if (_landmarks.Count == 4)
            {
                Bitmap combineFilter = _combineFilter.ApplyCombineFilter(_imageGetter.GetStandartImage(_capture, _mat), 128, 3);
                List<Rectangle> rectangles = _targetFinder.FindTarget(combineFilter, MinWidth, MinHeight);
                Bitmap detectedRectangles = _targetDrawer.DrawTargets(combineFilter, rectangles);
                OriginalPictures.Image = detectedRectangles;
            }
            else
                OriginalPictures.Image = _imageGetter.GetStandartImage(_capture, _mat);

            OriginalPictures.Invalidate();
        }

        private void CallFrameEvents()
        {
            OriginalPictures.MouseClick += FramePicturesClick;
            OriginalPictures.Paint += FramePicturesPaint;
            OriginalPictures.MouseDown += FramePicturesMouseDown;
            OriginalPictures.MouseMove += FramePicturesMouseMove;
            OriginalPictures.MouseUp += FramePicturesMouseUp;
        }

        private void FramePicturesClick(object sender, MouseEventArgs e)
        {
            _ripersMoveController.SetLandmarks(OriginalPictures, _targetValidateChecker, e);
        }

        private void FramePicturesPaint(object sender, PaintEventArgs e)
        {

            _riperDrawer.DrawFiducialMarks(e, PointCoordinateLabel.Text, _shapeType);
        }

        private void FramePicturesMouseDown(object sender, MouseEventArgs e)
        {
            _ripersMoveController.ClickToLandmarks(e);
        }

        private void FramePicturesMouseMove(object sender, MouseEventArgs e)
        {
            _ripersMoveController.MoveLandmarks(e, OriginalPictures);
        }

        private void FramePicturesMouseUp(object sender, MouseEventArgs e)
        {
            _ripersMoveController.ThrowLandmarks(e);
            _targetValidateChecker.CheckValidateTarget();
        }

        private void CircleRipers_Click(object sender, EventArgs e)
        {
            _shapeType = ShapeType.Circle;
            DisposeChooseButtons();
        }

        private void SquareRipers_Click(object sender, EventArgs e)
        {
            _shapeType = ShapeType.Square;
            DisposeChooseButtons();
        }

        private void CrossRiper_Click(object sender, EventArgs e)
        {
            _shapeType = ShapeType.Cross;
            DisposeChooseButtons();
        }

        private void DisposeChooseButtons()
        {
            toolStrip1.Enabled = true;
            CircleRipers.Dispose();
            SquareRipers.Dispose();
            CrossRipers.Dispose();
            ChooseRipers.Dispose();
        }
    }
}
