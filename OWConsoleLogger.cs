using DiscordRPC.Logging;
using OWML.Common;
using OWML.Logging;

namespace OWRichPresence
{
    public class OWConsoleLogger : ILogger
    {
        public MessageType Type { get; set; }

        public LogLevel Level
        {
            get => Type.OWMLToDiscord();
            set => Type = value.DiscordToOWML();
        }

        public OWConsoleLogger(MessageType type)
        {
            this.Type = type;
        }

        public OWConsoleLogger(LogLevel level)
        {
            this.Level = level;
        }

        private static string Format(string message, params object[] args)
        {
            if (args != null && args.Length > 0)
                return string.Format(message, args);
            else
                return message;
        }

        public void Trace(string message, params object[] args) => OWRichPresence.WriteLine(Format(message, args), MessageType.Debug);

        public void Info(string message, params object[] args) => OWRichPresence.WriteLine(Format(message, args), MessageType.Info);

        public void Warning(string message, params object[] args) => OWRichPresence.WriteLine(Format(message, args), MessageType.Warning);

        public void Error(string message, params object[] args) => OWRichPresence.WriteLine(Format(message, args), MessageType.Error);
    }
}
