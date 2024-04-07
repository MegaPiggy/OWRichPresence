using OWML.Common;
using OWML.ModHelper;
using DiscordRPC;
using DiscordRPC.Unity;
using UnityEngine;
using OWRichPresence.API;
using System;
using System.Collections.Generic;

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

		public readonly List<Action<string, string, string>> handlers = new();

#if DEBUG
		private static bool debug = true;
#else
		private static bool debug = false;
#endif

		private INewHorizons _newHorizons;
		private bool _newHorizonsExamples;
		private bool _outsider;


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
			_outsider = ModHelper.Interaction.ModExists("SBtT.TheOutsider");

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
					var darkbrambleimage = _outsider ? ImageKey.darkbrambleoutsider : ImageKey.darkbramble;
					var giantdeepimage = _newHorizonsExamples ? ImageKey.giantsdeepexamples : ImageKey.giantsdeep;
					CreateTrigger("TimberHearth_Body/Sector_TH", "Exploring Timber Hearth.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village", new Vector3(52.4282f, 43.9491f, 17.3538f), 25, "On the launch pad.", ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_ZeroGCave", "Exploring the Zero-G Cave.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_ZeroGCave/Characters_ZeroGCave/Villager_HEA_Tuff/WatchVolume", "Visiting Tuff.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_ZeroGCave/Characters_ZeroGCave/Villager_HEA_Tuff", 2, "Visiting Tuff.", ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_Village", "Exploring the village.", ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_Village/Sector_VillageCemetery", "Visiting the Cemetery.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_VillageCemetery/Characters_VillageCemetery/Villager_HEA_Tephra_PostObservatory/WatchVolume", "Visiting Tephra.", ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage", "Exploring the lower village.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Villager_HEA_Mica/WatchVolume", "Visiting Mica.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Villager_HEA_Gneiss/WatchVolume", "Visiting Gneiss.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Villager_HEA_Spinel/WatchVolume", "Visiting Spinel.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Villager_HEA_Spinel", 2, "Visiting Spinel.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Villager_HEA_Rutile/WatchVolume", "Visiting Rutile.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Villager_HEA_Marl/WatchVolume", "Visiting Marl.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Villager_HEA_Porphy/WatchVolume", "Visiting Porphy.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Kids_PreGame/Villager_HEA_Tephra/WatchVolume", "Visiting Tephra.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Kids_PreGame/Villager_HEA_Galena/WatchVolume", "Visiting Galena.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Kids_Hidden/Villager_HEA_Tephra (1)/WatchVolume", "Found Tephra.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Kids_Hidden/Villager_HEA_Galena (1)/WatchVolume", "Found Galena.", ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_Village/Sector_StartingCamp", "Sleeping under the stars.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_StartingCamp/Characters_StartingCamp/Villager_HEA_Slate/WatchVolume", "Visiting Slate.", ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_Village/Sector_UpperVillage", "Exploring the upper village.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_UpperVillage/Characters_UpperVillage/Villager_HEA_Gossan/WatchVolume", "Visiting Gossan.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_UpperVillage/Characters_UpperVillage/Villager_HEA_Tektite/WatchVolume", "Visiting Tektite.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_UpperVillage/Characters_UpperVillage/Villager_HEA_Tektite", 2, "Visiting Tektite.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_UpperVillage/Characters_UpperVillage/Villager_HEA_Arkose_GhostMatter/WatchVolume", "Visiting Arkose.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_UpperVillage/Characters_UpperVillage/Villager_HEA_Arkose_GhostMatter", 2, "Visiting Arkose.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_UpperVillage/Characters_UpperVillage/Villager_HEA_Moraine", 2, "Visiting Moraine.", ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory", "Visiting the Observatory.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory/Characters_Observatory/Character_HEA_Hal_Museum/WatchVolume", "Visiting Hal.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory/Characters_Observatory/Character_HEA_Hal_Museum", 2, "Visiting Hal.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Characters_Village/Villager_HEA_Hal_Outside/WatchVolume", "Visiting Hal.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Characters_Village/Villager_HEA_Hal_Outside", 2, "Visiting Hal.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory/Characters_Observatory/Villager_HEA_Hornfels/WatchVolume", "Visiting Hornfels.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory/Characters_Observatory/Villager_HEA_Hornfels", 2, "Visiting Hornfels.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory/Characters_Observatory/Villager_HEA_Hornfels (1)/WatchVolume", "Visiting Hornfels.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory/Characters_Observatory/Villager_HEA_Hornfels (1)", 2, "Visiting Hornfels.", ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_WaterWays", "Inside Timber Hearth.", ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_NomaiCrater", "Exploring a crater.", ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_ImpactCrater", "Exploring a crater.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_ImpactCrater/Characters_ImpactCrater/Villager_HEA_Tektite_2/WatchVolume", "Visiting Tektite.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_ImpactCrater/Characters_ImpactCrater/Villager_HEA_Tektite_2", 2, "Visiting Tektite.", ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_NomaiMines", "Inside Timber Hearth.", ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_NomaiMines/Sector_NomaiMinesInterior", "Inside Timber Hearth.", ImageKey.timberhearth);
					CreateTrigger("Moon_Body/Sector_THM", "Exploring the Attlerock.", ImageKey.attlerock);
					CreateTriggerVolume("Moon_Body/Sector_THM/Volumes_THM/PineGroveVolume", "Visiting Esker.", ImageKey.attlerock);
					CreateTrigger("BrittleHollow_Body/Sector_BH", "Exploring Brittle Hollow.", ImageKey.brittlehollow);
					CreateTrigger("BrittleHollow_Body/Sector_BH/Sector_Crossroads", "Exploring the Crossroads.", ImageKey.brittlehollow);
					CreateTriggerVolume("BrittleHollow_Body/Sector_BH/Sector_Crossroads/Characters_Crossroads/Traveller_HEA_Riebeck", new Vector3(-0.134f, 1.651f, 0.279f), 5, "Visiting Riebeck.", ImageKey.brittlehollow);
					CreateTrigger("VolcanicMoon_Body/Sector_VM", "Exploring Hollow's Lantern.", ImageKey.hollowslantern);
					CreateTrigger("Sun_Body/Sector_SUN", "Burning up near the Sun.", ImageKey.sun);
					CreateTrigger("SunStation_Body/Sector_SunStation", "Orbiting the Sun.", ImageKey.sunstation);
					CreateTrigger("TowerTwin_Body/Sector_TowerTwin", "Exploring Ash Twin.", ImageKey.ashtwin);
					CreateTrigger("CaveTwin_Body/Sector_CaveTwin", "Exploring Ember Twin.", ImageKey.embertwin);
					CreateTriggerVolume("CaveTwin_Body/Sector_CaveTwin/Sector_NorthHemisphere/Sector_NorthSurface/Sector_Lakebed/Interactables_Lakebed/Traveller_HEA_Chert", Vector3.zero, 5, "Visiting Chert.", ImageKey.embertwin);
					CreateTrigger("QuantumMoon_Body/Sector_QuantumMoon", "Exploring somewhere strange...", ImageKey.quantummoon);
					CreateTrigger("DreamWorld_Body/Sector_DreamWorld", "Exploring somewhere strange...", ImageKey.dreamworld);
					CreateTrigger("RingWorld_Body/Sector_RingWorld", "Exploring somewhere strange...", ImageKey.stranger);
					CreateTrigger("RingWorld_Body/Sector_RingInterior", "Exploring somewhere strange...", ImageKey.stranger);
					CreateTrigger("GiantsDeep_Body/Sector_GD", "Exploring Giant's Deep.", giantdeepimage);
					CreateTrigger("DarkBramble_Body/Sector_DB", "Exploring Dark Bramble.", darkbrambleimage);
					CreateTrigger("DB_AnglerNestDimension_Body/Sector_AnglerNestDimension", "Somewhere in Dark Bramble...", darkbrambleimage);
					CreateTrigger("DB_ClusterDimension_Body/Sector_ClusterDimension", "Somewhere in Dark Bramble...", darkbrambleimage);
					CreateTrigger("DB_Elsinore_Body/Sector_ElsinoreDimension", "Somewhere in Dark Bramble...", darkbrambleimage);
					CreateTrigger("DB_EscapePodDimension_Body/Sector_EscapePodDimension", "Somewhere in Dark Bramble...", darkbrambleimage);
					CreateTrigger("DB_ExitOnlyDimension_Body/Sector_ExitOnlyDimension", "Somewhere in Dark Bramble...", darkbrambleimage);
					CreateTrigger("DB_HubDimension_Body/Sector_HubDimension", "Somewhere in Dark Bramble...", darkbrambleimage);
					CreateTrigger("DB_PioneerDimension_Body/Sector_PioneerDimension", "Somewhere in Dark Bramble...", darkbrambleimage);
					CreateTrigger("DB_SmallNest_Body/Sector_SmallNestDimension", "Somewhere in Dark Bramble...", darkbrambleimage);
					CreateTrigger("DB_VesselDimension_Body/Sector_VesselDimension", "Somewhere in Dark Bramble...", darkbrambleimage);
					CreateTrigger("WhiteHole_Body/Sector_WhiteHole", "Exploring the White Hole.", ImageKey.whitehole);
					CreateTrigger("WhiteholeStation_Body/Sector_WhiteholeStation", "Exploring White Hole Station.", ImageKey.whitehole);
					CreateTrigger("FocalBody/Sector_HGT", "Exploring The Hourglass Twins.", ImageKey.hourglasstwins);
					CreateTrigger("Comet_Body/Sector_CO", "Exploring Interloper.", ImageKey.interloper);
					CreateTrigger("HearthianMapSatellite_Body/Sector_HearthianMapSatellite", "Checking on the Map Satellite.", ImageKey.outerwilds);
					CreateTrigger("OrbitalProbeCannon_Body/Sector_OrbitalProbeCannon", "Orbiting Giant's Deep.", ImageKey.orbitalprobecannon);
					CreateTrigger("GabbroShip_Body/Sector_GabbroShip", "Checking on Gabbro's ship.", ImageKey.ship);
					CreateTrigger("StatueIsland_Body/Sector_StatueIsland", "Exploring Statue Island.", giantdeepimage);
					CreateTrigger("GabbroIsland_Body/Sector_GabbroIsland", "Exploring Gabbro's Island.", giantdeepimage);
					CreateTriggerVolume("GabbroIsland_Body/Sector_GabbroIsland/Interactables_GabbroIsland/Traveller_HEA_Gabbro", new Vector3(-0.09f, 1.21f, 0), 5, "Visiting Gabbro.", giantdeepimage);
					CreateTrigger("ConstructionYardIsland_Body/Sector_ConstructionYard", "Exploring Construction Yard.", giantdeepimage);
					CreateTrigger("BrambleIsland_Body/Sector_BrambleIsland", "Exploring Bramble Island.", giantdeepimage);
					CreateTrigger("QuantumIsland_Body/Sector_QuantumIsland", "Exploring somewhere strange...", giantdeepimage);
					CreateTrigger("CannonBarrel_Body/Sector_CannonDebrisMid", "Orbiting Giant's Deep", ImageKey.orbitalprobecannon);
					CreateTrigger("CannonMuzzle_Body/Sector_CannonDebrisTip", "Orbiting Giant's Deep", ImageKey.orbitalprobecannon);
					CreateTrigger("Satellite_Body", "Checking on \"Sky Shutter\" Satellite.", ImageKey.skyshutter);
					CreateTrigger("BackerSatellite_Body/Sector_BackerSatellite", "Checking on the Backer Satellite.", ImageKey.outerwilds);
					_shipPresence = MakePresence("Inside the ship.", ImageKey.ship);
					CreateTrigger("Ship_Body/ShipSector", _shipPresence);
					AddObservatoryHemisphere();
					if (_outsider) ModHelper.Events.Unity.RunWhen(() => SearchUtilities.Find("PowerStation/SectorDB_PowerStation/SectorTrigger_PowerStation", false, false) != null, () => CreateTrigger("PowerStation/SectorDB_PowerStation", "Orbiting Dark Bramble.", ImageKey.powerstation));
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

		private static void AddObservatoryHemisphere()
		{
			var sectorTriggerParent = SearchUtilities.Find("TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory/SectorTrigger_ObservatoryProxTrigger");
			if (sectorTriggerParent == null) return;
			var rpo = new GameObject("Observatory" + richPresenceTriggerVolume);
			rpo.transform.SetParent(sectorTriggerParent.transform.parent, false);
			rpo.transform.position = sectorTriggerParent.transform.position + new Vector3(0.3689f, 15.6428f, 1.2f);
			rpo.transform.localEulerAngles = Vector3.zero;
			rpo.transform.localScale = Vector3.one;
			rpo.SetActive(false);
			var hemisphere = rpo.AddComponent<HemisphereShape>();
			hemisphere.radius = 12;
			var owtv = rpo.AddComponent<OWTriggerVolume>();
			owtv._shape = hemisphere;
			var rptv = rpo.AddComponent<RichPresenceTriggerVolume>();
			rptv.triggerVolume = owtv;
			rptv.presence = MakePresence("Visiting the Observatory.", ImageKey.timberhearth);
			rpo.SetActive(true);
		}

		public void SetRootPresence(string message, ImageKey imageKey)
		{
			_presenceStack.Clear();
			_presenceStack.Push(MakePresence(message, imageKey));
		}

		private void Update() => client.Invoke();

		private void OnApplicationQuit()
		{
			if (client.IsInitialized) client.Deinitialize();
		}

		public void ConsoleWriteLine(string message, MessageType type, bool @override = false)
		{
			if (debug || @override)
			{
				ModHelper.Console.WriteLine(message, type);
			}
		}

		public static void WriteLine(string message, MessageType type, bool @override = false) => Instance.ConsoleWriteLine(message, type, @override);

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

		public static RichPresenceTriggerVolume CreateTriggerVolume(string triggerVolumePath, string details, ImageKey imageKey) => CreateTriggerVolume(SearchUtilities.Find(triggerVolumePath, false)?.GetComponent<OWTriggerVolume>(), details, imageKey);
		public static RichPresenceTriggerVolume CreateTriggerVolume(string triggerVolumePath, RichPresence richPresence) => CreateTriggerVolume(SearchUtilities.Find(triggerVolumePath, false)?.GetComponent<OWTriggerVolume>(), richPresence);

		public static RichPresenceTriggerVolume CreateTriggerVolume(OWTriggerVolume owTriggerVolume, string details, ImageKey imageKey) => CreateTriggerVolume(owTriggerVolume, MakePresence(details, imageKey));
		public static RichPresenceTriggerVolume CreateTriggerVolume(OWTriggerVolume owTriggerVolume, RichPresence richPresence)
		{
			if (owTriggerVolume == null) return null;
			var rptv = owTriggerVolume.gameObject.GetAddComponent<RichPresenceTriggerVolume>();
			rptv.triggerVolume = owTriggerVolume;
			rptv.presence = richPresence;
			return rptv;
		}

		public static RichPresenceTriggerVolume CreateTriggerVolume(string parentPath, float radius, string details, ImageKey imageKey) => CreateTriggerVolume(parentPath, Vector3.zero, radius, details, imageKey);
		public static RichPresenceTriggerVolume CreateTriggerVolume(string parentPath, float radius, RichPresence richPresence) => CreateTriggerVolume(parentPath, Vector3.zero, radius, richPresence);
		public static RichPresenceTriggerVolume CreateTriggerVolume(string parentPath, Vector3 localPosition, float radius, string details, ImageKey imageKey) => CreateTriggerVolume(SearchUtilities.Find(parentPath, false), localPosition, radius, details, imageKey);
		public static RichPresenceTriggerVolume CreateTriggerVolume(string parentPath, Vector3 localPosition, float radius, RichPresence richPresence) => CreateTriggerVolume(SearchUtilities.Find(parentPath, false), localPosition, radius, richPresence);

		public static RichPresenceTriggerVolume CreateTriggerVolume(GameObject parent, float radius, string details, ImageKey imageKey) => CreateTriggerVolume(parent, Vector3.zero, radius, details, imageKey);
		public static RichPresenceTriggerVolume CreateTriggerVolume(GameObject parent, float radius, RichPresence richPresence) => CreateTriggerVolume(parent, Vector3.zero, radius, richPresence);
		public static RichPresenceTriggerVolume CreateTriggerVolume(GameObject parent, Vector3 localPosition, float radius, string details, ImageKey imageKey) => CreateTriggerVolume(parent, localPosition, radius, MakePresence(details, imageKey));
		public static RichPresenceTriggerVolume CreateTriggerVolume(GameObject parent, Vector3 localPosition, float radius, RichPresence richPresence)
		{
			if (parent == null) return null;
			var rpo = parent.FindChild(richPresenceTriggerVolume);
			if (rpo != null)
			{
				var rptv = rpo.GetAddComponent<RichPresenceTriggerVolume>();
				rptv.presence = richPresence;
				return rptv;
			}
			else
			{
				rpo = new GameObject(richPresenceTriggerVolume);
				rpo.transform.SetParent(parent.transform, false);
				rpo.transform.localPosition = localPosition;
				rpo.SetActive(false);
				var ss = rpo.AddComponent<SphereShape>();
				ss.radius = radius;
				var owtv = rpo.AddComponent<OWTriggerVolume>();
				owtv._shape = ss;
				var rptv = rpo.AddComponent<RichPresenceTriggerVolume>();
				rptv.triggerVolume = owtv;
				rptv.presence = richPresence;
				rpo.SetActive(true);
				return rptv;
			}
		}

		public static RichPresenceTrigger CreateTrigger(string parentPath, string details, ImageKey imageKey) => CreateTrigger(SearchUtilities.Find(parentPath, false), details, imageKey);
		public static RichPresenceTrigger CreateTrigger(string parentPath, RichPresence richPresence) => CreateTrigger(SearchUtilities.Find(parentPath, false), richPresence);

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
		public static void SetPresence(RichPresence richPresence)
		{
			Instance.client.SetPresence(richPresence);
			foreach (var handler in Instance.handlers)
			{
				handler(richPresence.Details, richPresence.Assets.LargeImageKey, richPresence.Assets.LargeImageText);
			}
		}

		public static void RegisterHandler(Action<string, string, string> handler)
		{
			Instance.handlers.Add(handler);
		}
	}
}