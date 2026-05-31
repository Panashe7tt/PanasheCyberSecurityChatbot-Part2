using System;
using System.Collections.Generic;
using System.Linq;

namespace Panashe.CybersecurityAwareness
{
    public class KeywordResponder
    {
        private readonly Random _random = new Random();

        // Each topic maps to a list of responses so we can rotate them
        private readonly Dictionary<string[], List<string>> _topicResponses = new Dictionary<string[], List<string>>
        {
            // ── PASSWORD ────────────────────────────────────────────────────────────────
            {
                new[] { "password", "passwords", "passphrase" },
                new List<string>
                {
                    "🔐 Password Safety Tips:\n" +
                    "• Use at least 12–16 characters combining letters, numbers & symbols.\n" +
                    "• Never reuse the same password across multiple sites.\n" +
                    "• Use a trusted password manager (e.g. Bitwarden, 1Password).\n" +
                    "• Avoid obvious choices like 'password123' or your birthday.\n" +
                    "• Enable Two-Factor Authentication (2FA) wherever possible.",

                    "🔑 Strong Password Advice:\n" +
                    "• A passphrase like 'Coffee!Rain#42Cats' is both strong and memorable.\n" +
                    "• Change passwords immediately if you suspect a breach.\n" +
                    "• Never share your password — not even with IT support.\n" +
                    "• Check haveibeenpwned.com to see if your email was in a data breach."
                }
            },

            // ── PHISHING ─────────────────────────────────────────────────────────────
            {
                new[] { "phishing", "phish", "fake email", "suspicious email", "spear phishing", "smishing", "vishing" },
                new List<string>
                {
                    "🎣 Phishing Awareness:\n" +
                    "• Phishing emails pretend to be banks, services, or colleagues to steal your info.\n" +
                    "• Check the sender's actual email address — not just the display name.\n" +
                    "• Hover over links before clicking to see the real destination URL.\n" +
                    "• Urgent messages like 'Your account will be suspended!' are red flags.\n" +
                    "• When in doubt, go directly to the website instead of clicking email links.",

                    "📧 Types of Phishing:\n" +
                    "• Spear Phishing — targeted attacks using your personal info.\n" +
                    "• Smishing — phishing via SMS/text messages.\n" +
                    "• Vishing — phishing over phone calls.\n" +
                    "• Never provide OTP codes or passwords to callers, even if they claim to be your bank.\n" +
                    "• Report suspicious emails to your IT department or email provider."
                }
            },

            // ── SCAM ─────────────────────────────────────────────────────────────────
            {
                new[] { "scam", "scams", "fraud", "fraudulent", "con", "trick", "fake", "deception" },
                new List<string>
                {
                    "🚨 Common Online Scams to Watch Out For:\n" +
                    "• Romance scams — someone builds trust online then asks for money.\n" +
                    "• Lottery/prize scams — 'You've won! Just pay a small fee.'\n" +
                    "• Tech support scams — fake alerts saying your PC is infected.\n" +
                    "• Investment scams — promises of guaranteed high returns.\n" +
                    "• Job scams — fake jobs asking for personal banking details upfront.",

                    "💡 How to Avoid Scams:\n" +
                    "• If it sounds too good to be true, it almost certainly is.\n" +
                    "• Never send money, gift cards, or crypto to someone you haven't met.\n" +
                    "• Verify unexpected calls or emails by contacting the company directly.\n" +
                    "• Government agencies will NEVER demand immediate payment by phone.\n" +
                    "• Report scams to your national consumer protection body."
                }
            },

            // ── MALWARE ──────────────────────────────────────────────────────────────
            {
                new[] { "malware", "virus", "trojan", "ransomware", "spyware", "adware", "worm", "keylogger", "rootkit" },
                new List<string>
                {
                    "🦠 Malware Basics:\n" +
                    "• Malware is software designed to damage or gain unauthorised access to systems.\n" +
                    "• Types include: viruses, worms, trojans, ransomware, spyware, and adware.\n" +
                    "• Install reputable antivirus software and keep it up to date.\n" +
                    "• Never download attachments or software from untrusted sources.\n" +
                    "• Regularly back up your data so ransomware can't hold you hostage.",

                    "🛡️ Ransomware Specifically:\n" +
                    "• Ransomware encrypts your files and demands payment to restore them.\n" +
                    "• Do NOT pay the ransom — there's no guarantee you'll get your data back.\n" +
                    "• Disconnect infected devices from the network immediately.\n" +
                    "• Maintain offline backups (external hard drive or cloud) updated regularly.\n" +
                    "• Keep your OS and applications patched — ransomware exploits known vulnerabilities."
                }
            },

            // ── PRIVACY ──────────────────────────────────────────────────────────────
            {
                new[] { "privacy", "personal data", "data protection", "gdpr", "popia", "personal information" },
                new List<string>
                {
                    "🔏 Online Privacy Tips:\n" +
                    "• Review the privacy settings on ALL your social media accounts.\n" +
                    "• Don't overshare personal info (birthday, phone number, address) publicly.\n" +
                    "• Use a privacy-focused browser like Firefox or Brave.\n" +
                    "• Opt out of data tracking wherever possible.\n" +
                    "• Read app permissions — does a flashlight app really need your contacts?",

                    "📋 Data Protection Rights:\n" +
                    "• In South Africa, POPIA protects your personal information.\n" +
                    "• You have the right to know what data companies hold about you.\n" +
                    "• You can request deletion of your data (the 'right to be forgotten').\n" +
                    "• Be cautious when filling in online forms — only provide what's truly required.\n" +
                    "• Use separate email addresses for shopping, banking, and personal use."
                }
            },

            // ── VPN ───────────────────────────────────────────────────────────────────
            {
                new[] { "vpn", "virtual private network", "proxy" },
                new List<string>
                {
                    "🌐 What is a VPN?\n" +
                    "• A VPN (Virtual Private Network) encrypts your internet connection.\n" +
                    "• It hides your real IP address and makes your browsing more private.\n" +
                    "• Use a VPN on public Wi-Fi (cafés, airports) to prevent eavesdropping.\n" +
                    "• Choose reputable paid VPNs (e.g. ProtonVPN, NordVPN, Mullvad).\n" +
                    "• Free VPNs often log and sell your data — use with caution!",

                    "📡 VPN Use Cases:\n" +
                    "• Secure remote work connections to your company network.\n" +
                    "• Bypass geo-restrictions on content (where legally permitted).\n" +
                    "• Prevent your ISP from tracking your browsing habits.\n" +
                    "• A VPN does NOT make you fully anonymous — combine with good hygiene.\n" +
                    "• Always check that your VPN has a 'kill switch' to protect you if it drops."
                }
            },

            // ── TWO FACTOR AUTHENTICATION ────────────────────────────────────────────
            {
                new[] { "2fa", "two factor", "two-factor", "mfa", "multi factor", "multi-factor", "authenticator", "otp", "one time" },
                new List<string>
                {
                    "🔑 Two-Factor Authentication (2FA):\n" +
                    "• 2FA adds a second layer of security beyond just a password.\n" +
                    "• Even if someone steals your password, they can't log in without the second factor.\n" +
                    "• Use an authenticator app (Google Authenticator, Authy, Microsoft Authenticator).\n" +
                    "• SMS-based 2FA is better than nothing, but authenticator apps are safer.\n" +
                    "• Enable 2FA on email, banking, social media, and any sensitive accounts."
                }
            },

            // ── SOCIAL ENGINEERING ───────────────────────────────────────────────────
            {
                new[] { "social engineering", "manipulation", "pretexting", "baiting", "tailgating", "impersonation" },
                new List<string>
                {
                    "🎭 Social Engineering:\n" +
                    "• Social engineering manipulates PEOPLE rather than attacking technology.\n" +
                    "• Pretexting — attacker creates a fake scenario to extract info.\n" +
                    "• Baiting — leaving infected USB drives in public hoping someone plugs one in.\n" +
                    "• Tailgating — following someone into a secure area without authorisation.\n" +
                    "• Always verify someone's identity before sharing sensitive information.",

                    "🧠 Defending Against Social Engineering:\n" +
                    "• Be sceptical of unsolicited requests for information.\n" +
                    "• Verify callers by hanging up and calling the official number back.\n" +
                    "• Don't plug in unknown USB drives or devices.\n" +
                    "• Security awareness training is the best defence.\n" +
                    "• Trust your instincts — if something feels off, it probably is."
                }
            },

            // ── ENCRYPTION ───────────────────────────────────────────────────────────
            {
                new[] { "encryption", "encrypt", "decrypt", "cipher", "https", "ssl", "tls", "end to end" },
                new List<string>
                {
                    "🔒 Encryption Explained:\n" +
                    "• Encryption scrambles data so only authorised parties can read it.\n" +
                    "• HTTPS (the padlock in your browser) encrypts data between you and websites.\n" +
                    "• End-to-end encryption (E2EE) in apps like Signal means only you and the recipient can read messages.\n" +
                    "• Encrypt sensitive files on your device (BitLocker on Windows, FileVault on Mac).\n" +
                    "• Always check for HTTPS before entering passwords or payment details online."
                }
            },

            // ── FIREWALL ──────────────────────────────────────────────────────────────
            {
                new[] { "firewall", "network security", "intrusion", "ids", "ips" },
                new List<string>
                {
                    "🧱 Firewalls & Network Security:\n" +
                    "• A firewall monitors and controls incoming/outgoing network traffic.\n" +
                    "• Keep your OS firewall enabled at all times.\n" +
                    "• Home routers have built-in firewalls — ensure yours is configured properly.\n" +
                    "• An IDS (Intrusion Detection System) alerts you to suspicious activity.\n" +
                    "• Segment your home network — put IoT devices on a separate guest network."
                }
            },

            // ── UPDATES / PATCHING ───────────────────────────────────────────────────
            {
                new[] { "update", "updates", "patch", "patching", "software update", "os update", "outdated" },
                new List<string>
                {
                    "🔄 Keep Everything Updated:\n" +
                    "• Software updates often include critical security patches.\n" +
                    "• Enable automatic updates for your OS, browser, and antivirus.\n" +
                    "• Unpatched software is one of the most common attack vectors.\n" +
                    "• Update your router firmware too — it's often forgotten!\n" +
                    "• End-of-life software (e.g. Windows 7) no longer receives patches — upgrade it."
                }
            },

            // ── PUBLIC WI-FI ─────────────────────────────────────────────────────────
            {
                new[] { "public wifi", "public wi-fi", "wifi", "wi-fi", "hotspot", "open network" },
                new List<string>
                {
                    "📶 Public Wi-Fi Safety:\n" +
                    "• Avoid accessing banking or sensitive accounts on public Wi-Fi.\n" +
                    "• Attackers can set up fake hotspots with names like 'Free_Airport_WiFi'.\n" +
                    "• Use a VPN when connecting to any public network.\n" +
                    "• Turn off 'auto-connect to Wi-Fi' on your phone.\n" +
                    "• Stick to HTTPS websites and avoid anything asking for sensitive info."
                }
            },

            // ── IDENTITY THEFT ───────────────────────────────────────────────────────
            {
                new[] { "identity theft", "identity fraud", "stolen identity", "impersonate me", "account takeover" },
                new List<string>
                {
                    "🪪 Identity Theft:\n" +
                    "• Identity theft happens when someone uses your personal info without permission.\n" +
                    "• Criminals use stolen IDs to open bank accounts, apply for loans, and more.\n" +
                    "• Monitor your bank and credit statements regularly for unusual activity.\n" +
                    "• Shred physical documents containing personal info before discarding.\n" +
                    "• Place a fraud alert with credit bureaus if you suspect your identity has been stolen."
                }
            },

            // ── DARK WEB ─────────────────────────────────────────────────────────────
            {
                new[] { "dark web", "darkweb", "deep web", "tor" },
                new List<string>
                {
                    "🕸️ Dark Web Awareness:\n" +
                    "• The Dark Web is a hidden part of the internet not indexed by search engines.\n" +
                    "• Stolen credentials, credit card data, and malware are sold there.\n" +
                    "• Use services like HaveIBeenPwned to check if your data has been leaked.\n" +
                    "• If your credentials appear on the dark web, change passwords immediately.\n" +
                    "• Browsing the dark web carries legal and security risks — avoid unless necessary."
                }
            },

            // ── BACKUP ───────────────────────────────────────────────────────────────
            {
                new[] { "backup", "backups", "data backup", "cloud backup", "restore" },
                new List<string>
                {
                    "💾 Data Backup Best Practices:\n" +
                    "• Follow the 3-2-1 rule: 3 copies, on 2 different media, 1 offsite/cloud.\n" +
                    "• Automate backups so you never forget.\n" +
                    "• Test your backups regularly — a backup you've never restored may not work.\n" +
                    "• Cloud backups (Google Drive, OneDrive, iCloud) are convenient but encrypt sensitive files first.\n" +
                    "• Keep at least one backup offline (external drive) to protect against ransomware."
                }
            },

            // ── SAFE BROWSING ────────────────────────────────────────────────────────
            {
                new[] { "browsing", "safe browsing", "browser", "website", "url", "link", "download" },
                new List<string>
                {
                    "🌍 Safe Browsing Habits:\n" +
                    "• Always check for HTTPS and the padlock before entering any sensitive data.\n" +
                    "• Be wary of shortened URLs — use a URL expander to see the real destination.\n" +
                    "• Download software only from official websites or trusted app stores.\n" +
                    "• Install a browser extension like uBlock Origin to block malicious ads.\n" +
                    "• Clear cookies and cache regularly to remove stored tracking data."
                }
            },

            // ── SOCIAL MEDIA SECURITY ────────────────────────────────────────────────
            {
                new[] { "social media", "facebook", "instagram", "twitter", "linkedin", "tiktok", "snapchat" },
                new List<string>
                {
                    "📱 Social Media Security:\n" +
                    "• Set your profiles to 'private' and only accept connections you know.\n" +
                    "• Oversharing your location, routine, or travel plans is a safety risk.\n" +
                    "• Be cautious of quizzes that ask for personal details — they harvest data.\n" +
                    "• Use a unique, strong password and enable 2FA on every social account.\n" +
                    "• Think before you post — content online can last forever and be screenshot."
                }
            },

            // ── CYBER BULLYING ───────────────────────────────────────────────────────
            {
                new[] { "cyberbullying", "cyber bullying", "harassment", "online abuse", "trolling" },
                new List<string>
                {
                    "🛑 Cyberbullying & Online Harassment:\n" +
                    "• Cyberbullying includes sending threatening messages, spreading rumours online, or sharing private images without consent.\n" +
                    "• Block and report abusive accounts — don't engage with trolls.\n" +
                    "• Save evidence (screenshots) before blocking if you plan to report.\n" +
                    "• Reach out to a trusted adult, counsellor, or the platform's safety team.\n" +
                    "• In South Africa, report cybercrime to the SAPS or https://cybercrime.org.za"
                }
            },

            // ── ZERO DAY ─────────────────────────────────────────────────────────────
            {
                new[] { "zero day", "zero-day", "vulnerability", "exploit", "cve" },
                new List<string>
                {
                    "⚠️ Zero-Day Vulnerabilities:\n" +
                    "• A zero-day is a flaw in software that the vendor doesn't yet know about.\n" +
                    "• Until a patch is released, all users of that software are at risk.\n" +
                    "• Keep software updated — patches often address newly discovered vulnerabilities.\n" +
                    "• Use defence-in-depth: no single control should be your only protection.\n" +
                    "• Follow security news sites like Krebs on Security or The Hacker News."
                }
            },

            // ── IOT SECURITY ─────────────────────────────────────────────────────────
            {
                new[] { "iot", "internet of things", "smart device", "smart home", "smart tv", "camera", "cctv" },
                new List<string>
                {
                    "🏠 IoT & Smart Device Security:\n" +
                    "• Change default usernames and passwords on ALL smart devices immediately.\n" +
                    "• Keep device firmware updated — smart TVs, routers, cameras all need patches.\n" +
                    "• Put smart home devices on a separate guest Wi-Fi network.\n" +
                    "• Disable features you don't use (remote access, UPnP) to reduce attack surface.\n" +
                    "• Research a device's security reputation before purchasing."
                }
            },

            // ── GENERAL CYBERSECURITY ────────────────────────────────────────────────
            {
                new[] { "cybersecurity", "cyber security", "information security", "infosec", "security tips", "stay safe", "online safety" },
                new List<string>
                {
                    "🛡️ General Cybersecurity Best Practices:\n" +
                    "• Use strong, unique passwords + a password manager.\n" +
                    "• Enable 2FA on all important accounts.\n" +
                    "• Keep all software and devices updated.\n" +
                    "• Be sceptical of unsolicited emails, calls, and messages.\n" +
                    "• Back up your data regularly using the 3-2-1 rule.",

                    "🔐 Cyber Hygiene Checklist:\n" +
                    "• ✅ Password manager installed\n" +
                    "• ✅ 2FA enabled on email & banking\n" +
                    "• ✅ Automatic OS updates turned on\n" +
                    "• ✅ Antivirus software active\n" +
                    "• ✅ Regular data backups in place\n" +
                    "Type any topic above for details!"
                }
            }
        };

        // Track last response index per topic to rotate answers
        private readonly Dictionary<string, int> _lastResponseIndex = new Dictionary<string, int>();

        public string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            string lower = input.ToLower();

            foreach (var entry in _topicResponses)
            {
                if (entry.Key.Any(keyword => lower.Contains(keyword)))
                {
                    string topicKey = entry.Key[0]; // Use first keyword as the key
                    List<string> responses = entry.Value;

                    // Rotate responses so repeated queries get a different answer
                    if (!_lastResponseIndex.ContainsKey(topicKey))
                        _lastResponseIndex[topicKey] = 0;
                    else
                        _lastResponseIndex[topicKey] = (_lastResponseIndex[topicKey] + 1) % responses.Count;

                    return responses[_lastResponseIndex[topicKey]];
                }
            }

            return null; // No match found
        }

        /// <summary>Returns a formatted list of all available topics for the help menu.</summary>
        public string GetTopicList()
        {
            return "📚 Topics I can help you with:\n\n" +
                   "🔐 password / passphrase\n" +
                   "🎣 phishing / smishing / vishing\n" +
                   "🚨 scam / fraud\n" +
                   "🦠 malware / virus / ransomware\n" +
                   "🔏 privacy / data protection / POPIA\n" +
                   "🌐 VPN / proxy\n" +
                   "🔑 2FA / MFA / authenticator / OTP\n" +
                   "🎭 social engineering / pretexting\n" +
                   "🔒 encryption / HTTPS / SSL\n" +
                   "🧱 firewall / network security\n" +
                   "🔄 updates / patching\n" +
                   "📶 public Wi-Fi / hotspot\n" +
                   "🪪 identity theft / account takeover\n" +
                   "🕸️ dark web / deep web\n" +
                   "💾 backup / data backup\n" +
                   "🌍 safe browsing / browser safety\n" +
                   "📱 social media security\n" +
                   "🛑 cyberbullying / harassment\n" +
                   "⚠️ zero-day / vulnerability\n" +
                   "🏠 IoT / smart home security\n\n" +
                   "Just type any keyword to learn more!";
        }
    }
}
