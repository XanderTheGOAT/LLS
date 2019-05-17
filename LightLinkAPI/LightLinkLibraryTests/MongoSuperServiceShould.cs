using System;
using System.Collections.Generic;
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
        //[TestMethod]
        public void TestAddComputer()
        {
            Computer c = new Computer();
            c.Name = "Cotton Eye Joe";
            c.UserName = "Joe";
            c.ConnectedDevices = new string[] { "Beep", "Boop" };

            MongoSuperService mss = new MongoSuperService();
            mss.AddComputer(c);
        }

        //[TestMethod]
        public void TestAddUser()
        {
            User u = new User();
            u.UserName = "Ben";
            u.Password = "BenIsGreat";
            u.Profiles = new List<Profile>();

            MongoSuperService mss = new MongoSuperService();
            mss.AddUser(u);
        }

        //[TestMethod]
        public void TestAddProfileToUser()
        {
            Profile p = new Profile();
            p.Name = "Rainbow";
            p.Configurations = new Dictionary<string, dynamic>();
            p.Configurations.Add("color", "red");

            MongoSuperService mss = new MongoSuperService();
            mss.AddProfileToUser("Ben", p);
        }

        [TestMethod]
        public void TestBenExists()
        {
            MongoSuperService mss = new MongoSuperService();
            Assert.IsTrue(mss.Exists("Ben"));
        }

        [TestMethod]
        public void TestJohnDoesNotExist()
        {
            MongoSuperService mss = new MongoSuperService();
            Assert.IsFalse(mss.Exists("John"));
        }

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
