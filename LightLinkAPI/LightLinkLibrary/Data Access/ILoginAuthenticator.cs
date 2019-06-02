using LightLinkModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightLinkLibrary.Data_Access
{
    public interface ILoginAuthenticator
    {
        bool Authenticate(UserLogin logInfo);
    }
}
