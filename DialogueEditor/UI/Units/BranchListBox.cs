using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DialogueEditor.UI.Units
{
    public class BranchListBox : ListBox
    {
        static BranchListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BranchListBox),
                new FrameworkPropertyMetadata(typeof(BranchListBox)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new BranchListBoxItem();
        }
    }
}
