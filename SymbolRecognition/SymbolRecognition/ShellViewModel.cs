using Caliburn.Micro;

namespace SymbolRecognition
{
    public class ShellViewModel : PropertyChangedBase
    {
        private readonly IUserService _userService;

        public ShellViewModel(IUserService userService)
        {
            _userService = userService;
            MyName = "Insert your name";
        }

        public string MyName { get; set; }

        public string FullName => $"{MyName} {_userService.GetSampleUserLastName()}";

        public void ResetName()
        {
            MyName = "name after resetting";
        }

        public bool CanResetName => FullName.Length > 10;
    }
}