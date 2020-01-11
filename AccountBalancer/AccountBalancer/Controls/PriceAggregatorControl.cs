using AccountBalancer.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountBalancer
{
    public class PriceAggregatorControl : Control
    {
        static PriceAggregatorControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PriceAggregatorControl), new FrameworkPropertyMetadata(typeof(PriceAggregatorControl)));
        }

        public static readonly DependencyProperty CollectionSourceProperty = DependencyProperty.Register("CollectionSource", typeof(ObservableCollection<Tuple<float,int>>), typeof(PriceAggregatorControl), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty TotalPriceProperty = DependencyProperty.Register("TotalPrice", typeof(float), typeof(PriceAggregatorControl), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty RemoveItemCommandProperty = DependencyProperty.Register("RemoveItemCommand", typeof(ICommand), typeof(PriceAggregatorControl), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty AddItemCommandProperty = DependencyProperty.Register("AddItemCommand", typeof(ICommand), typeof(PriceAggregatorControl), new FrameworkPropertyMetadata(null));

        public ObservableCollection<Tuple<float, int>> CollectionSource
        {
            get { return (ObservableCollection<Tuple<float, int>>)base.GetValue(CollectionSourceProperty); }
            set { base.SetValue(CollectionSourceProperty, value);  }
        }

        public float TotalPrice
        {
            get { return (float)base.GetValue(TotalPriceProperty); }
            set { base.SetValue(TotalPriceProperty, value); }
        }

        public ICommand AddItemCommand
        {
            get { return (ICommand)base.GetValue(AddItemCommandProperty); }
            set { base.SetValue(AddItemCommandProperty, value); }
        }

        public ICommand RemoveItemCommand
        {
            get { return (ICommand)base.GetValue(RemoveItemCommandProperty); }
            set { base.SetValue(RemoveItemCommandProperty, value); }
        }
    }
}
