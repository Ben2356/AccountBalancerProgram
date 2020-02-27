using System.Windows;
using System.Windows.Controls;

namespace AccountBalancer.Controls
{
    /// <summary>
    /// Step progress bar control that displays the current step and denotes completed, in progress, and unfinished steps by color and style
    /// </summary>
    public class StepProgressBar : ItemsControl
    {
        static StepProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StepProgressBar), new FrameworkPropertyMetadata(typeof(StepProgressBar)));
        }

        public static readonly DependencyProperty CurrentStepIndexProperty = DependencyProperty.Register("CurrentStepIndex", typeof(int), typeof(StepProgressBar), null);

        /// <summary>
        /// The index of the step that is currently in progress
        /// </summary>
        public int CurrentStepIndex
        {
            get { return (int)base.GetValue(CurrentStepIndexProperty);  }
            set { base.SetValue(CurrentStepIndexProperty, value);  }
        }
    }
}
