using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DialogueEditor.UI.Units
{
    public class EntryDock : DockBase
    {
        static EntryDock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EntryDock),
                new FrameworkPropertyMetadata(typeof(EntryDock)));
        }
    }
}
