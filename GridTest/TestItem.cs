using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GridTest {
    public class TestItem : INotifyPropertyChanged {
        
        private string name;
        private bool numberShown;

        public TestItem() {
        }

        public int X { get; set; }
        public int Y { get; set; }

        public string Name {
            get {
                return name;
            }
            set {
                SetProperty(ref name, value);
            }
        }

        public bool NumberShown {
            get {
                return numberShown;
            }
            set {
                SetProperty(ref numberShown, value);
            }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null) {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
