using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountBalancer.Controls
{
    /// <summary>
    /// Aggregator control to view, add new prices, and remove individual prices from a collection source. Additionally, the total of all the prices are presented
    /// </summary>
    public class PriceAggregatorControl : Control
    {
        static PriceAggregatorControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PriceAggregatorControl), new FrameworkPropertyMetadata(typeof(PriceAggregatorControl)));
        }

        public static readonly DependencyProperty CollectionSourceProperty = DependencyProperty.Register("CollectionSource", typeof(ObservableCollection<Tuple<decimal,int>>), typeof(PriceAggregatorControl), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty TotalPriceProperty = DependencyProperty.Register("TotalPrice", typeof(decimal), typeof(PriceAggregatorControl), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty RemoveItemCommandProperty = DependencyProperty.Register("RemoveItemCommand", typeof(ICommand), typeof(PriceAggregatorControl), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty AddItemCommandProperty = DependencyProperty.Register("AddItemCommand", typeof(ICommand), typeof(PriceAggregatorControl), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// The collection whose values are to be presented
        /// </summary>
        public ObservableCollection<Tuple<decimal, int>> CollectionSource
        {
            get { return (ObservableCollection<Tuple<decimal, int>>)base.GetValue(CollectionSourceProperty); }
            set { base.SetValue(CollectionSourceProperty, value);  }
        }

        /// <summary>
        /// The total price of all the items in the collection
        /// </summary>
        public decimal TotalPrice
        {
            get { return (decimal)base.GetValue(TotalPriceProperty); }
            set { base.SetValue(TotalPriceProperty, value); }
        }

        /// <summary>
        /// Command to add additional prices to the collection
        /// </summary>
        public ICommand AddItemCommand
        {
            get { return (ICommand)base.GetValue(AddItemCommandProperty); }
            set { base.SetValue(AddItemCommandProperty, value); }
        }

        /// <summary>
        /// Command to remove prices from the collection
        /// </summary>
        public ICommand RemoveItemCommand
        {
            get { return (ICommand)base.GetValue(RemoveItemCommandProperty); }
            set { base.SetValue(RemoveItemCommandProperty, value); }
        }
    }
}
