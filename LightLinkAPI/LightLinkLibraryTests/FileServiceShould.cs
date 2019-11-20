using LightLinkLibrary.Data_Access.Implementations;
using LightLinkModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightLinkLibraryTests
{
    [TestClass]
    public class FileServiceShould
    {
        [TestMethod]
        public void TestAddProfile()
        {
            var profile = new Profile
            {
                Name = "Green",
                Configurations = new Dictionary<string, dynamic>()
            };
            profile.Configurations.Add("mouse", "00ffff");
            var fservice = new FileProfileService();
            fservice.AddProfileToUser(null, profile);
            
        }

        [TestMethod]
        public void ChangeCurrentProfile()
        {
            var fservice = new FileProfileService();
            List<Profile> profiles = (List<Profile>)fservice.GetProfilesForUser(null);
            fservice.SetActiveForUser(null, profiles[profiles.Count-1]);
            Profile expected = profiles[0];
            Profile actual = fservice.GetActiveForUser(null);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveGreenProfile()
        {
            var fservice = new FileProfileService();
            fservice.RemoveProfileFromUser(null, "Green");
        }

        [TestMethod]
        public void UpdateYellowProfile()
        {
            var fservice = new FileProfileService();
            Profile yellow = new Profile() {
                Name = "Yellow",
                Configurations = new Dictionary<string, dynamic>()
            };
            yellow.Configurations.Add("mouse", "ffff00");

            fservice.UpdateProfileOnUser(null, "Yellow", yellow);
        }
    }
}
