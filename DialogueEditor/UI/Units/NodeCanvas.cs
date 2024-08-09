using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace DialogueEditor.UI.Units
{
    public class NodeCanvas : ListBox
    {
        public static readonly DependencyProperty DropCommandProperty =
            DependencyProperty.Register(nameof(DropCommand), typeof(ICommand), typeof(NodeCanvas), new PropertyMetadata(null));

        public ICommand DropCommand
        {
            get => (ICommand)GetValue(DropCommandProperty);
            set => SetValue(DropCommandProperty, value);
        }

        static NodeCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeCanvas),
                new FrameworkPropertyMetadata(typeof(NodeCanvas)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NodeCanvasItem();
        }

        protected override void OnDrop(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ExitDock)) is ExitDock exitDock
                && e.OriginalSource is Ellipse ellipse) {
                DropCommand?.Execute((exitDock.DataContext, ellipse.DataContext));
            }
        }
    }
}
