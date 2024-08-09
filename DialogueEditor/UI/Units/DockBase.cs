using DialogueEditor.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DialogueEditor.UI.Units
{
    public abstract class DockBase : Control
    {
        protected FrameworkElement DockHost;
        private UIElement _parent;
        private double _hOffset;
        private double _vOffset;

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register(nameof(Position), typeof(Point), typeof(DockBase), new PropertyMetadata(new Point(0, 0)));

        public Point Position
        {
            get => (Point)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        public override void OnApplyTemplate()
        {
            if (GetTemplateChild("PART_DockHost") is FrameworkElement dockHost) {
                DockHost = dockHost;
                _hOffset = DockHost.Width / 2;
                _vOffset = DockHost.Height / 2;
                DockHost.LayoutUpdated += DockHost_LayoutUpdated;
                DockHost.MouseLeftButtonDown += DockHost_MouseLeftButtonDown;
                DockHost.Unloaded += DockHost_Unloaded;
                _parent = this.FindParentOrNull<NodeCanvasItem>();
            }
        }

        private void DockHost_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void DockHost_Unloaded(object sender, RoutedEventArgs e)
        {
            DockHost.LayoutUpdated -= DockHost_LayoutUpdated;
            DockHost.MouseLeftButtonDown -= DockHost_MouseLeftButtonDown;
            DockHost.Unloaded -= DockHost_Unloaded;
        }

        private void DockHost_LayoutUpdated(object sender, EventArgs e)
        {
            SetPositionInCanvas();
        }

        public void SetPositionInCanvas()
        {
            try {
                double parentLeft = Canvas.GetLeft(_parent);
                double parentTop = Canvas.GetTop(_parent);
                Point position = DockHost.TransformToAncestor(_parent).Transform(new Point(0, 0));
                double left = parentLeft + position.X + _hOffset;
                double top = parentTop + position.Y + _vOffset;
                Position = new Point(left, top);
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
