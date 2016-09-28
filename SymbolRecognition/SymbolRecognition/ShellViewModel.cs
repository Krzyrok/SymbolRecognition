using Caliburn.Micro;

namespace SymbolRecognition
{
    public class ShellViewModel : PropertyChangedBase
    {
        public ShellViewModel()
        {
            MyName = "Insert your name";
        }

        private string _myName;

        public string MyName
        {
            get
            {
                return _myName;
            }

            set
            {
                if (_myName == value)
                {
                    return;
                }
                _myName = value;
                NotifyOfPropertyChange(nameof(MyName));
                NotifyOfPropertyChange(nameof(FullName));
            }
        }

        public string FullName => $"{MyName} LastName";

        public void ResetName()
        {
            MyName = "name after resetting";
            NotifyOfPropertyChange(nameof(MyName));
            NotifyOfPropertyChange(nameof(FullName));
        }

        public bool CanResetName()
        {
            return true;
        }
    }
}