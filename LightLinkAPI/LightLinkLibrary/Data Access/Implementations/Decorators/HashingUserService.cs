using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using LightLinkModels;

namespace LightLinkLibrary.Data_Access.Implementations.Decorators
{
    public class HashingUserService : AbstractUserServiceDecorator
    {
        public HashingUserService(IUserService service): base(service)
        {}

        private string HashPassword(string password)
        {
            var hashed = default(byte[]);
            using (var sha = SHA256.Create())
            {
                hashed = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        
            return Encoding.UTF8.GetString(hashed);
        }

        public override void AddUser(User dto)
        {
            dto.Password = HashPassword(dto.Password);
            Service.AddUser(dto);
        }

        public override void UpdateUser(string id, User dto)
        {
            dto.Password = HashPassword(dto.Password);
            Service.UpdateUser(id, dto);
        }
    }
}
