using System;

namespace PanasheCybersecurityChatbot
{
    public class MemoryStore
    {
        public string UserName { get; set; } = "User";
        public string FavouriteTopic { get; set; } = "";

        public void Store(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;

            if (key.ToLower().Contains("name"))
            {
                UserName = value.Trim();
            }
            else if (key.ToLower().Contains("topic") || key.ToLower().Contains("interest"))
            {
                FavouriteTopic = value.Trim();
            }
        }

        public string GetPersonalisedOpener()
        {
            if (!string.IsNullOrEmpty(FavouriteTopic))
            {
                return $"As someone interested in {FavouriteTopic}, ";
            }
            return "";
        }

        public string GetMemoryStatus()
        {
            if (!string.IsNullOrEmpty(FavouriteTopic))
            {
                return $"\n\n(I remember you're interested in {FavouriteTopic})";
            }
            return "";
        }
    }
}