using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DialogueEditor.UI.Units
{
    public class BranchListBoxItem : ListBoxItem
    {
        static BranchListBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BranchListBoxItem),
                new FrameworkPropertyMetadata(typeof(BranchListBoxItem)));
        }
    }
}
