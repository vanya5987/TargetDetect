using DirectShowLib;
using TestTask.Interfaces;

namespace TestTask.CheckValidate
{
    internal sealed class ValidateChecker : ITargetValidateChecker, ICamsValidateChecker
    {
        private readonly List<Point> _landmarks;
        private int _maxLandmarks;
        private int _minWidth;
        private int _minHeight;

        public ValidateChecker(List<Point> landmarks, int maxLandmarks, int minWidth, int minHeight)
        {
            _landmarks = landmarks ?? throw new ArgumentNullException(nameof(_landmarks));
            _maxLandmarks = maxLandmarks;
            _minWidth = minWidth;
            _minHeight = minHeight;

            if (_maxLandmarks < 0)
                throw new IndexOutOfRangeException(nameof(_maxLandmarks));
            if (_minHeight < 0)
                throw new IndexOutOfRangeException(nameof(_minHeight));
            if (_minWidth < 0)
                throw new IndexOutOfRangeException(nameof(_minWidth));
        }

        public void CheckValidateTarget()
        {
            if (_landmarks.Count < _maxLandmarks)
                return;

            int left = _landmarks.Min(p => p.X);
            int right = _landmarks.Max(p => p.X);
            int top = _landmarks.Min(p => p.Y);
            int bottom = _landmarks.Max(p => p.Y);

            int width = right - left;
            int height = bottom - top;

            if (width < _minWidth)
                right = left + _minWidth;
            if (height < _minHeight)
                bottom = top + _minHeight;
            if (_landmarks.Count < _maxLandmarks)
                throw new InvalidOperationException("Недостаточно точек для формирования цели.");

            _landmarks[0] = new Point(left, top);
            _landmarks[1] = new Point(right, top);
            _landmarks[2] = new Point(left, bottom);
            _landmarks[3] = new Point(right, bottom);
        }

        public void CheckValidateCams(ToolStripComboBox cameraChoice,DsDevice[] cams)
        {
            if (cams == null || cams.Length == 0)
                throw new IndexOutOfRangeException("Камеры не найдены...");

            if (cameraChoice.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите камеру.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
