using TestTask.CheckValidate;

namespace TestTask.RiperControl
{
    internal class RiperController
    {
        private readonly List<Point> _landmarks;
        private int _draggingIndex;
        private int _maxLandmarks;
        private bool _isDragging = false;

        public RiperController(List<Point> landmarks, int draggingIndex, int maxLandmarks)
        {
            _landmarks = landmarks ?? throw new ArgumentNullException(nameof(_landmarks));
            _draggingIndex = draggingIndex;
            _maxLandmarks = maxLandmarks;

            if (_draggingIndex < -1)
                throw new ArgumentOutOfRangeException(nameof(_draggingIndex));
            if( _maxLandmarks < 0)
                throw new ArgumentOutOfRangeException(nameof(_maxLandmarks));
        }

        public void ClickToLandmarks(MouseEventArgs e)
        {
            _draggingIndex = _landmarks.FindIndex(p => Math.Abs(p.X - e.X) < 10 && Math.Abs(p.Y - e.Y) < 10);

            if (_draggingIndex >= 0)
                _isDragging = true;
        }

        public void MoveLandmarks(MouseEventArgs e, PictureBox pictureBox)
        {
            if (_draggingIndex >= 0 && e.Button == MouseButtons.Left && _draggingIndex < _landmarks.Count)
            {
                _landmarks[_draggingIndex] = e.Location;
                pictureBox.Invalidate();
            }
        }

        public void ThrowLandmarks(MouseEventArgs e)
        {
            _draggingIndex = -1;
            _isDragging = false;
        }

        public void SetLandmarks(PictureBox pictureBox,ValidateChecker validateChecker,MouseEventArgs e)
        {
            if (_isDragging)
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (_landmarks.Count < _maxLandmarks)
                {
                    _landmarks.Add(e.Location);

                    if (_landmarks.Count == _maxLandmarks)
                        validateChecker.CheckValidateRectangle();

                    pictureBox.Invalidate();
                }
            }
        }
    }
}
