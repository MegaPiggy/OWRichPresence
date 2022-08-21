using OWML.Common;
using OWML.ModHelper;
using DiscordRPC;
using DiscordRPC.Unity;
using UnityEngine;
using System.Collections.Generic;

namespace OWRichPresence
{
	public class OWRichPresence : ModBehaviour
	{
		public DiscordRpcClient client;
		public static OWRichPresence Instance { get; private set; }

		public ListStack<RichPresence> _presenceStack = new();
		public RichPresence _shipPresence;

#if DEBUG
		private static bool debug = true;
#else
		private static bool debug = false;
#endif

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

			OnSceneLoad(OWScene.TitleScreen);

			LoadManager.OnCompleteSceneLoad += (originalScene, loadScene) => OnSceneLoad(loadScene);
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
					CreateTrigger(SearchUtilities.Find("TimberHearth_Body/Sector_TH"), "Exploring Timber Hearth.", ImageKey.timberhearth);
					CreateTrigger(SearchUtilities.Find("Moon_Body/Sector_THM"), "Exploring the Attlerock.", ImageKey.attlerock);
					CreateTrigger(SearchUtilities.Find("BrittleHollow_Body/Sector_BH"), "Exploring Brittle Hollow.", ImageKey.brittlehollow);
					CreateTrigger(SearchUtilities.Find("VolcanicMoon_Body/Sector_VM"), "Exploring Hollow's Lantern.", ImageKey.hollowslantern);
					CreateTrigger(SearchUtilities.Find("Sun_Body/Sector_SUN"), "Burning up near the Sun.", ImageKey.sun);
					//CreateTrigger(SearchUtilities.Find("SunStation_Body/Sector_SunStation"), "Exploring the Sun Station.", ImageKey.sunstation);
					CreateTrigger(SearchUtilities.Find("TowerTwin_Body/Sector_TowerTwin"), "Exploring Ash Twin.", ImageKey.ashtwin);
					CreateTrigger(SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin"), "Exploring Ember Twin.", ImageKey.embertwin);
					CreateTrigger(SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon"), "Exploring somewhere strange...", ImageKey.quantummoon);
					CreateTrigger(SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld"), "Exploring somewhere strange...", ImageKey.dreamworld);
					CreateTrigger(SearchUtilities.Find("RingWorld_Body/Sector_RingWorld"), "Exploring somewhere strange...", ImageKey.stranger);
					CreateTrigger(SearchUtilities.Find("GiantsDeep_Body/Sector_GD"), "Exploring Giant's Deep.", ImageKey.giantsdeep);
					CreateTrigger(SearchUtilities.Find("DarkBramble_Body/Sector_DB"), "Exploring Dark Bramble.", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_AnglerNestDimension_Body/Sector_AnglerNestDimension"), "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_ClusterDimension_Body/Sector_ClusterDimension"), "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_Elsinore_Body/Sector_ElsinoreDimension"), "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_EscapePodDimension_Body/Sector_EscapePodDimension"), "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_ExitOnlyDimension_Body/Sector_ExitOnlyDimension"), "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_HubDimension_Body/Sector_HubDimension"), "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_PioneerDimension_Body/Sector_PioneerDimension"), "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_SmallNest_Body/Sector_SmallNestDimension"), "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_VesselDimension_Body/Sector_VesselDimension"), "Somewhere in Dark Bramble...", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("WhiteHole_Body/Sector_WhiteHole"), "Exploring the White Hole.", ImageKey.whitehole);
					CreateTrigger(SearchUtilities.Find("WhiteholeStation_Body/Sector_WhiteholeStation"), "Exploring White Hole Station.", ImageKey.whitehole);
					CreateTrigger(SearchUtilities.Find("FocalBody/Sector_HGT"), "Exploring The Hourglass Twins.", ImageKey.hourglasstwins);
					CreateTrigger(SearchUtilities.Find("Comet_Body/Sector_CO"), "Exploring Interloper.", ImageKey.interloper);
					CreateTrigger(SearchUtilities.Find("HearthianMapSatellite_Body/Sector_HearthianMapSatellite"), "Checking on the Map Satellite.", ImageKey.outerwilds);
					//CreateTrigger(SearchUtilities.Find("OrbitalProbeCannon_Body/Sector_OrbitalProbeCannon"), "Exploring the Orbital Probe Cannon.", ImageKey.orbitalprobecannon);
					CreateTrigger(SearchUtilities.Find("GabbroShip_Body/Sector_GabbroShip"), "Checking on Gabbro's ship.", ImageKey.ship);
					CreateTrigger(SearchUtilities.Find("StatueIsland_Body/Sector_StatueIsland"), "Exploring Statue Island.", ImageKey.giantsdeep);
					CreateTrigger(SearchUtilities.Find("GabbroIsland_Body/Sector_GabbroIsland"), "Exploring Gabbro's Island.", ImageKey.giantsdeep);
					CreateTrigger(SearchUtilities.Find("ConstructionYardIsland_Body/Sector_ConstructionYard"), "Exploring Construction Yard.", ImageKey.giantsdeep);
					CreateTrigger(SearchUtilities.Find("BrambleIsland_Body/Sector_BrambleIsland"), "Exploring Bramble Island.", ImageKey.giantsdeep);
					//CreateTrigger(SearchUtilities.Find("QuantumIsland_Body/Sector_QuantumIsland"), "Exploring Quantum Tower", ImageKey.giantsdeep);
					//CreateTrigger(SearchUtilities.Find("CannonBarrel_Body/Sector_CannonDebrisMid"), "Exploring Orbital Probe Cannon Barrel", ImageKey.orbitalprobecannon);
					//CreateTrigger(SearchUtilities.Find("CannonMuzzle_Body/Sector_CannonDebrisTip"), "Exploring Orbital Probe Cannon Muzzle", ImageKey.orbitalprobecannon);
					CreateTrigger(SearchUtilities.Find("Satellite_Body"), "Checking on \"Sky Shutter\" Satellite.", ImageKey.skyshutter);
					CreateTrigger(SearchUtilities.Find("BackerSatellite_Body/Sector_BackerSatellite"), "Checking on the Backer Satellite.", ImageKey.outerwilds);
					_shipPresence = MakePresence("Inside the ship.", ImageKey.ship);
					CreateTrigger(SearchUtilities.Find("Ship_Body/ShipSector"), _shipPresence);
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

		public static RichPresenceTrigger CreateTrigger(GameObject parent, string details, ImageKey imageKey)
		{
			var rpo = new GameObject("RichPresenceTrigger");
			rpo.transform.SetParent(parent.transform, false);
			rpo.SetActive(false);
			var rpt = rpo.AddComponent<RichPresenceTrigger>();
			rpt.presence = MakePresence(details, imageKey);
			rpo.SetActive(true);
			return rpt;
		}

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

		public static void SetPresence(string details, ImageKey imageKey) => Instance.client.SetPresence(MakePresence(details, imageKey));
		public static void SetPresence(RichPresence richPresence) => Instance.client.SetPresence(richPresence);
	}

	public enum ImageKey
	{
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
		whitehole
	}
}
