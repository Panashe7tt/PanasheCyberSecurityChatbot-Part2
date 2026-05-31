using Panashe.CybersecurityAwareness;
using System;

namespace PanasheCybersecurityChatbot
{
    public class ChatBot
    {
        private readonly KeywordResponder _keywordResponder;
        private readonly SentimentDetector _sentimentDetector;
        private readonly MemoryStore _memoryStore;

        // Delegate
        public MessageLogger Logger;

        private bool _awaitingName = true;
        private string _lastTopic = "";
        private Sentiment _lastSentiment = Sentiment.Neutral;

        public ChatBot()
        {
            _keywordResponder = new KeywordResponder();
            _sentimentDetector = new SentimentDetector();
            _memoryStore = new MemoryStore();

            // Assign delegate
            Logger = ChatLogger.LogMessage;
        }

        public string GetGreeting()
        {
            return "Hello! I'm Nash, your Cybersecurity Awareness Assistant.\n\nWhat's your name?";
        }

        public string ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please type something so I can help you!";

            Logger?.Invoke(input);

            string lowerInput = input.ToLower().Trim();

            // ---------------- MEMORY INPUT ----------------
            if (lowerInput.Contains("i'm interested in"))
            {
                string topic = input.Substring(input.ToLower().IndexOf("interested in") + 13).Trim();
                _memoryStore.Store("topic", topic);

                return $"Great! I'll remember you're interested in {topic}.";
            }

            if (lowerInput.Contains("my favourite topic is"))
            {
                string topic = input.Substring(input.ToLower().IndexOf("my favourite topic is") + 20).Trim();
                _memoryStore.Store("topic", topic);

                return $"Nice! I'll remember your favourite topic is {topic}.";
            }

            // ---------------- NAME HANDLING ----------------
            if (_awaitingName)
            {
                string name = input.Trim();

                if (name.ToLower().StartsWith("my name is "))
                {
                    name = name.Substring(11).Trim();
                }

                _memoryStore.UserName = name;
                _awaitingName = false;

                return $"Nice to meet you, {name}! 👋\n\nAsk me about cybersecurity topics like passwords, phishing, malware, scams, privacy, etc.";
            }

            // ---------------- FOLLOW-UP FLOW ----------------
            if (lowerInput.Contains("tell me more") ||
                lowerInput.Contains("explain more") ||
                lowerInput.Contains("more details") ||
                lowerInput.Contains("another tip") ||
                lowerInput.Contains("next tip") ||
                lowerInput.Contains("continue") ||
                lowerInput.Contains("tell me something else"))
            {
                if (!string.IsNullOrEmpty(_lastTopic))
                {
                    return "Here's more info:\n\n" +
                           _keywordResponder.GetResponse(_lastTopic);
                }

                return "Please ask me about a cybersecurity topic first.";
            }

            // ---------------- SENTIMENT ----------------
            Sentiment sentiment = _sentimentDetector.Detect(input);
            _lastSentiment = sentiment;

            string sentimentResponse = _sentimentDetector.GetSentimentResponse(sentiment);

            // ---------------- KEYWORD RESPONSE ----------------
            string keywordResponse = _keywordResponder.GetResponse(input);

            if (!string.IsNullOrEmpty(keywordResponse))
            {
                _lastTopic = input;

                string response = "";

                if (!string.IsNullOrEmpty(sentimentResponse))
                    response += sentimentResponse + "\n\n";

                response += _memoryStore.GetPersonalisedOpener();
                response += keywordResponse;
                response += _memoryStore.GetMemoryStatus();

                return response.Trim();
            }

            return "Try keywords like: password, phishing, malware, privacy, scam.";
        }

        public Sentiment GetLastSentiment()
        {
            return _lastSentiment;
        }
    }
}