using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DialogueEditor.UI.Units
{
    public class EdgeCanvas : ListBox
    {
        static EdgeCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EdgeCanvas),
                new FrameworkPropertyMetadata(typeof(EdgeCanvas)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new EdgeCanvasItem();
        }
    }
}
