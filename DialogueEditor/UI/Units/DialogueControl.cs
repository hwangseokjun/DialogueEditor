using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DialogueEditor.UI.Units
{
    public class DialogueControl : Control
    {
        static DialogueControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogueControl),
                new FrameworkPropertyMetadata(typeof(DialogueControl)));
        }
    }
}
