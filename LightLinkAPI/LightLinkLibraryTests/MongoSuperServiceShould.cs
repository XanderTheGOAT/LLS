using System;
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
            SeedDatabase("joe");
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

        private void SeedDatabase(string person)
        {
            var sut = new MongoSuperService();
            sut.AddUser(new User()
            {
                UserName = person,
                Password = "Not A GOD"
            });
        }
    }
}
