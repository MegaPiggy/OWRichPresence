using DiscordRPC.Logging;
using OWML.Common;

namespace OWRichPresence
{
    public static class Extensions
    {
        public static string KeyToText(this ImageKey imageKey)
        {
            switch (imageKey)
            {
                case ImageKey.ashtwin:
                    return "Ash Twin";
                case ImageKey.attlerock:
                    return "Attlerock";
                case ImageKey.brittlehollow:
                    return "Brittle Hollow";
                case ImageKey.darkbramble:
                    return "Dark Bramble";
                case ImageKey.dreamworld:
                    return "Dreamworld";
                case ImageKey.embertwin:
                    return "Ember Twin";
                case ImageKey.eyeoftheuniverse:
                    return "Eye of the Universe";
                case ImageKey.giantsdeep:
                    return "Giant's Deep";
                case ImageKey.hollowslantern:
                    return "Hollow's Lantern";
                case ImageKey.interloper:
                    return "Interloper";
                case ImageKey.orbitalprobecannon:
                    return "Orbital Probe Cannon";
                case ImageKey.outerwilds:
                    return "Outer Wilds Ventures";
                case ImageKey.quantummoon:
                    return "Quantum Moon";
                case ImageKey.ship:
                    return "Ship";
                case ImageKey.skyshutter:
                    return "Sky Shutter Satellite";
                case ImageKey.stranger:
                    return "Stranger";
                case ImageKey.sun:
                    return "Sun";
                case ImageKey.sunstation:
                    return "Sun Station";
                case ImageKey.timberhearth:
                    return "Timber Hearth";
                case ImageKey.whitehole:
                    return "White Hole";
                default:
                    return string.Empty;
            }
        }

        public static MessageType DiscordToOWML(this LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace:
                    return MessageType.Debug;
                case LogLevel.Info:
                    return MessageType.Info;
                case LogLevel.Warning:
                    return MessageType.Warning;
                case LogLevel.Error:
                    return MessageType.Error;
                case LogLevel.None:
                default:
                    return MessageType.Message;
            }
        }

        public static LogLevel OWMLToDiscord(this MessageType type)
        {
            switch (type)
            {
                case MessageType.Debug:
                    return LogLevel.Trace;
                case MessageType.Info:
                    return LogLevel.Info;
                case MessageType.Warning:
                    return LogLevel.Warning;
                case MessageType.Error:
                    return LogLevel.Error;
                case MessageType.Message:
                default:
                    return LogLevel.None;
            }
        }
    }
}
