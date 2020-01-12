using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace AccountBalancer
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AccountBalancer"
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class StepProgressBar : ItemsControl
    {
        static StepProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StepProgressBar), new FrameworkPropertyMetadata(typeof(StepProgressBar)));
        }

        public static readonly DependencyProperty CurrentStepIndexProperty = DependencyProperty.Register("CurrentStepIndex", typeof(int), typeof(StepProgressBar), new FrameworkPropertyMetadata(0, null, UpdateStepIndex));

        private static object UpdateStepIndex(DependencyObject target, object value)
        {
            StepProgressBar stepProgressBar = target as StepProgressBar;
            ObservableCollection<string> stepsSource = stepProgressBar.ItemsSource as ObservableCollection<string>;
            int maxStepsSize = stepsSource.Count;
            int stepIndex = (int)value;
            if (stepIndex < 0)
            {
                stepIndex = 0;
            }
            else if (stepIndex >= maxStepsSize)
            {
                stepIndex = maxStepsSize-1;
            }
            return stepIndex;
        }

        public int CurrentStepIndex
        {
            get { return (int)base.GetValue(CurrentStepIndexProperty);  }
            set { base.SetValue(CurrentStepIndexProperty, value);  }
        }
    }
}
