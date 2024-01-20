using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFArrows.Arrows
{
    /// <summary>
    /// ��ͷ����
    /// </summary>
    public abstract class ArrowBase : Shape
    {
        #region Fields

        /// <summary>
        /// ������״(������ͷ�;�����״)
        /// </summary>
        private readonly PathGeometry _wholeGeometry = new PathGeometry();

        /// <summary>
        /// ��ȥ��ͷ��ľ�����״
        /// </summary>
        private readonly PathFigure _figureConcrete = new PathFigure();

        /// <summary>
        /// ��ʼ���ļ�ͷ�߶�
        /// </summary>
        private readonly PathFigure _figureStart=new PathFigure();

        /// <summary>
        /// �������ļ�ͷ�߶�
        /// </summary>
        private readonly PathFigure _figureEnd=new PathFigure();

        #endregion Fields

        #region Properties

        /// <summary>
        /// ��ͷ���߼нǵ���������
        /// </summary>
        public static readonly DependencyProperty ArrowAngleProperty =
            DependencyProperty.Register("ArrowAngle", typeof(double), typeof(ArrowBase),
                new FrameworkPropertyMetadata(45.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// ��ͷ���߼н�
        /// </summary>
        public double ArrowAngle
        {
            set { SetValue(ArrowAngleProperty, value); }
            get { return (double)GetValue(ArrowAngleProperty); }
        }

        /// <summary>
        /// ��ͷ���ȵ���������
        /// </summary>
        public static readonly DependencyProperty ArrowLengthProperty =
            DependencyProperty.Register("ArrowLength", typeof(double), typeof(ArrowBase),
                new FrameworkPropertyMetadata(9.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// ��ͷ���ߵĳ���
        /// </summary>
        public double ArrowLength
        {
            set { SetValue(ArrowLengthProperty, value); }
            get { return (double)GetValue(ArrowLengthProperty); }
        }

        /// <summary>
        /// ��ͷ���ڶ˵���������
        /// </summary>
        public static readonly DependencyProperty ArrowEndsProperty =
            DependencyProperty.Register("ArrowEnds", typeof(ArrowEnds), typeof(ArrowBase),
                new FrameworkPropertyMetadata(ArrowEnds.End, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// ��ͷ���ڶ�
        /// </summary>
        public ArrowEnds ArrowEnds
        {
            set { SetValue(ArrowEndsProperty, value); }
            get { return (ArrowEnds)GetValue(ArrowEndsProperty); }
        }

        /// <summary>
        /// ��ͷ�Ƿ�պϵ���������
        /// </summary>
        public static readonly DependencyProperty IsArrowClosedProperty =
            DependencyProperty.Register("IsArrowClosed", typeof(bool), typeof(ArrowBase),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// ��ͷ�Ƿ�պ�
        /// </summary>
        public bool IsArrowClosed
        {
            set { SetValue(IsArrowClosedProperty, value); }
            get { return (bool)GetValue(IsArrowClosedProperty); }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public static readonly DependencyProperty StartPointProperty = DependencyProperty.Register(
            "StartPoint", typeof(Point), typeof(ArrowBase),
            new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public Point StartPoint
        {
            get { return (Point)GetValue(StartPointProperty); }
            set { SetValue(StartPointProperty, value); }
        }

        #endregion Properties

        #region Protected Methods

        /// <summary>
        /// ���캯��
        /// </summary>
        protected ArrowBase()
        {
            var polyLineSegStart = new PolyLineSegment();
            _figureStart.Segments.Add(polyLineSegStart);

            var polyLineSegEnd = new PolyLineSegment();
            _figureEnd.Segments.Add(polyLineSegEnd);

            _wholeGeometry.Figures.Add(_figureConcrete);
            _wholeGeometry.Figures.Add(_figureStart);
            _wholeGeometry.Figures.Add(_figureEnd);
        }

        /// <summary>
        /// ��ȡ������״�ĸ�����ɲ���
        /// </summary>
        protected abstract PathSegmentCollection FillFigure();

        /// <summary>
        /// ��ȡ��ʼ��ͷ���Ľ�����
        /// </summary>
        /// <returns>��ʼ��ͷ���Ľ�����</returns>
        protected abstract Point GetStartArrowEndPoint();

        /// <summary>
        /// ��ȡ������ͷ���Ŀ�ʼ��
        /// </summary>
        /// <returns>������ͷ���Ŀ�ʼ��</returns>
        protected abstract Point GetEndArrowStartPoint();

        /// <summary>
        /// ��ȡ������ͷ���Ľ�����
        /// </summary>
        /// <returns>������ͷ���Ľ�����</returns>
        protected abstract Point GetEndArrowEndPoint();

        /// <summary>
        /// ������״
        /// </summary>
        protected override Geometry DefiningGeometry
        {
            get
            {
                _figureConcrete.StartPoint = StartPoint;

                //��վ�����״,�����ظ����
                _figureConcrete.Segments.Clear();
                var segements = FillFigure();
                if (segements != null)
                {
                    foreach (var segement in segements)
                    {
                        _figureConcrete.Segments.Add(segement);
                    }
                }

                //���ƿ�ʼ���ļ�ͷ
                if ((ArrowEnds & ArrowEnds.Start) == ArrowEnds.Start)
                {
                    CalculateArrow(_figureStart, GetStartArrowEndPoint(), StartPoint);
                }

                // ���ƽ������ļ�ͷ
                if ((ArrowEnds & ArrowEnds.End) == ArrowEnds.End)
                {
                    CalculateArrow(_figureEnd, GetEndArrowStartPoint(), GetEndArrowEndPoint());
                }

                return _wholeGeometry;
            }
        }


        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            Point p = hitTestParameters.HitPoint;
            EllipseGeometry ell = new EllipseGeometry(p, 5, 5);
            _he = null;
            VisualTreeHelper.HitTest(this, null, new HitTestResultCallback(Hit), new GeometryHitTestParameters(ell));
            if (_he != null && _he.VisualHit == this) return new PointHitTestResult(this, hitTestParameters.HitPoint);
            return null;
        }
        HitTestResult _he = null;
        private HitTestResultBehavior Hit(HitTestResult he)
        {
            _he = he;
            return HitTestResultBehavior.Continue;
        }

        #endregion  Protected Methods

        #region Private Methods

        /// <summary>
        /// ����������֮��������ͷ
        /// </summary>
        /// <param name="pathfig">��ͷ���ڵ���״</param>
        /// <param name="startPoint">��ʼ��</param>
        /// <param name="endPoint">������</param>
        /// <returns>����õ���״</returns>
        private void CalculateArrow(PathFigure pathfig, Point startPoint, Point endPoint)
        {
            var polyseg = pathfig.Segments[0] as PolyLineSegment;
            if (polyseg != null)
            {
                var matx = new Matrix();
                Vector vect = startPoint - endPoint;
                //��ȡ��λ����
                vect.Normalize();
                vect *= ArrowLength;
                //��ת�нǵ�һ��
                matx.Rotate(ArrowAngle / 2);
                //�����ϰ�μ�ͷ�ĵ�
                pathfig.StartPoint = endPoint + vect * matx;

                polyseg.Points.Clear();
                polyseg.Points.Add(endPoint);

                matx.Rotate(-ArrowAngle);
                //�����°�μ�ͷ�ĵ�
                polyseg.Points.Add(endPoint + vect * matx);
            }

            pathfig.IsClosed = IsArrowClosed;
        }

        #endregion Private Methods

    }
}
