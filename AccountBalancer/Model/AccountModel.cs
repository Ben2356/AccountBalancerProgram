using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace AccountBalancer.Model
{
    /// <summary>
    /// The account model that contains all the necessary inputs
    /// </summary>
    public class AccountModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Constructs an account model and sets up the collection changed properties of the collections
        /// </summary>
        public AccountModel()
        {
            deductions = new ObservableCollection<Tuple<decimal, int>>();
            deductions.CollectionChanged += Deductions_CollectionChanged;
            credits = new ObservableCollection<Tuple<decimal, int>>();
            credits.CollectionChanged += Credits_CollectionChanged;
            deposits = new ObservableCollection<Tuple<decimal, int>>();
            deposits.CollectionChanged += Deposits_CollectionChanged;
            withdrawals = new ObservableCollection<Tuple<decimal, int>>();
            withdrawals.CollectionChanged += Withdrawals_CollectionChanged;
        }

        /// <summary>
        /// Triggers the TotalWithdrawals property changed event when the withdrawals collection changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void Withdrawals_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("TotalWithdrawals");
        }

        /// <summary>
        /// Triggers the TotalDeposits property changed event when the deposits collection changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void Deposits_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("TotalDeposits");
        }

        /// <summary>
        /// Triggers the TotalCredits property changed event when the credits collection changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void Credits_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("TotalCredits");
        }

        /// <summary>
        /// Triggers the TotalDeductions property changed event when the deductions collection changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void Deductions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("TotalDeductions");
        }

        //account register fields
        private decimal accountRegisterBalance;

        /// <summary>
        /// The account register balance field
        /// </summary>
        public decimal AccountRegisterBalance
        {
            get { return accountRegisterBalance; }
            set 
            { 
                accountRegisterBalance = value;
                RaisePropertyChanged("accountRegisterBalance");
            }
        }

        private readonly ObservableCollection<Tuple<decimal, int>> deductions;

        /// <summary>
        /// Deductions collection with each entry represented as a tuple of the deduction value and a unique id
        /// </summary>
        public ObservableCollection<Tuple<decimal,int>> Deductions
        {
            get { return deductions; }
        }

        /// <summary>
        /// The sum of the deductions collection
        /// </summary>
        public decimal TotalDeductions
        {
            get
            {
                return deductions.Sum(x => x.Item1);
            }
        }

        /// <summary>
        /// Adds a deduction
        /// </summary>
        /// <param name="value">The value to be added</param>
        public void AddDeduction(decimal value)
        {
            AddCollectionItem(deductions, value);
        }

        /// <summary>
        /// Removes a deduction
        /// </summary>
        /// <param name="id">The id of the value to be removed</param>
        public void RemoveDeduction(int id)
        {
            RemoveCollectionItem(deductions, id);
        }

        private readonly ObservableCollection<Tuple<decimal,int>> credits;

        /// <summary>
        /// Credits collection with each entry represented as a tuple of the credit value and a unique id
        /// </summary>
        public ObservableCollection<Tuple<decimal,int>> Credits
        {
            get { return credits; }
        }

        /// <summary>
        /// The sum of the credits collection
        /// </summary>
        public decimal TotalCredits
        {
            get
            {
                return credits.Sum(x => x.Item1);
            }
        }

        /// <summary>
        /// Adds a credit
        /// </summary>
        /// <param name="value">The value to be added</param>
        public void AddCredit(decimal value)
        {
            AddCollectionItem(credits, value);
        }

        /// <summary>
        /// Removes a credit
        /// </summary>
        /// <param name="id">The id of the value to be removed</param>
        public void RemoveCredit(int id)
        {
            RemoveCollectionItem(credits, id);
        }

        /// <summary>
        /// The new account register balance calculated from the accountRegisterBalance, TotalDeductions, and TotalCredits
        /// </summary>
        public decimal NewAccountRegisterBalance
        {
            get
            {
                return accountRegisterBalance - (TotalDeductions + TotalCredits);
            }
        }

        //account statement fields
        private decimal statementEndingBalance;

        /// <summary>
        /// The statement ending balance field
        /// </summary>
        public decimal StatementEndingBalance
        {
            get { return statementEndingBalance; }
            set 
            { 
                statementEndingBalance = value; 
                RaisePropertyChanged("StatementEndingBalance"); 
            }
        }

        private readonly ObservableCollection<Tuple<decimal, int>> deposits;

        /// <summary>
        /// Deposits collection with each entry represented as a tuple of the deposit value and a unique id
        /// </summary>
        public ObservableCollection<Tuple<decimal, int>> Deposits
        {
            get { return deposits; }
        }

        /// <summary>
        /// Adds a deposit
        /// </summary>
        /// <param name="value">The value to be added</param>
        public void AddDeposit(decimal value)
        {
            AddCollectionItem(deposits, value);
        }

        /// <summary>
        /// Removes a deposit
        /// </summary>
        /// <param name="id">The id of the value to be removed</param>
        public void RemoveDeposit(int id)
        {
            RemoveCollectionItem(deposits, id);
        }

        /// <summary>
        /// The sum of the deposits collection
        /// </summary>
        public decimal TotalDeposits
        {
            get
            {
                return deposits.Sum(x => x.Item1);
            }
        }

        private readonly ObservableCollection<Tuple<decimal, int>> withdrawals;

        /// <summary>
        /// Withdrawals collection with each entry represented as a tuple of the withdrawal value and a unique id
        /// </summary>
        public ObservableCollection<Tuple<decimal, int>> Withdrawals
        {
            get { return withdrawals; }
        }

        /// <summary>
        /// Adds a withdrawal
        /// </summary>
        /// <param name="value">The value to be added</param>
        public void AddWithdrawal(decimal value)
        {
            AddCollectionItem(withdrawals, value);
        }

        /// <summary>
        /// Removes a withdrawal
        /// </summary>
        /// <param name="id">The id of the value to be removed</param>
        public void RemoveWithdrawal(int id)
        {
            RemoveCollectionItem(withdrawals, id);
        }

        /// <summary>
        /// The sum of the withdrawals collection
        /// </summary>
        public decimal TotalWithdrawals
        {
            get
            {
                return withdrawals.Sum(x => x.Item1);
            }
        }

        /// <summary>
        /// The account statement subtotal calculated from the statement ending balance and total deposits
        /// </summary>
        public decimal AccountStatementSubtotal
        {
            get
            {
                return StatementEndingBalance + TotalDeposits;
            }
        }

        /// <summary>
        /// The ending total calculated from the account statement subtotal and the total withdrawals
        /// </summary>
        public decimal Total
        {
            get
            {
                return AccountStatementSubtotal - TotalWithdrawals;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Adds the value along with a unique id to the provided collection
        /// </summary>
        /// <param name="collection">The collection to add the value to</param>
        /// <param name="value">The data to be added</param>
        private void AddCollectionItem(ObservableCollection<Tuple<decimal, int>> collection, decimal value)
        {
            int id = 0;
            if (collection.Count > 0)
            {
                id = collection[collection.Count - 1].Item2 + 1;
            }
            collection.Add(new Tuple<decimal, int>(value, id));
        }

        /// <summary>
        /// Removes an item specified by it's unique id
        /// </summary>
        /// <param name="collection">The collection to remove the item from</param>
        /// <param name="id">The id of the entry to be removed from the collection</param>
        private void RemoveCollectionItem(ObservableCollection<Tuple<decimal, int>> collection, int id)
        {
            int index = GetCollectionIndexFromId(collection, id);
            if(index == -1)
            {
                return;
            }
            collection.RemoveAt(index);
        }

        /// <summary>
        /// Gets the index of the item with the provided id from the collection
        /// </summary>
        /// <param name="collection">The collection to check if the id exists in</param>
        /// <param name="id">The id of the entry to be removed from the collection</param>
        /// <returns>The index of the of the entry that matches the id. If no match is found then -1 is returned</returns>
        private int GetCollectionIndexFromId(ObservableCollection<Tuple<decimal, int>> collection, int id)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                Tuple<decimal, int> item = collection[i];
                if (item.Item2 == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
