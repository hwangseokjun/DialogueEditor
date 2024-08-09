using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DialogueEditor.Utils
{
    public static class Extensions
    {
        public static T FindParentOrNull<T>(this DependencyObject child) where T : DependencyObject
        {
            Debug.Assert(child != null);
            DependencyObject obj = VisualTreeHelper.GetParent(child);
            return obj == null ? (T)null :
                obj is T parent ? parent : FindParentOrNull<T>(obj);
        }

        public static T FindChildOrNull<T>(this DependencyObject parentOrNull) where T : DependencyObject
        {
            if (parentOrNull == null) {
                return null;
            }

            int count = VisualTreeHelper.GetChildrenCount(parentOrNull);

            for (int i = 0; i < count; ++i) {
                DependencyObject child = VisualTreeHelper.GetChild(parentOrNull, i);
                T result = (child as T) ?? FindChildOrNull<T>(child);

                if (result != null) {
                    return result;
                }
            }

            return null;
        }
    }
}
