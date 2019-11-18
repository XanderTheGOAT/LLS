using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using LightLinkModels;
using System.IO;
using System.Linq;
namespace LightLinkLibrary.Data_Access.Implementations
{
    public class FileProfileService : IProfileService
    {

        const string PROFILES_FILE = "RGBProfiles\\Profile.txt";
        const string CURRENT_PROFILE_NAME = "RGBProfiles\\CurrentProfile.txt";
        List<Profile> profiles;
        Profile currentProfile;

        public FileProfileService()
        {
            CheckForFiles();
            profiles = ((Newtonsoft.Json.Linq.JArray)Deserialize(PROFILES_FILE)).ToObject<List<Profile>>();
            profiles = profiles == null ? new List<Profile>() : profiles;
            currentProfile = ((Newtonsoft.Json.Linq.JToken)Deserialize(CURRENT_PROFILE_NAME)).ToObject<Profile>();
            currentProfile = currentProfile == null ? new Profile() : currentProfile;
        }

        public void AddProfileToUser(string username, Profile dto)
        {
            profiles.Add(dto);
            Serialize(profiles, PROFILES_FILE);
        }

        public bool Exists(string username, string name)
        {
            var existingProfile = profiles.FirstOrDefault(p => p.Name.Equals(name));
            return existingProfile != null;
        }

        public Profile GetActiveForUser(string username)
        {
            return currentProfile;
        }

        public IEnumerable<Profile> GetProfilesForUser(string username)
        {
            return profiles;
        }

        public void RemoveProfileFromUser(string username, string name)
        {
            var removeProfile = profiles.FirstOrDefault(p => p.Name.Equals(name));
            profiles.Remove(removeProfile);
            Serialize(profiles, PROFILES_FILE);
        }

        public void SetActiveForUser(string username, Profile dto)
        {

            currentProfile.IsActive = false;
            profiles[profiles.IndexOf(currentProfile)].IsActive = false;
            dto.IsActive = true;
            profiles[profiles.IndexOf(dto)].IsActive = true;
            Serialize(dto, CURRENT_PROFILE_NAME);
        }

        public void UpdateProfileOnUser(string username, string name, Profile dto)
        {
            var updatingProfile = profiles.FirstOrDefault(p => p.Name.Equals(name));
            profiles[profiles.IndexOf(updatingProfile)] = dto;
        }

        private void CheckForFiles()
        {
            if (!File.Exists(PROFILES_FILE))
            {
                var stream = File.Create(PROFILES_FILE);
                stream.Close();
                currentProfile = new Profile();
                currentProfile.Name = "Default Profile";
                currentProfile.Configurations = new Dictionary<string, dynamic>();
                currentProfile.Configurations.Add("mouse", "red");
                currentProfile.IsActive = true;
                List<Profile> profiles = new List<Profile>();
                profiles.Add(currentProfile);
                Serialize(profiles, PROFILES_FILE);
            }

            if (!File.Exists(CURRENT_PROFILE_NAME))
            {
                var stream = File.Create(CURRENT_PROFILE_NAME);
                stream.Close();
                Serialize(currentProfile, CURRENT_PROFILE_NAME);
            }
        }

        public static void Serialize(object obj, string filePath)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter(filePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, obj);
            }
        }

        public static object Deserialize(string filePath)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StreamReader(filePath))
            using (var reader = new JsonTextReader(sw))
            {
                return serializer.Deserialize(reader);
            }
        }

    }
}
