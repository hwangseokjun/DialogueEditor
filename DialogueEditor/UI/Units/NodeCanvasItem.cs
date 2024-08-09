using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DialogueEditor.UI.Units
{
    public class NodeCanvasItem : ListBoxItem
    {
        private Point _clickPoint;
        private Point _startPoint;
        private Canvas _canvas;

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register(nameof(Position), typeof(Point), typeof(NodeCanvasItem), new PropertyMetadata(new Point(0, 0)));

        public Point Position
        {
            get => (Point)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        static NodeCanvasItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeCanvasItem),
                new FrameworkPropertyMetadata(typeof(NodeCanvasItem)));
        }

        public override void OnApplyTemplate()
        {
            if (VisualParent is Canvas canvas) {
                _canvas = canvas;
                LayoutUpdated += NodeCanvasItem_LayoutUpdated;
                Unloaded += NodeCanvasItem_Unloaded;
            }
        }

        private void NodeCanvasItem_Unloaded(object sender, RoutedEventArgs e)
        {
            LayoutUpdated -= NodeCanvasItem_LayoutUpdated;
            Unloaded -= NodeCanvasItem_Unloaded;
        }

        private void NodeCanvasItem_LayoutUpdated(object sender, EventArgs e)
        {
            double left = Canvas.GetLeft(this);
            double top = Canvas.GetTop(this);
            Position = new Point(left, top);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            _clickPoint = Mouse.GetPosition(_canvas);
            _startPoint = TransformToAncestor(_canvas).Transform(new Point(0, 0));
            _ = CaptureMouse();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsMouseCaptured) {
                Point mousePoint = Mouse.GetPosition(_canvas);
                double left = _startPoint.X + (mousePoint.X - _clickPoint.X);
                double top = _startPoint.Y + (mousePoint.Y - _clickPoint.Y);
                Canvas.SetLeft(this, left);
                Canvas.SetTop(this, top);
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (IsMouseCaptured) {
                ReleaseMouseCapture();
            }
        }
    }
}
