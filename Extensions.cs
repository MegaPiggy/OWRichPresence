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

            //Carson
            ImageKey.carson => "Carson",
            ImageKey.alpinia => "Alpinia",
            ImageKey.dester => "Dester",
            ImageKey.draeo => "Draeo",
            ImageKey.ekbur => "Ekbur",
            ImageKey.glaze => "Glaze",
            ImageKey.graebur => "Graebur",
            ImageKey.loon => "Loon",
            ImageKey.mirtha => "Mirtha",
            ImageKey.platinumhue => "Platinum Hue",
            ImageKey.pyer => "Pyer",
            ImageKey.scarletstorm => "Scarlet Storm",
            ImageKey.urath => "Urath",
            ImageKey.volcus => "Volcus",

            //Astral Codec
            ImageKey.lingeringchime => "Lingering Chime",

            //New Horizons
            ImageKey.newhorizons => "New Horizons",
            ImageKey.defaultplanet => "Custom Planet",
            ImageKey.defaultplanetatmosphere => "Custom Atmospheric Planet",
            ImageKey.defaultstar => "Custom Star",

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
            ImageKey.hinderingskies => "Hindering Skies",
            ImageKey.lava1 => "Lava 1",
            ImageKey.lava2 => "Lava 2",

            //TRAPPIST-1
            ImageKey.trappist1 => "TRAPPIST-1",
            ImageKey.boilingfire => "Boiling Fire",
            ImageKey.cloudyskies => "Cloudy Skies",
            ImageKey.deepwaters => "Deep Waters",
            ImageKey.everlastingsunset => "Everlasting Sunset",
            ImageKey.frigidseas => "Frigid Seas",
            ImageKey.glacialsteam => "Glacial Steam",
            ImageKey.hollowice => "Hollow Ice",

            //RSS
            ImageKey.barnardsstar => "Barnard's Star",
            ImageKey.barnardsstarb => "Barnard's Star B",
            ImageKey.callisto => "Callisto",
            ImageKey.ceres => "Ceres",
            ImageKey.charon => "Charon",
            ImageKey.deimos => "Deimos",
            ImageKey.earth => "Earth",
            ImageKey.europa => "Europa",
            ImageKey.ganymede => "Ganymede",
            ImageKey.halleyscomet => "Halley's Comet",
            ImageKey.hydra => "Hydra",
            ImageKey.io => "Io",
            ImageKey.jupiter => "Jupiter",
            ImageKey.kerberos => "Kerberos",
            ImageKey.luhman16a => "Luhman 16A",
            ImageKey.luhman16b => "Luhman 16B",
            ImageKey.mars => "Mars",
            ImageKey.mercury => "Mercury",
            ImageKey.themoon => "The Moon",
            ImageKey.neptune => "Neptune",
            ImageKey.nix => "Nix",
            ImageKey.perdition => "Perdition",
            ImageKey.phobos => "Phobos",
            ImageKey.pluto => "Pluto",
            ImageKey.proteus => "Proteus",
            ImageKey.proximab => "Proxima B",
            ImageKey.proximac => "Proxima C",
            ImageKey.proximacentauri => "Proxima Centauri",
            ImageKey.puck => "Puck",
            ImageKey.rigilkentaurus => "Rigil Kentaurus",
            ImageKey.saturn => "Saturn",
            ImageKey.sol => "Sol",
            ImageKey.styx => "Styx",
            ImageKey.titan => "Titan",
            ImageKey.titania => "Titania",
            ImageKey.toliman => "Toliman",
            ImageKey.triton => "Triton",
            ImageKey.uranus => "Uranus",
            ImageKey.venus => "Venus",
            ImageKey.vesta => "Vesta",

            //Upsilon Andromedae
            ImageKey.titawin => "Titawin",
            ImageKey.upsilonandromedaeb => "Upsilon Andromedae B",

            //Outsider
            ImageKey.darkbrambleoutsider => "Dark Bramble",
            ImageKey.powerstation => "Bramble Power Station",

            //Hearth's Neighbor
            ImageKey.neighborsun => "Neighbor Sun",
            ImageKey.lonelyhermit => "Lonely Hermit",
            ImageKey.alpinecore => "Alpine Core",
            ImageKey.lakecore => "Lake Core",
            ImageKey.lavacore => "Lava Core",
            ImageKey.derelictship => "Derelict Ship",

            //Tesseract's Secret
            ImageKey.tesseract => "Tesseract",

            _ => string.Empty,
        };

        public static string ConvertToRealName(string id) => id switch
        {
            "DB_D_ENTRY" => "Entry Dimension",
            "DB_D_FEEDGROUND" => "Feed Ground Dimension",
            "DB_D_GOAL" => "Goal Dimension",
            "DB_D_HOPPERTOWN" => "Hopper Town Dimension",
            "DB_D_MININGSITE" => "Mining Site Dimension",
            "DB_D_REDSEED" => "Red Seed Dimension",
            "DB_D_WHITEHOLE" => "White Hole Dimension",
            _ => id,
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
