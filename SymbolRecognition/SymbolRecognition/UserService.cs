namespace SymbolRecognition
{
    public class UserService : IUserService
    {
        public string GetSampleUserLastName()
        {
            return "LastName";
        }
    }

    public interface IUserService
    {
        string GetSampleUserLastName();
    }
}
