using DiscordRPC.Logging;
using OWML.Common;

namespace OWRichPresence
{
    public static class Extensions
    {
		public static string KeyToText(this ImageKey imageKey) => imageKey switch
		{
			// Vanilla
			ImageKey.ashtwin => "Ash Twin",
			ImageKey.attlerock => "Attlerock",
			ImageKey.brittlehollow => "Brittle Hollow",
			ImageKey.darkbramble => "Dark Bramble",
			ImageKey.dreamworld => "Dreamworld",
			ImageKey.embertwin => "Ember Twin",
			ImageKey.eyeoftheuniverse => "Eye of the Universe",
			ImageKey.giantsdeep => "Giant's Deep",
			ImageKey.hollowslantern => "Hollow's Lantern",
			ImageKey.interloper => "Interloper",
			ImageKey.orbitalprobecannon => "Orbital Probe Cannon",
			ImageKey.outerwilds => "Outer Wilds Ventures",
			ImageKey.quantummoon => "Quantum Moon",
			ImageKey.ship => "Ship",
			ImageKey.skyshutter => "Sky Shutter Satellite",
			ImageKey.stranger => "Stranger",
			ImageKey.sun => "Sun",
			ImageKey.sunstation => "Sun Station",
			ImageKey.timberhearth => "Timber Hearth",
			ImageKey.whitehole => "White Hole",

			//New Horizons Examples
			ImageKey.darkgateway => "Dark Gateway",
			ImageKey.dauntingconfidant => "Daunting Confidant",
			ImageKey.devilsmaw => "Devil's Maw",
			ImageKey.frigidpygmy => "Frigid Pygmy",
			ImageKey.giantsdeepexamples => "Giant's Deep",
			ImageKey.lavatwin => "Lava Twin",
			ImageKey.lavatwins => "Lava Twins",
			ImageKey.lightgateway => "Light Gateway",
			ImageKey.lunalure => "Luna Lure",
			ImageKey.nightlight => "Night Light",
			ImageKey.ringedjewel => "Ringed Jewel",
			ImageKey.sequesteredluminary => "Sequestered Luminary",
			ImageKey.snowball => "Snowball",
			ImageKey.terralure => "Terra Lure",
			ImageKey.wetrock => "Wetrock",

			//TRAPPIST-1
			ImageKey.trappist1 => "TRAPPIST-1",
			ImageKey.boilingfire => "Boiling Fire",
			ImageKey.cloudyskies => "Cloudy Skies",
			ImageKey.deepwaters => "Deep Waters",
			ImageKey.everlastingsunset => "Everlasting Sunset",
			ImageKey.frigidseas => "Frigid Seas",
			ImageKey.glacialsteam => "Glacial Steam",
			ImageKey.hollowice => "Hollow Ice",


			_ => string.Empty,
		};

		public static MessageType DiscordToOWML(this LogLevel level) => level switch
		{
			LogLevel.Trace => MessageType.Debug,
			LogLevel.Info => MessageType.Info,
			LogLevel.Warning => MessageType.Warning,
			LogLevel.Error => MessageType.Error,
			_ => MessageType.Message,
		};

		public static LogLevel OWMLToDiscord(this MessageType type) => type switch
		{
			MessageType.Debug => LogLevel.Trace,
			MessageType.Info => LogLevel.Info,
			MessageType.Warning => LogLevel.Warning,
			MessageType.Error => LogLevel.Error,
			_ => LogLevel.None,
		};
	}
}
