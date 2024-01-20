using System.Windows;
using System.Windows.Media;

namespace WPFArrows.Arrows
{
    /// <summary>
    /// ����֮�����ͷ��ֱ��
    /// </summary>
    public class ArrowLine:ArrowBase
    {
        #region Fields

        /// <summary>
        /// �߶�
        /// </summary>
        private readonly LineSegment _lineSegment=new LineSegment();

        #endregion Fields

        #region Properties

        /// <summary>
        /// ������
        /// </summary>
        public static readonly DependencyProperty EndPointProperty = DependencyProperty.Register(
            "EndPoint", typeof(Point), typeof(ArrowLine), 
            new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// ������
        /// </summary>
        public Point EndPoint
        {
            get { return (Point) GetValue(EndPointProperty); }
            set { SetValue(EndPointProperty, value); }
        }

        #endregion Properties

        #region Protected Methods

        /// <summary>
        /// ���Figure
        /// </summary>
        protected override PathSegmentCollection FillFigure()
        {
            _lineSegment.Point = EndPoint;
            return new PathSegmentCollection
            {
                _lineSegment
            };
        }

        /// <summary>
        /// ��ȡ��ʼ��ͷ���Ľ�����
        /// </summary>
        /// <returns>��ʼ��ͷ���Ľ�����</returns>
        protected override Point GetStartArrowEndPoint()
        {
            return EndPoint;
        }

        /// <summary>
        /// ��ȡ������ͷ���Ŀ�ʼ��
        /// </summary>
        /// <returns>������ͷ���Ŀ�ʼ��</returns>
        protected override Point GetEndArrowStartPoint()
        {
            return StartPoint;
        }

        /// <summary>
        /// ��ȡ������ͷ���Ľ�����
        /// </summary>
        /// <returns>������ͷ���Ľ�����</returns>
        protected override Point GetEndArrowEndPoint()
        {
            return EndPoint;
        }

        #endregion  Protected Methods

    }
}
