using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DialogueEditor.UI.Units
{
    public class ExitDock : DockBase
    {
        static ExitDock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExitDock),
                new FrameworkPropertyMetadata(typeof(ExitDock)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (DockHost != null) {
                DockHost.MouseMove += DockHost_MouseMove;
                DockHost.Unloaded += DockHost_Unloaded;
            }
        }

        private void DockHost_Unloaded(object sender, RoutedEventArgs e)
        {
            DockHost.MouseMove -= DockHost_MouseMove;
            DockHost.Unloaded -= DockHost_Unloaded;
        }

        private void DockHost_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) {
                _ = DragDrop.DoDragDrop(this, this, DragDropEffects.Copy);
            }
        }
    }
}
