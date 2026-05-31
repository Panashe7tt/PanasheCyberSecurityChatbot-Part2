using System;
using System.Collections.Generic;
using System.Linq;

namespace Panashe.CybersecurityAwareness
{
    public enum Sentiment
    {
        Neutral,
        Happy,
        Sad,
        Worried,
        Frustrated,
        Curious,
        Angry,
        Confused,
        Excited,
        Fearful,
        Relieved,
        Shocked
    }

    public class SentimentRecord
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; } = string.Empty;
        public Sentiment Sentiment { get; set; }
    }

    public class SentimentDetector
    {
        private readonly Dictionary<Sentiment, Dictionary<string, int>> _keywords;
        private readonly List<SentimentRecord> _history = new();

        public SentimentDetector()
        {
            _keywords = new Dictionary<Sentiment, Dictionary<string, int>>
            {
                {
                    Sentiment.Happy, new Dictionary<string, int>
                    {
                        { "great", 2 }, { "awesome", 3 }, { "amazing", 3 },
                        { "happy", 2 }, { "love", 3 }, { "excellent", 3 },
                        { "good", 1 }, { "nice", 1 }
                    }
                },
                {
                    Sentiment.Sad, new Dictionary<string, int>
                    {
                        { "sad", 2 }, { "unhappy", 2 }, { "down", 1 },
                        { "upset", 2 }, { "depressed", 3 }
                    }
                },
                {
                    Sentiment.Worried, new Dictionary<string, int>
                    {
                        { "worried", 2 }, { "anxious", 3 }, { "nervous", 2 },
                        { "stress", 1 }, { "panic", 3 }, { "concerned", 2 }
                    }
                },
                {
                    Sentiment.Frustrated, new Dictionary<string, int>
                    {
                        { "frustrated", 3 }, { "annoyed", 2 },
                        { "irritated", 2 }, { "mad", 2 }, { "fed up", 3 }
                    }
                },
                {
                    Sentiment.Curious, new Dictionary<string, int>
                    {
                        { "why", 2 }, { "how", 2 }, { "what", 1 },
                        { "curious", 2 }, { "wonder", 2 }, { "i wonder", 3 }
                    }
                },
                {
                    Sentiment.Angry, new Dictionary<string, int>
                    {
                        { "angry", 2 }, { "furious", 3 }, { "rage", 3 },
                        { "hate", 3 }, { "outraged", 3 }
                    }
                },
                {
                    Sentiment.Confused, new Dictionary<string, int>
                    {
                        { "confused", 2 }, { "lost", 2 },
                        { "puzzled", 2 }, { "don't understand", 3 }
                    }
                },
                {
                    Sentiment.Excited, new Dictionary<string, int>
                    {
                        { "excited", 2 }, { "thrilled", 3 },
                        { "pumped", 2 }, { "can't wait", 3 }
                    }
                },
                {
                    Sentiment.Fearful, new Dictionary<string, int>
                    {
                        { "scared", 2 }, { "afraid", 2 },
                        { "fear", 1 }, { "terrified", 3 }
                    }
                },
                {
                    Sentiment.Relieved, new Dictionary<string, int>
                    {
                        { "relieved", 3 }, { "phew", 3 }, { "thankfully", 2 }
                    }
                },
                {
                    Sentiment.Shocked, new Dictionary<string, int>
                    {
                        { "shocked", 3 }, { "stunned", 3 },
                        { "wow", 2 }, { "unbelievable", 3 }
                    }
                }
            };
        }

        // MAIN DETECTION METHOD
        public Sentiment Detect(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return Sentiment.Neutral;

            string text = message.ToLower();

            var scores = new Dictionary<Sentiment, int>();

            foreach (var sentiment in _keywords)
            {
                int score = 0;

                foreach (var keyword in sentiment.Value)
                {
                    if (text.Contains(keyword.Key))
                        score += keyword.Value;
                }

                scores[sentiment.Key] = score;
            }

            var best = scores.OrderByDescending(s => s.Value).First();
            var result = best.Value == 0 ? Sentiment.Neutral : best.Key;

            _history.Add(new SentimentRecord
            {
                Timestamp = DateTime.Now,
                Message = message,
                Sentiment = result
            });

            return result;
        }

        // FIXED: matches your old method name
        public string GetSentimentResponse(Sentiment sentiment)
        {
            return GetResponse(sentiment);
        }

        // CORE RESPONSE LOGIC
        public string GetResponse(Sentiment sentiment)
        {
            return sentiment switch
            {
                Sentiment.Happy => "That's great to hear! 😊",
                Sentiment.Sad => "I'm sorry you're feeling that way. 💙",
                Sentiment.Worried => "It's okay to feel worried. Let's work through it.",
                Sentiment.Frustrated => "That sounds frustrating. Take a break if needed.",
                Sentiment.Curious => "Good question! Let's explore it.",
                Sentiment.Angry => "I understand you're upset. Let's try to calm things down.",
                Sentiment.Confused => "Let me help clear that up for you.",
                Sentiment.Excited => "That’s exciting! 🎉",
                Sentiment.Fearful => "You're safe—let’s handle this together.",
                Sentiment.Relieved => "I'm glad you feel relieved.",
                Sentiment.Shocked => "That sounds really shocking!",
                _ => "Thanks for sharing."
            };
        }

        // HISTORY (for your messages.json system)
        public List<SentimentRecord> GetHistory()
        {
            return _history;
        }

        // SUMMARY ANALYTICS
        public Dictionary<Sentiment, int> GetSentimentSummary()
        {
            return _history
                .GroupBy(h => h.Sentiment)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}