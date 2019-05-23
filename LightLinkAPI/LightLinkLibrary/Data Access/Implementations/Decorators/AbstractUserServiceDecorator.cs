using LightLinkModels;
using System;
using System.Collections.Generic;

namespace LightLinkLibrary.Data_Access.Implementations.Decorators
{
    public abstract class AbstractUserServiceDecorator : IUserService
    {
        public AbstractUserServiceDecorator(IUserService service)
        {
            Service = service;
        }

        protected IUserService Service { get; }

        public abstract void AddUser(User dto);

        public abstract void UpdateUser(string id, User dto);

        public void DeleteUser(string id)
        {
            Service.DeleteUser(id);
        }

        public bool Exists(string username)
        {
            return Service.Exists(username);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return Service.GetAllUsers();
        }

        public User GetUserById(string id)
        {
            return Service.GetUserById(id);
        }
    }
}
