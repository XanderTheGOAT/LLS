using System.Collections.Generic;

namespace LightLinkModels
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<Profile> Profiles { get; set; }
    }
}
