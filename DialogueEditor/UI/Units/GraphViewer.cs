using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DialogueEditor.UI.Units
{
    public class GraphViewer : Control
    {
        private readonly Storyboard _vStoryboard;
        private readonly Storyboard _hStoryboard;
        private readonly ScaleTransform _scaleTransform;
        private readonly DoubleAnimation _doubleAnimation;
        private readonly CubicEase _cubicEase;
        private readonly PropertyPath _xScalePath;
        private readonly PropertyPath _yScalePath;

        private Point _clickPoint;
        private double _hOffset;
        private double _vOffset;
        private ScrollViewer _scrollViewer;
        private Grid _grid;

        public static readonly DependencyProperty ZoomFactorProperty =
            DependencyProperty.Register(nameof(ZoomFactor), typeof(double), typeof(GraphViewer), new PropertyMetadata(1.0, OnZoomFactorChanged));
        public static readonly DependencyProperty WheelUpCommandProperty =
            DependencyProperty.Register(nameof(WheelUpCommand), typeof(ICommand), typeof(GraphViewer), new PropertyMetadata(null));
        public static readonly DependencyProperty WheelDownCommandProperty =
            DependencyProperty.Register(nameof(WheelDownCommand), typeof(ICommand), typeof(GraphViewer), new PropertyMetadata(null));

        public double ZoomFactor
        {
            get => (double)GetValue(ZoomFactorProperty);
            set => SetValue(ZoomFactorProperty, value);
        }
        public ICommand WheelUpCommand
        {
            get => (ICommand)GetValue(WheelUpCommandProperty);
            set => SetValue(WheelUpCommandProperty, value);
        }
        public ICommand WheelDownCommand
        {
            get => (ICommand)GetValue(WheelDownCommandProperty);
            set => SetValue(WheelDownCommandProperty, value);
        }

        static GraphViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphViewer),
                new FrameworkPropertyMetadata(typeof(GraphViewer)));
        }

        public GraphViewer()
        {
            _vStoryboard = new Storyboard();
            _hStoryboard = new Storyboard();
            _scaleTransform = new ScaleTransform();
            _cubicEase = new CubicEase();
            _doubleAnimation = new DoubleAnimation();
            _xScalePath = new PropertyPath("LayoutTransform.ScaleX");
            _yScalePath = new PropertyPath("LayoutTransform.ScaleY");
        }

        public override void OnApplyTemplate()
        {
            if (GetTemplateChild("PART_ScrollViewer") is ScrollViewer scrollViewer) {
                _scrollViewer = scrollViewer;
            }

            if (GetTemplateChild("PART_Grid") is Grid grid) {
                _grid = grid;
            }
        }

        private static void OnZoomFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GraphViewer graphViewer) {
                double oldValue = (double)e.OldValue;
                double newValue = (double)e.NewValue;
                graphViewer.ResizeZoom(oldValue, newValue);
            }
        }

        private void ResizeZoom(double oldValue, double newValue)
        {
            _scaleTransform.ScaleX = oldValue;
            _scaleTransform.ScaleY = oldValue;
            _grid.LayoutTransform = _scaleTransform;
            _cubicEase.EasingMode = EasingMode.EaseInOut;
            _doubleAnimation.EasingFunction = _cubicEase;
            _doubleAnimation.Duration = TimeSpan.FromMilliseconds(300);
            _doubleAnimation.From = oldValue;
            _doubleAnimation.To = newValue;
            _hStoryboard.Children.Add(_doubleAnimation);
            Storyboard.SetTargetProperty(_doubleAnimation, _xScalePath);
            Storyboard.SetTarget(_doubleAnimation, _grid);
            _hStoryboard.Begin();

            _vStoryboard.Children.Add(_doubleAnimation);
            Storyboard.SetTargetProperty(_doubleAnimation, _yScalePath);
            Storyboard.SetTarget(_doubleAnimation, _grid);
            _vStoryboard.Begin();

            _hStoryboard.Children.Clear();
            _vStoryboard.Children.Clear();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed) {
                _clickPoint = e.GetPosition(_scrollViewer);
                _hOffset = _scrollViewer.HorizontalOffset;
                _vOffset = _scrollViewer.VerticalOffset;
                _ = _scrollViewer.CaptureMouse();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_scrollViewer.IsMouseCaptured) {
                Point mousePoint = e.GetPosition(_scrollViewer);
                _scrollViewer.ScrollToHorizontalOffset(_hOffset + (_clickPoint.X - mousePoint.X));
                _scrollViewer.ScrollToVerticalOffset(_vOffset + (_clickPoint.Y - mousePoint.Y));
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (_scrollViewer.IsMouseCaptured) {
                _scrollViewer.ReleaseMouseCapture();
            }
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control) {
                return;
            }

            if (0 < e.Delta) {
                WheelUpCommand?.Execute(null);
            } else {
                WheelDownCommand?.Execute(null);
            }

            e.Handled = true;
        }
    }
}
