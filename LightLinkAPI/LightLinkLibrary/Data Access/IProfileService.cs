using LightLinkModels;
using System.Collections.Generic;

namespace LightLinkLibrary.Data_Access
{
    public interface IProfileService
    {
        Profile GetProfileByName(string username, string name);
        IEnumerable<Profile> GetProfilesForUser(string username);
        void AddProfileToUser(string username, Profile dto);
        Profile RemoveProfileFromUser(string username, Profile dto);
        void UpdateProfileOnUser(string username, string name, Profile dto);
    }
}
