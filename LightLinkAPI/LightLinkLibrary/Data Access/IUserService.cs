using LightLinkModels;
using System.Collections.Generic;

namespace LightLinkLibrary.Data_Access
{
    public interface IUserService
    {
        User GetUserById(string id);
        IEnumerable<User> GetAllUsers();
        void AddUser(User dto);
        User DeleteUser(User dto);
        void UpdateUser(string id, User dto);
    }
}
