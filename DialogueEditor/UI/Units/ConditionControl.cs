﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DialogueEditor.UI.Units
{
    public class ConditionControl : Control
    {
        static ConditionControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ConditionControl),
                new FrameworkPropertyMetadata(typeof(ConditionControl)));
        }
    }
}