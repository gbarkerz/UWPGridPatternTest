using System;
using System.Collections.ObjectModel;

namespace GridTest
{
    public class TestViewModel 
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
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    int randomNumber = random.Next(3);

                    testList.Add(new TestItem()
                    {
                        X = x,
                        Y = y,
                        Name = (y + 1).ToString(),
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
    }
}
