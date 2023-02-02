using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GridTest
{
    public class TestViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TestItem> testList;
        public ObservableCollection<TestItem> TestListCollection
        {
            get { return testList; }
            set { this.testList = value; }
        }

        public TestViewModel()
        {
            testList = new ObservableCollection<TestItem>();

            CreateTestListItems();
        }

        public void CreateTestListItems()
        {
            Random random = new Random();

            // Put some meaningless numbers in each square for demo purposes.
            for (int i = 0; i < 9; i++)
            {
                for (int j = i; j < i + 9; j++)
                {
                    int squareNumber = (j % 9) + 1;

                    int randomNumber = random.Next(3);

                    testList.Add(new TestItem()
                    {
                        Index = (i * 9) + (j - i),
                        Name = squareNumber.ToString(),
                        NumberShown = (randomNumber % 3 == 0)
                    });

                }
            }
        }

        public void Refresh()
        {
            testList.Clear();

            CreateTestListItems();
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
