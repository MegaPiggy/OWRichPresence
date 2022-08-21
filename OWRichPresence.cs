using OWML.Common;
using OWML.ModHelper;
using DiscordRPC;
using DiscordRPC.Unity;
using UnityEngine;

namespace OWRichPresence
{
	public class OWRichPresence : ModBehaviour
	{
		public DiscordRpcClient client;
		public static OWRichPresence Instance { get; private set; }
		public static bool TriggersActive = true;

		public ListStack<RichPresence> _presenceStack = new();
		public RichPresence _shipPresence;
		public RichPresence _giantsDeepPresence;

#if DEBUG
		private static bool debug = true;
#else
		private static bool debug = false;
#endif

		private INewHorizons _newHorizons;
		private bool _newHorizonsExamples;

		public override object GetApi() => new RichPresenceAPI();

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			// Starting here, you'll have access to OWML's mod helper.
			ConsoleWriteLine($"My mod {nameof(OWRichPresence)} is loaded!", MessageType.Success);

			var logger = new OWConsoleLogger(MessageType.Debug);
			client = new DiscordRpcClient("1010346259757944882", -1, logger, false, new UnityNamedPipe(logger));

			client.Initialize();

			_newHorizonsExamples = ModHelper.Interaction.ModExists("xen.NewHorizonsExamples");

			OnSceneLoad(OWScene.TitleScreen);

			_newHorizons = ModHelper.Interaction.TryGetModApi<INewHorizons>("xen.NewHorizons");
			if (_newHorizons != null) _newHorizons.GetStarSystemLoadedEvent().AddListener(OnStarSystemLoaded);

			LoadManager.OnCompleteSceneLoad += (originalScene, loadScene) => OnSceneLoad(loadScene);
		}

		private RichPresenceTrigger CreateTriggerWithNH(string details, ImageKey imageKey) => CreateTrigger(_newHorizons?.GetPlanet(imageKey.KeyToText())?.GetComponentInChildren<Sector>()?.gameObject, details, imageKey);
		private RichPresenceTrigger CreateTriggerWithNH(string planetName, string details, ImageKey imageKey) => CreateTrigger(_newHorizons?.GetPlanet(planetName)?.GetComponentInChildren<Sector>()?.gameObject, details, imageKey);
		private RichPresenceTrigger CreateTriggerWithNH(string planetName, RichPresence richPresence) => CreateTrigger(_newHorizons?.GetPlanet(planetName)?.GetComponentInChildren<Sector>()?.gameObject, richPresence);

		private void OnStarSystemLoaded(string starSystem)
		{
			if (_newHorizonsExamples)
			{
				if (starSystem == "SolarSystem")
				{
					UpdatePresence(_giantsDeepPresence, ImageKey.giantsdeepexamples);
					CreateTriggerWithNH("Observing the Dark Gateway.", ImageKey.darkgateway);
					CreateTriggerWithNH("Observing Devil's Maw.", ImageKey.devilsmaw);
					CreateTriggerWithNH("Exploring the Lava Twins.", ImageKey.lavatwins);
					CreateTriggerWithNH("Lava 1", "Exploring Lava 1.", ImageKey.lavatwin);
					CreateTriggerWithNH("Lava 2", "Exploring Lava 2.", ImageKey.lavatwin);
					CreateTriggerWithNH("Observing the Light Gateway.", ImageKey.lightgateway);
					CreateTriggerWithNH("Exploring Luna Lure.", ImageKey.lunalure);
					CreateTriggerWithNH("Burning up near Night Light.", ImageKey.nightlight);
					CreateTriggerWithNH("Exploring Ringed Jewel.", ImageKey.ringedjewel);
					CreateTriggerWithNH("Exploring Snowball.", ImageKey.snowball);
					CreateTriggerWithNH("Exploring Terra Lure.", ImageKey.terralure);
					CreateTriggerWithNH("Exploring Wetrock.", ImageKey.wetrock);
				}
				else if (starSystem == "xen.NewHorizonsExamples")
				{
					CreateTriggerWithNH("Burning up near Sequestered Luminary.", ImageKey.sequesteredluminary);
					CreateTriggerWithNH("Exploring Daunting Confidant.", ImageKey.dauntingconfidant);
					CreateTriggerWithNH("Exploring Frigid Pygmy.", ImageKey.frigidpygmy);
				}
			}
		}

		private void OnSceneLoad(OWScene loadScene)
		{
			_presenceStack.Clear();
			switch (loadScene)
			{
				case OWScene.TitleScreen:
					_presenceStack.Push(MakePresence("In the title screen.", ImageKey.outerwilds));
					break;
				case OWScene.SolarSystem:
					CreateTrigger("TimberHearth_Body/Sector_TH", "Exploring Timber Hearth.", ImageKey.timberhearth);
					CreateTrigger("Moon_Body/Sector_THM", "Exploring the Attlerock.", ImageKey.attlerock);
					CreateTrigger("BrittleHollow_Body/Sector_BH", "Exploring Brittle Hollow.", ImageKey.brittlehollow);
					CreateTrigger("VolcanicMoon_Body/Sector_VM", "Exploring Hollow's Lantern.", ImageKey.hollowslantern);
					CreateTrigger("Sun_Body/Sector_SUN", "Burning up near the Sun.", ImageKey.sun);
					CreateTrigger("SunStation_Body/Sector_SunStation", "Orbiting the Sun.", ImageKey.sunstation);
					CreateTrigger("TowerTwin_Body/Sector_TowerTwin", "Exploring Ash Twin.", ImageKey.ashtwin);
					CreateTrigger("CaveTwin_Body/Sector_CaveTwin", "Exploring Ember Twin.", ImageKey.embertwin);
					CreateTrigger("QuantumMoon_Body/Sector_QuantumMoon", "Exploring somewhere strange...", ImageKey.quantummoon);
					CreateTrigger("DreamWorld_Body/Sector_DreamWorld", "Exploring somewhere strange...", ImageKey.dreamworld);
					CreateTrigger("RingWorld_Body/Sector_RingWorld", "Exploring somewhere strange...", ImageKey.stranger);
					_giantsDeepPresence = MakePresence("Exploring Giant's Deep.", ImageKey.giantsdeep);
					CreateTrigger("GiantsDeep_Body/Sector_GD", _giantsDeepPresence);
					CreateTrigger("DarkBramble_Body/Sector_DB", "Exploring Dark Bramble.", ImageKey.darkbramble);
					CreateTrigger("DB_AnglerNestDimension_Body/Sector_AnglerNestDimension", "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger("DB_ClusterDimension_Body/Sector_ClusterDimension", "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger("DB_Elsinore_Body/Sector_ElsinoreDimension", "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger("DB_EscapePodDimension_Body/Sector_EscapePodDimension", "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger("DB_ExitOnlyDimension_Body/Sector_ExitOnlyDimension", "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger("DB_HubDimension_Body/Sector_HubDimension", "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger("DB_PioneerDimension_Body/Sector_PioneerDimension", "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger("DB_SmallNest_Body/Sector_SmallNestDimension", "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger("DB_VesselDimension_Body/Sector_VesselDimension", "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger("WhiteHole_Body/Sector_WhiteHole", "Exploring the White Hole.", ImageKey.whitehole);
					CreateTrigger("WhiteholeStation_Body/Sector_WhiteholeStation", "Exploring White Hole Station.", ImageKey.whitehole);
					CreateTrigger("FocalBody/Sector_HGT", "Exploring The Hourglass Twins.", ImageKey.hourglasstwins);
					CreateTrigger("Comet_Body/Sector_CO", "Exploring Interloper.", ImageKey.interloper);
					CreateTrigger("HearthianMapSatellite_Body/Sector_HearthianMapSatellite", "Checking on the Map Satellite.", ImageKey.outerwilds);
					CreateTrigger("OrbitalProbeCannon_Body/Sector_OrbitalProbeCannon", "Orbiting Giant's Deep.", ImageKey.orbitalprobecannon);
					CreateTrigger("GabbroShip_Body/Sector_GabbroShip", "Checking on Gabbro's ship.", ImageKey.ship);
					CreateTrigger("StatueIsland_Body/Sector_StatueIsland", "Exploring Statue Island.", ImageKey.giantsdeep);
					CreateTrigger("GabbroIsland_Body/Sector_GabbroIsland", "Exploring Gabbro's Island.", ImageKey.giantsdeep);
					CreateTrigger("ConstructionYardIsland_Body/Sector_ConstructionYard", "Exploring Construction Yard.", ImageKey.giantsdeep);
					CreateTrigger("BrambleIsland_Body/Sector_BrambleIsland", "Exploring Bramble Island.", ImageKey.giantsdeep);
					CreateTrigger("QuantumIsland_Body/Sector_QuantumIsland", "Exploring somewhere strange...", ImageKey.giantsdeep);
					CreateTrigger("CannonBarrel_Body/Sector_CannonDebrisMid", "Orbiting Giant's Deep", ImageKey.orbitalprobecannon);
					CreateTrigger("CannonMuzzle_Body/Sector_CannonDebrisTip", "Orbiting Giant's Deep", ImageKey.orbitalprobecannon);
					CreateTrigger("Satellite_Body", "Checking on \"Sky Shutter\" Satellite.", ImageKey.skyshutter);
					CreateTrigger("BackerSatellite_Body/Sector_BackerSatellite", "Checking on the Backer Satellite.", ImageKey.outerwilds);
					_shipPresence = MakePresence("Inside the ship.", ImageKey.ship);
					CreateTrigger("Ship_Body/ShipSector", _shipPresence);
					_presenceStack.Push(MakePresence("Exploring the solar system.", ImageKey.sun));
					break;
				case OWScene.EyeOfTheUniverse:
					_presenceStack.Push(MakePresence("Somewhere...", ImageKey.eyeoftheuniverse));
					break;
				case OWScene.Credits_Fast:
					_presenceStack.Push(MakePresence("Watching the credits.", ImageKey.outerwilds));
					break;
				case OWScene.Credits_Final:
					_presenceStack.Push(MakePresence("Beat the game.", ImageKey.outerwilds));
					break;
				case OWScene.PostCreditsScene:
					_presenceStack.Push(MakePresence("14.3 billion years later...", ImageKey.outerwilds));
					break;
				case OWScene.None:
				case OWScene.Undefined:
				default:
					_presenceStack.Push(MakePresence("Unknown", ImageKey.outerwilds));
					break;
			}
			client.SetPresence(_presenceStack.Peek());
		}

		private void Update() => client.Invoke();

		private void OnApplicationQuit() => client.Deinitialize();

		public void ConsoleWriteLine(string message, MessageType type)
		{
			if (debug)
			{
				ModHelper.Console.WriteLine(message, type);
			}
		}

		public static void WriteLine(string message, MessageType type) => Instance.ConsoleWriteLine(message, type);

		public static RichPresenceTrigger CreateTrigger(string parentPath, string details, ImageKey imageKey) => CreateTrigger(SearchUtilities.Find(parentPath), details, imageKey);
		public static RichPresenceTrigger CreateTrigger(string parentPath, RichPresence richPresence) => CreateTrigger(SearchUtilities.Find(parentPath), richPresence);

		public static RichPresenceTrigger CreateTrigger(GameObject parent, string details, ImageKey imageKey) => CreateTrigger(parent, MakePresence(details, imageKey));

		public static RichPresenceTrigger CreateTrigger(GameObject parent, RichPresence richPresence)
		{
			var rpo = new GameObject("RichPresenceTrigger");
			rpo.transform.SetParent(parent.transform, false);
			rpo.SetActive(false);
			var rpt = rpo.AddComponent<RichPresenceTrigger>();
			rpt.presence = richPresence;
			rpo.SetActive(true);
			return rpt;
		}

		public static RichPresence MakePresence(string details, ImageKey imageKey) => new()
		{
			Details = details,
			Assets = new Assets
			{
				LargeImageKey = imageKey.ToString(),
				LargeImageText = imageKey.KeyToText()
			}
		};

		public static void UpdatePresence(RichPresence presence, string details, ImageKey imageKey)
		{
			UpdatePresence(presence, details);
			UpdatePresence(presence, imageKey);
		}

		public static void UpdatePresence(RichPresence presence, string details)
		{
			presence.Details = details;
		}

		public static void UpdatePresence(RichPresence presence, ImageKey imageKey)
		{
			presence.Assets = new Assets
			{
				LargeImageKey = imageKey.ToString(),
				LargeImageText = imageKey.KeyToText()
			};
		}

		public static void SetPresence(string details, ImageKey imageKey) => Instance.client.SetPresence(MakePresence(details, imageKey));
		public static void SetPresence(RichPresence richPresence) => Instance.client.SetPresence(richPresence);
	}

	public enum ImageKey
	{
		// Vanilla
		ashtwin,
		attlerock,
		brittlehollow,
		darkbramble,
		dreamworld,
		embertwin,
		eyeoftheuniverse,
		giantsdeep,
		hollowslantern,
		hourglasstwins,
		interloper,
		orbitalprobecannon,
		outerwilds,
		quantummoon,
		ship,
		skyshutter,
		stranger,
		sun,
		sunstation,
		timberhearth,
		whitehole,

		//New Horizons Examples
		darkgateway = 100,
		dauntingconfidant,
		devilsmaw,
		frigidpygmy,
		giantsdeepexamples,
		lavatwin,
		lavatwins,
		lightgateway,
		lunalure,
		nightlight,
		ringedjewel,
		sequesteredluminary,
		snowball,
		terralure,
		wetrock,
	}
}