using Caliburn.Micro;

namespace SymbolRecognition
{
    public class ShellViewModel : PropertyChangedBase
    {
        public ShellViewModel()
        {
            MyName = "Insert your name";
        }

        public string MyName { get; set; }

        public string FullName => $"{MyName} LastName";

        public void ResetName()
        {
            MyName = "name after resetting";
        }

        public bool CanResetName => FullName.Length > 10;
    }
}