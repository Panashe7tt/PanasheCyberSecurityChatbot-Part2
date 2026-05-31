using System;

namespace PanasheCybersecurityChatbot
{
    // DELEGATE (required by assignment)
    public delegate void MessageLogger(string message);

    public static class ChatLogger
    {
        public static void LogMessage(string message)
        {
            Console.WriteLine($"[CHAT LOG {DateTime.Now}] {message}");
        }
    }
}