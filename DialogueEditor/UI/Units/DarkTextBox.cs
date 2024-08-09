using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DialogueEditor.UI.Units
{
    public class DarkTextBox : TextBox
    {
        static DarkTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DarkTextBox),
                new FrameworkPropertyMetadata(typeof(DarkTextBox)));
        }
    }
}
