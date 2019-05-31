using System;
using System.Collections.Generic;
using System.Text;

namespace LightLinkModels
{
    public class UserLogin
    {
        public string Username { get; }
        public string Password { get; }
        public UserLogin(String username, String password)
        {
            Username = username;
            Password = password;
        }

    }
}
