using System;
using System.Linq;
using FluentAssertions;
using LightLinkLibrary.Data_Access.Implementations;
using LightLinkModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightLinkLibraryTests
{
    [TestClass]
    public class MongoSuperServiceShould
    {
        [TestMethod]
        public void GiveUserIfUserExists()
        {
            AddUserToDatabase("joe");
            var sut = new MongoSuperService();
            var actual = sut.GetUserById("joe");
            actual.Should().NotBeNull(because: "User is in the database.");
            actual.UserName.Should().NotBeNull(because: "It is requried to be stored in the database.")
                .And.Should().BeEquivalentTo("joe", because: "That is the name we searched by.");
        }

        [TestMethod]
        public void GiveNullIfUserDoesNotExist()
        {
            var sut = new MongoSuperService();
            var actual = sut.GetUserById("jilly");
            actual.Should().BeNull(because: "User does not exist.");
        }

        [TestMethod]
        public void GiveAllTheProfilesForUsers()
        {  
            var collection = new Profile[] { new Profile { Name  = "The Very Best Shit." } };
            AddUserToDatabase("alex", values: collection);
            var sut = new MongoSuperService();
            var actual = sut.GetProfilesForUser("alex");
            actual.Should().BeEquivalentTo(collection);
        }


        private void AddUserToDatabase(string username,string password = "Not A God", params Profile[] values)
        {
            var sut = new MongoSuperService();
            if (sut.GetAllUsers().Any(c => c.UserName == username))
            {
                sut.AddUser(new User()
                {
                    UserName = username,
                    Password = password,
                    Profiles = values
                });
            }
        }
    }
}
