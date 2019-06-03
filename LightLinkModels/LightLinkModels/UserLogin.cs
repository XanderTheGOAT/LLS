namespace LightLinkModels
{
    public class UserLogin
    {
        public string Username { get; }
        public string Password { get; set; }
        public UserLogin(string username, string password)
        {
            Username = username;
            Password = password;
        }

    }
}
