using FluentAssertions;
using LightLinkLibrary.Data_Access.Implementations;
using LightLinkModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LightLinkLibraryTests
{
    [TestClass]
    public class MongoSuperServiceShould
    {
        [TestMethod]
        public void TestAddComputer()
        {
            var c = new Computer
            {
                Name = "Cotton Eye Joe",
                UserName = "Joe",
                ConnectedDevices = new string[] { "Beep", "Boop" }.ToList()
            };

            var mss = new MongoSuperService("69.27.22.253");
            mss.AddComputer(c);
        }

        [TestMethod]
        public void TestAddUser()
        {
            var u = new User
            {
                UserName = "Ben",
                Password = "BenIsGreat",
                Profiles = new List<Profile>()
            };

            var mss = new MongoSuperService("69.27.22.253");
            mss.AddUser(u);
        }

        [TestMethod]
        public void TestAddProfileToUser()
        {
            var mss = new MongoSuperService("69.27.22.253");
            var p = new Profile
            {
                Name = "Rainbow",
                Configurations = new Dictionary<string, dynamic>()
            };
            if (!mss.Exists("Ben"))
            {
                var u = new User
                {
                    UserName = "Ben",
                    Password = "BenIsGreat",
                    Profiles = new List<Profile>()
                };
                mss.AddUser(u);
            }
            p.Configurations.Add("keyboard", "red");
            mss.AddProfileToUser("Ben", p);
        }

        [TestMethod]
        public void TestBenExists()
        {
            var mss = new MongoSuperService("69.27.22.253");
            Assert.IsTrue(mss.Exists("Ben"));
        }

        [TestMethod]
        public void TestJohnDoesNotExist()
        {
            var mss = new MongoSuperService("69.27.22.253");
            Assert.IsFalse(mss.Exists("John"));
        }

        [TestMethod]
        public void GiveUserIfUserExists()
        {
            AddUserToDatabase("joe");
            var sut = new MongoSuperService("69.27.22.253");
            var actual = sut.GetUserById("joe");
            actual.Should().NotBeNull(because: "User is in the database.");
            actual.UserName.Should().NotBeNull(because: "It is requried to be stored in the database.");
            actual.UserName.Should().Be("joe");
        }

        [TestMethod]
        public void GiveNullIfUserDoesNotExist()
        {
            var sut = new MongoSuperService("69.27.22.253");
            var actual = sut.GetUserById("jilly");
            actual.Should().BeNull(because: "User does not exist.");
        }

        [TestMethod]
        public void GiveAllTheProfilesForUsers()
        {
            var collection = new Profile[] { new Profile { Name = "The Very Best Shit." } };
            AddUserToDatabase("alex", values: collection);
            var sut = new MongoSuperService("69.27.22.253");
            var actual = sut.GetProfilesForUser("alex");
            actual.Should().BeEquivalentTo(collection);
        }


        private void AddUserToDatabase(string username, string password = "Not A God", params Profile[] values)
        {
            var sut = new MongoSuperService("69.27.22.253");
            if (!sut.GetAllUsers().Any(c => c.UserName == username))
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
