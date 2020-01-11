using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBalancer.model
{
    public class AccountModel : INotifyPropertyChanged
    {
        public AccountModel()
        {
            deductions = new ObservableCollection<Tuple<float, int>>();
            deductions.CollectionChanged += Deductions_CollectionChanged;
            credits = new ObservableCollection<Tuple<float, int>>();
            credits.CollectionChanged += Credits_CollectionChanged;
            deposits = new ObservableCollection<Tuple<float, int>>();
            deposits.CollectionChanged += Deposits_CollectionChanged;
            withdrawals = new ObservableCollection<Tuple<float, int>>();
            withdrawals.CollectionChanged += Withdrawals_CollectionChanged;
        }

        private void Withdrawals_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("TotalWithdrawals");
        }

        private void Deposits_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("TotalDeposits");
        }

        private void Credits_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("TotalCredits");
        }

        private void Deductions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("TotalDeductions");
        }

        //account register fields
        private float accountRegisterBalance;
        public float AccountRegisterBalance
        {
            get { return accountRegisterBalance; }
            set 
            { 
                accountRegisterBalance = value;
                RaisePropertyChanged("accountRegisterBalance");
            }
        }

        private readonly ObservableCollection<Tuple<float, int>> deductions;
        public ObservableCollection<Tuple<float,int>> Deductions
        {
            get { return deductions; }
        }

        public float TotalDeductions
        {
            get
            {
                return deductions.Sum(x => x.Item1);
            }
        }

        public void AddDeduction(float value)
        {
            AddCollectionItem(deductions, value);
        }

        public void RemoveDeduction(int id)
        {
            RemoveCollectionItem(deductions, id);
        }

        private readonly ObservableCollection<Tuple<float,int>> credits;

        public ObservableCollection<Tuple<float,int>> Credits
        {
            get { return credits; }
        }

        public float TotalCredits
        {
            get
            {
                return credits.Sum(x => x.Item1);
            }
        }

        public void AddCredit(float value)
        {
            AddCollectionItem(credits, value);
        }

        public void RemoveCredit(int id)
        {
            RemoveCollectionItem(credits, id);
        }

        public float NewAccountRegisterBalance
        {
            get
            {
                return accountRegisterBalance - TotalDeductions + TotalCredits;
            }
        }

        //account statement fields
        private float statementEndingBalance;
        public float StatementEndingBalance
        {
            get { return statementEndingBalance; }
            set 
            { 
                statementEndingBalance = value; 
                RaisePropertyChanged("StatementEndingBalance"); 
            }
        }

        private readonly ObservableCollection<Tuple<float, int>> deposits;
        public ObservableCollection<Tuple<float, int>> Deposits
        {
            get { return deposits; }
        }

        public void AddDeposit(float value)
        {
            AddCollectionItem(deposits, value);
        }

        public void RemoveDeposit(int id)
        {
            RemoveCollectionItem(deposits, id);
        }

        public float TotalDeposits
        {
            get
            {
                return deposits.Sum(x => x.Item1);
            }
        }

        private readonly ObservableCollection<Tuple<float, int>> withdrawals;
        public ObservableCollection<Tuple<float, int>> Withdrawals
        {
            get { return withdrawals; }
        }

        public void AddWithdrawal(float value)
        {
            AddCollectionItem(withdrawals, value);
        }

        public void RemoveWithdrawal(int id)
        {
            RemoveCollectionItem(withdrawals, id);
        }

        public float TotalWithdrawals
        {
            get
            {
                return withdrawals.Sum(x => x.Item1);
            }
        }

        public float AccountStatementSubtotal
        {
            get
            {
                return StatementEndingBalance + TotalDeposits;
            }
        }

        public float Total
        {
            get
            {
                return AccountStatementSubtotal - TotalWithdrawals;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void AddCollectionItem(ObservableCollection<Tuple<float, int>> collection, float value)
        {
            int id = 0;
            if (collection.Count > 0)
            {
                id = collection[collection.Count - 1].Item2 + 1;
            }
            collection.Add(new Tuple<float, int>(value, id));
        }

        private void RemoveCollectionItem(ObservableCollection<Tuple<float, int>> collection, int id)
        {
            int index = GetCollectionIndexFromId(collection, id);
            collection.RemoveAt(index);
        }

        private int GetCollectionIndexFromId(ObservableCollection<Tuple<float, int>> collection, int id)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                Tuple<float, int> item = collection[i];
                if (item.Item2 == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
