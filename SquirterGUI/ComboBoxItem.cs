namespace SquirterGUI {
    public class ComboBoxItem {
        private readonly long _pages;
        private readonly string _name;

        public ComboBoxItem(string name, long pages) {
            _name = name;
            _pages = pages;
        }

        public override string ToString() {
            return _name;
        }

        public long Value { 
            get { 
                return _pages; 
            }
        }
    }
}