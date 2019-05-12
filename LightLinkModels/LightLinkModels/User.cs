using System;
using System.Collections.Generic;
using System.Text;

namespace LightLinkModels
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Profile[] Profiles { get; set; }
    }
}
