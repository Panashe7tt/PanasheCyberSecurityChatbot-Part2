using Panashe.CybersecurityAwareness;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PanasheCybersecurityChatbot
{
    public partial class MainWindow : Window
    {
        private readonly ChatBot _chatBot;

        public MainWindow()
        {
            InitializeComponent();

            _chatBot = new ChatBot();

            LoadLogo();
            PlayVoiceGreeting();

            AppendBotMessage(_chatBot.GetGreeting());
        }

        private void LoadLogo()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nash-logo.jpeg");

                if (File.Exists(path))
                {
                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(path);
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    img.EndInit();

                    LogoImage.Source = img;
                }
            }
            catch { }
        }

        private void PlayVoiceGreeting()
        {
            try { Audio.PlayGreeting(); }
            catch { }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SendMessage();
        }

        private void SendMessage()
        {
            string text = UserInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(text)) return;

            AppendUserMessage(text);
            UserInput.Clear();

            string response = _chatBot.ProcessInput(text);

            AppendBotMessage(response);
            ChatScroll.ScrollToEnd();
        }

        // USER BUBBLE
        private void AppendUserMessage(string message)
        {
            ChatPanel.Children.Add(new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(0, 255, 204)),
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(12),
                Margin = new Thickness(120, 8, 0, 8),
                HorizontalAlignment = HorizontalAlignment.Right,
                Child = new TextBlock
                {
                    Text = message,
                    Foreground = Brushes.Black,
                    TextWrapping = TextWrapping.Wrap
                }
            });
        }

        // BOT BUBBLE (WITH EMOTION COLORS + TYPING EFFECT)
        private async void AppendBotMessage(string message)
        {
            Sentiment sentiment = _chatBot.GetLastSentiment();

            Border bubble = new Border
            {
                Background = new SolidColorBrush(GetEmotionColor(sentiment)),
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(12),
                Margin = new Thickness(0, 8, 120, 8),
                HorizontalAlignment = HorizontalAlignment.Left,
                MaxWidth = 600
            };

            TextBlock text = new TextBlock
            {
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap
            };

            bubble.Child = text;
            ChatPanel.Children.Add(bubble);

            string display = "Nash:\n\n";

            foreach (char c in message)
            {
                display += c;
                text.Text = display;
                await Task.Delay(10);
            }
        }

        // EMOTION COLORS
        private Color GetEmotionColor(Sentiment s)
        {
            return s switch
            {
                Sentiment.Worried => Color.FromRgb(255, 99, 71),
                Sentiment.Fearful => Color.FromRgb(255, 69, 0),
                Sentiment.Frustrated => Color.FromRgb(255, 140, 0),
                Sentiment.Angry => Color.FromRgb(220, 20, 60),
                Sentiment.Happy => Color.FromRgb(0, 200, 120),
                Sentiment.Excited => Color.FromRgb(0, 180, 255),
                Sentiment.Relieved => Color.FromRgb(72, 209, 204),
                Sentiment.Curious => Color.FromRgb(123, 104, 238),
                Sentiment.Shocked => Color.FromRgb(255, 215, 0),
                Sentiment.Confused => Color.FromRgb(169, 169, 169),
                _ => Color.FromRgb(40, 40, 60)
            };
        }
    }
}