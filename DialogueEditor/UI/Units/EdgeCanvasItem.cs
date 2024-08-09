using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DialogueEditor.UI.Units
{
    public class EdgeCanvasItem : ListBoxItem
    {
        private PathFigure _pathFigure;
        private BezierSegment _bezierSegment;

        public static readonly DependencyProperty StartPositionProperty =
            DependencyProperty.Register(nameof(StartPosition), typeof(Point), typeof(EdgeCanvasItem), new PropertyMetadata(new Point(0, 0), OnStartPositionChanged));
        public static readonly DependencyProperty EndPositionProperty =
            DependencyProperty.Register(nameof(EndPosition), typeof(Point), typeof(EdgeCanvasItem), new PropertyMetadata(new Point(0, 0), OnEndPositionChanged));

        public Point StartPosition
        {
            get => (Point)GetValue(StartPositionProperty);
            set => SetValue(StartPositionProperty, value);
        }
        public Point EndPosition
        {
            get => (Point)GetValue(EndPositionProperty);
            set => SetValue(EndPositionProperty, value);
        }

        static EdgeCanvasItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EdgeCanvasItem),
                new FrameworkPropertyMetadata(typeof(EdgeCanvasItem)));
        }

        public override void OnApplyTemplate()
        {
            if (GetTemplateChild("PART_PathFigure") is PathFigure pathFigure) {
                _pathFigure = pathFigure;
            }

            if (GetTemplateChild("PART_BezierSegment") is BezierSegment bezierSegment) {
                _bezierSegment = bezierSegment;
            }

            if (_pathFigure != null && _bezierSegment != null) {
                SetStartPosition(StartPosition);
                SetEndPosition(EndPosition);
            }
        }

        private static void OnStartPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is EdgeCanvasItem item) {
                item.SetStartPosition((Point)e.NewValue);
            }
        }

        private static void OnEndPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is EdgeCanvasItem item) {
                item.SetEndPosition((Point)e.NewValue);
            }
        }

        private void SetStartPosition(Point position)
        {
            if (_pathFigure != null && _bezierSegment != null) {
                _pathFigure.StartPoint = position;
                _bezierSegment.Point1 = new Point(position.X + 50, position.Y);
            }
        }

        private void SetEndPosition(Point position)
        {
            if (_pathFigure != null && _bezierSegment != null) {
                _bezierSegment.Point3 = position;
                _bezierSegment.Point2 = new Point(position.X - 50, position.Y);
            }
        }
    }
}
