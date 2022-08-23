using OWML.Common;
using OWML.ModHelper;
using DiscordRPC;
using DiscordRPC.Unity;
using UnityEngine;
using OWRichPresence.API;

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


		public const string richPresenceTrigger = "RichPresenceTrigger";

		public const string richPresenceTriggerVolume = "RichPresenceTriggerVolume";

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

			_newHorizons = ModHelper.Interaction.TryGetModApi<INewHorizons>("xen.NewHorizons");
			_newHorizonsExamples = ModHelper.Interaction.ModExists("xen.NewHorizonsExamples");

			OnSceneLoad(OWScene.TitleScreen);

			LoadManager.OnCompleteSceneLoad += (originalScene, loadScene) => OnSceneLoad(loadScene);
		}

		private RichPresenceTrigger CreateTriggerWithNH(string details, ImageKey imageKey) => CreateTrigger(_newHorizons?.GetPlanet(imageKey.KeyToText())?.GetComponentInChildren<Sector>()?.gameObject, details, imageKey);
		private RichPresenceTrigger CreateTriggerWithNH(string planetName, string details, ImageKey imageKey) => CreateTrigger(_newHorizons?.GetPlanet(planetName)?.GetComponentInChildren<Sector>()?.gameObject, details, imageKey);
		private RichPresenceTrigger CreateTriggerWithNH(string planetName, RichPresence richPresence) => CreateTrigger(_newHorizons?.GetPlanet(planetName)?.GetComponentInChildren<Sector>()?.gameObject, richPresence);

		private void OnSceneLoad(OWScene loadScene)
		{
			switch (loadScene)
			{
				case OWScene.TitleScreen:
					SetRootPresence("In the title screen.", ImageKey.outerwilds);
					break;
				case OWScene.SolarSystem:
					var giantdeepimage = _newHorizonsExamples ? ImageKey.giantsdeepexamples : ImageKey.giantsdeep;
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
					CreateTrigger("GiantsDeep_Body/Sector_GD", "Exploring Giant's Deep.", giantdeepimage);
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
					CreateTrigger("StatueIsland_Body/Sector_StatueIsland", "Exploring Statue Island.", giantdeepimage);
					CreateTrigger("GabbroIsland_Body/Sector_GabbroIsland", "Exploring Gabbro's Island.", giantdeepimage);
					CreateTrigger("ConstructionYardIsland_Body/Sector_ConstructionYard", "Exploring Construction Yard.", giantdeepimage);
					CreateTrigger("BrambleIsland_Body/Sector_BrambleIsland", "Exploring Bramble Island.", giantdeepimage);
					CreateTrigger("QuantumIsland_Body/Sector_QuantumIsland", "Exploring somewhere strange...", giantdeepimage);
					CreateTrigger("CannonBarrel_Body/Sector_CannonDebrisMid", "Orbiting Giant's Deep", ImageKey.orbitalprobecannon);
					CreateTrigger("CannonMuzzle_Body/Sector_CannonDebrisTip", "Orbiting Giant's Deep", ImageKey.orbitalprobecannon);
					CreateTrigger("Satellite_Body", "Checking on \"Sky Shutter\" Satellite.", ImageKey.skyshutter);
					CreateTrigger("BackerSatellite_Body/Sector_BackerSatellite", "Checking on the Backer Satellite.", ImageKey.outerwilds);
					_shipPresence = MakePresence("Inside the ship.", ImageKey.ship);
					CreateTrigger("Ship_Body/ShipSector", _shipPresence);
					SetRootPresence("Exploring the solar system.", ImageKey.sun);
					break;
				case OWScene.EyeOfTheUniverse:
					SetRootPresence("Somewhere...", ImageKey.eyeoftheuniverse);
					break;
				case OWScene.Credits_Fast:
					SetRootPresence("Watching the credits.", ImageKey.outerwilds);
					break;
				case OWScene.Credits_Final:
					SetRootPresence("Beat the game.", ImageKey.outerwilds);
					break;
				case OWScene.PostCreditsScene:
					SetRootPresence("14.3 billion years later...", ImageKey.outerwilds);
					break;
				case OWScene.None:
				case OWScene.Undefined:
				default:
					SetRootPresence("Unknown", ImageKey.outerwilds);
					break;
			}
			client.SetPresence(_presenceStack.Peek());
		}

		public void SetRootPresence(string message, ImageKey imageKey)
		{
			_presenceStack.Clear();
			_presenceStack.Push(MakePresence(message, imageKey));
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

		public static void Push(RichPresence presence)
		{
			if (presence == null) return;

			Instance._presenceStack.Push(presence);

			if (PlayerState.IsInsideShip() && presence != Instance._shipPresence)
			{
				Instance._presenceStack.Remove(Instance._shipPresence);
				Instance._presenceStack.Push(Instance._shipPresence);
			}

			if (TriggersActive)
			{
				SetPresence(Instance._presenceStack.Peek());
			}
		}

		public static void Remove(RichPresence presence)
		{
			if (presence == null) return;

			Instance._presenceStack.Remove(presence);

			if (PlayerState.IsInsideShip() && presence != Instance._shipPresence)
			{
				Instance._presenceStack.Remove(Instance._shipPresence);
				Instance._presenceStack.Push(Instance._shipPresence);
			}

			if (TriggersActive)
			{
				SetPresence(Instance._presenceStack.Peek());
			}
		}

		public static RichPresenceTriggerVolume CreateTriggerVolume(string triggerVolumePath, string details, ImageKey imageKey) => CreateTriggerVolume(SearchUtilities.Find(triggerVolumePath)?.GetComponent<OWTriggerVolume>(), details, imageKey);
		public static RichPresenceTriggerVolume CreateTriggerVolume(string triggerVolumePath, RichPresence richPresence) => CreateTriggerVolume(SearchUtilities.Find(triggerVolumePath)?.GetComponent<OWTriggerVolume>(), richPresence);

		public static RichPresenceTriggerVolume CreateTriggerVolume(OWTriggerVolume owTriggerVolume, string details, ImageKey imageKey) => CreateTriggerVolume(owTriggerVolume, MakePresence(details, imageKey));
		public static RichPresenceTriggerVolume CreateTriggerVolume(OWTriggerVolume owTriggerVolume, RichPresence richPresence)
		{
			if (owTriggerVolume == null) return null;
			var rptv = owTriggerVolume.gameObject.GetAddComponent<RichPresenceTriggerVolume>();
			rptv.triggerVolume = owTriggerVolume;
			rptv.presence = richPresence;
			return rptv;
		}

		public static RichPresenceTrigger CreateTrigger(string parentPath, string details, ImageKey imageKey) => CreateTrigger(SearchUtilities.Find(parentPath), details, imageKey);
		public static RichPresenceTrigger CreateTrigger(string parentPath, RichPresence richPresence) => CreateTrigger(SearchUtilities.Find(parentPath), richPresence);

		public static RichPresenceTrigger CreateTrigger(GameObject parent, string details, ImageKey imageKey) => CreateTrigger(parent, MakePresence(details, imageKey));

		public static RichPresenceTrigger CreateTrigger(GameObject parent, RichPresence richPresence)
		{
			if (parent == null) return null;
			var rpo = parent.FindChild(richPresenceTrigger);
			if (rpo != null)
			{
				var rpt = rpo.GetAddComponent<RichPresenceTrigger>();
				rpt.presence = richPresence;
				return rpt;
			}
			else
			{
				rpo = new GameObject(richPresenceTrigger);
				rpo.transform.SetParent(parent.transform, false);
				rpo.SetActive(false);
				var rpt = rpo.AddComponent<RichPresenceTrigger>();
				rpt.presence = richPresence;
				rpo.SetActive(true);
				return rpt;
			}
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
}