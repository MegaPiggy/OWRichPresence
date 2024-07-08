using OWML.Common;
using OWML.ModHelper;
using DiscordRPC;
using DiscordRPC.Unity;
using UnityEngine;
using OWRichPresence.API;
using System;
using System.Collections.Generic;
using OWRichPresence.Langs;

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
		private OWDiscordRPCTranslation Translation = new();



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
			string langSelected = ModHelper.Config.GetSettingsValue<string>("Language");
            switch (langSelected)
            {
                case "English":
                    Translation = English.Content;
                    break;
                case "Français":
                    Translation = French.Content;
                    break;
            }
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

        public override void Configure(IModConfig config)
        {
            string langSelected = config.GetSettingsValue<string>("Language");
            switch (langSelected)
            {
                case "English":
                    Translation = English.Content;
                    break;
                case "Français":
                    Translation = French.Content;
                    break;
            }

			if (LoadManager.s_currentScene != OWScene.None)
			{
				OnSceneLoad(LoadManager.s_currentScene);
			}

        }

        private RichPresenceTrigger CreateTriggerWithNH(string details, ImageKey imageKey) => CreateTrigger(_newHorizons?.GetPlanet(imageKey.KeyToText())?.GetComponentInChildren<Sector>()?.gameObject, details, imageKey);
		private RichPresenceTrigger CreateTriggerWithNH(string planetName, string details, ImageKey imageKey) => CreateTrigger(_newHorizons?.GetPlanet(planetName)?.GetComponentInChildren<Sector>()?.gameObject, details, imageKey);
		private RichPresenceTrigger CreateTriggerWithNH(string planetName, RichPresence richPresence) => CreateTrigger(_newHorizons?.GetPlanet(planetName)?.GetComponentInChildren<Sector>()?.gameObject, richPresence);

        private void OnSceneLoad(OWScene loadScene)
		{

			switch (loadScene)
			{
				case OWScene.TitleScreen:
					SetRootPresence(Translation.TitleScreen, ImageKey.outerwilds);
					break;
				case OWScene.SolarSystem:
					var darkbrambleimage = _outsider ? ImageKey.darkbrambleoutsider : ImageKey.darkbramble;
					var giantdeepimage = _newHorizonsExamples ? ImageKey.giantsdeepexamples : ImageKey.giantsdeep;
					CreateTrigger("TimberHearth_Body/Sector_TH", Translation.SolarSystem.TimberHearth.Exploring, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village", new Vector3(52.4282f, 43.9491f, 17.3538f), 25, Translation.SolarSystem.TimberHearth.LaunchPad, ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_ZeroGCave", Translation.SolarSystem.TimberHearth.ZeroG, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_ZeroGCave/Characters_ZeroGCave/Villager_HEA_Tuff/WatchVolume", Translation.SolarSystem.TimberHearth.Tuff, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_ZeroGCave/Characters_ZeroGCave/Villager_HEA_Tuff", 2, Translation.SolarSystem.TimberHearth.Tuff, ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_Village", Translation.SolarSystem.TimberHearth.Village, ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_Village/Sector_VillageCemetery", "Visiting the Cemetery.", ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_VillageCemetery/Characters_VillageCemetery/Villager_HEA_Tephra_PostObservatory/WatchVolume", Translation.SolarSystem.TimberHearth.Tephra, ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage", Translation.SolarSystem.TimberHearth.LowerVillage, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Villager_HEA_Mica/WatchVolume", Translation.SolarSystem.TimberHearth.Mica, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Villager_HEA_Gneiss/WatchVolume", Translation.SolarSystem.TimberHearth.Gneiss, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Villager_HEA_Spinel/WatchVolume", Translation.SolarSystem.TimberHearth.Spinel, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Villager_HEA_Spinel", 2, Translation.SolarSystem.TimberHearth.Spinel, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Villager_HEA_Rutile/WatchVolume", Translation.SolarSystem.TimberHearth.Rutile, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Villager_HEA_Marl/WatchVolume", Translation.SolarSystem.TimberHearth.Marl, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Villager_HEA_Porphy/WatchVolume", Translation.SolarSystem.TimberHearth.Porphy, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Kids_PreGame/Villager_HEA_Tephra/WatchVolume", Translation.SolarSystem.TimberHearth.Tephra, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Kids_PreGame/Villager_HEA_Galena/WatchVolume", Translation.SolarSystem.TimberHearth.Galena, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Kids_Hidden/Villager_HEA_Tephra (1)/WatchVolume", Translation.SolarSystem.TimberHearth.FoundTephra, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_LowerVillage/Characters_LowerVillage/Kids_Hidden/Villager_HEA_Galena (1)/WatchVolume", Translation.SolarSystem.TimberHearth.FoundGalena, ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_Village/Sector_StartingCamp", Translation.SolarSystem.TimberHearth.Sleeping, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_StartingCamp/Characters_StartingCamp/Villager_HEA_Slate/WatchVolume", Translation.SolarSystem.TimberHearth.Slate, ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_Village/Sector_UpperVillage", Translation.SolarSystem.TimberHearth.UpperVillage, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_UpperVillage/Characters_UpperVillage/Villager_HEA_Gossan/WatchVolume", Translation.SolarSystem.TimberHearth.Gossan, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_UpperVillage/Characters_UpperVillage/Villager_HEA_Tektite/WatchVolume", Translation.SolarSystem.TimberHearth.Tektite, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_UpperVillage/Characters_UpperVillage/Villager_HEA_Tektite", 2, Translation.SolarSystem.TimberHearth.Tektite, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_UpperVillage/Characters_UpperVillage/Villager_HEA_Arkose_GhostMatter/WatchVolume", Translation.SolarSystem.TimberHearth.Arkose, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_UpperVillage/Characters_UpperVillage/Villager_HEA_Arkose_GhostMatter", 2, Translation.SolarSystem.TimberHearth.Arkose, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_UpperVillage/Characters_UpperVillage/Villager_HEA_Moraine", 2, Translation.SolarSystem.TimberHearth.Moraine, ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory", Translation.SolarSystem.TimberHearth.Observatory, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory/Characters_Observatory/Character_HEA_Hal_Museum/WatchVolume", Translation.SolarSystem.TimberHearth.Hal, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory/Characters_Observatory/Character_HEA_Hal_Museum", 2, Translation.SolarSystem.TimberHearth.Hal, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Characters_Village/Villager_HEA_Hal_Outside/WatchVolume", Translation.SolarSystem.TimberHearth.Hal, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Characters_Village/Villager_HEA_Hal_Outside", 2, Translation.SolarSystem.TimberHearth.Hal, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory/Characters_Observatory/Villager_HEA_Hornfels/WatchVolume", Translation.SolarSystem.TimberHearth.Hornfels, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory/Characters_Observatory/Villager_HEA_Hornfels", 2, Translation.SolarSystem.TimberHearth.Hornfels, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory/Characters_Observatory/Villager_HEA_Hornfels (1)/WatchVolume", Translation.SolarSystem.TimberHearth.Hornfels, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory/Characters_Observatory/Villager_HEA_Hornfels (1)", 2, Translation.SolarSystem.TimberHearth.Hornfels, ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_WaterWays", Translation.SolarSystem.TimberHearth.Inside, ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_NomaiCrater", Translation.SolarSystem.TimberHearth.Crater, ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_ImpactCrater", Translation.SolarSystem.TimberHearth.Crater, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_ImpactCrater/Characters_ImpactCrater/Villager_HEA_Tektite_2/WatchVolume", Translation.SolarSystem.TimberHearth.Tektite, ImageKey.timberhearth);
					CreateTriggerVolume("TimberHearth_Body/Sector_TH/Sector_ImpactCrater/Characters_ImpactCrater/Villager_HEA_Tektite_2", 2, Translation.SolarSystem.TimberHearth.Tektite, ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_NomaiMines", Translation.SolarSystem.TimberHearth.Inside, ImageKey.timberhearth);
					CreateTrigger("TimberHearth_Body/Sector_TH/Sector_NomaiMines/Sector_NomaiMinesInterior", Translation.SolarSystem.TimberHearth.Inside, ImageKey.timberhearth);
					CreateTrigger("Moon_Body/Sector_THM", Translation.SolarSystem.Attlerock.Exploring, ImageKey.attlerock);
					CreateTriggerVolume("Moon_Body/Sector_THM/Volumes_THM/PineGroveVolume", Translation.SolarSystem.Attlerock.Esker, ImageKey.attlerock);
					CreateTrigger("BrittleHollow_Body/Sector_BH", Translation.SolarSystem.BrittleHollow.Exploring, ImageKey.brittlehollow);
					CreateTrigger("BrittleHollow_Body/Sector_BH/Sector_Crossroads", Translation.SolarSystem.BrittleHollow.Crossroads, ImageKey.brittlehollow);
					CreateTriggerVolume("BrittleHollow_Body/Sector_BH/Sector_Crossroads/Characters_Crossroads/Traveller_HEA_Riebeck", new Vector3(-0.134f, 1.651f, 0.279f), 5, Translation.SolarSystem.BrittleHollow.Riebeck, ImageKey.brittlehollow);
					CreateTrigger("VolcanicMoon_Body/Sector_VM", Translation.SolarSystem.HollowsLantern.Exploring, ImageKey.hollowslantern);
					CreateTrigger("Sun_Body/Sector_SUN", Translation.SolarSystem.Sun.Burning, ImageKey.sun);
					CreateTrigger("SunStation_Body/Sector_SunStation", Translation.SolarSystem.Sun.Orbiting, ImageKey.sunstation);
					CreateTrigger("TowerTwin_Body/Sector_TowerTwin", Translation.SolarSystem.AshTwin.Exploring, ImageKey.ashtwin);
					CreateTrigger("CaveTwin_Body/Sector_CaveTwin", Translation.SolarSystem.EmberTwin.Exploring, ImageKey.embertwin);
					CreateTriggerVolume("CaveTwin_Body/Sector_CaveTwin/Sector_NorthHemisphere/Sector_NorthSurface/Sector_Lakebed/Interactables_Lakebed/Traveller_HEA_Chert", Vector3.zero, 5, Translation.SolarSystem.EmberTwin.Chert, ImageKey.embertwin);
					CreateTrigger("QuantumMoon_Body/Sector_QuantumMoon", Translation.SolarSystem.QuantumMoon.Exploring, ImageKey.quantummoon);
					CreateTrigger("DreamWorld_Body/Sector_DreamWorld", Translation.SolarSystem.DreamWorld.Exploring, ImageKey.dreamworld);
					CreateTrigger("RingWorld_Body/Sector_RingWorld", Translation.SolarSystem.Stranger.Exploring, ImageKey.stranger);
					CreateTrigger("RingWorld_Body/Sector_RingInterior", Translation.SolarSystem.Stranger.Exploring, ImageKey.stranger);
					CreateTrigger("GiantsDeep_Body/Sector_GD", Translation.SolarSystem.GiantsDeep.Exploring, giantdeepimage);
					CreateTrigger("DarkBramble_Body/Sector_DB", Translation.SolarSystem.DarkBramble.Exploring, darkbrambleimage);
					CreateTrigger("DB_AnglerNestDimension_Body/Sector_AnglerNestDimension", Translation.SolarSystem.DarkBramble.Somewhere, darkbrambleimage);
					CreateTrigger("DB_ClusterDimension_Body/Sector_ClusterDimension", Translation.SolarSystem.DarkBramble.Somewhere, darkbrambleimage);
					CreateTrigger("DB_Elsinore_Body/Sector_ElsinoreDimension", Translation.SolarSystem.DarkBramble.Somewhere, darkbrambleimage);
					CreateTrigger("DB_EscapePodDimension_Body/Sector_EscapePodDimension", Translation.SolarSystem.DarkBramble.Somewhere, darkbrambleimage);
					CreateTrigger("DB_ExitOnlyDimension_Body/Sector_ExitOnlyDimension", Translation.SolarSystem.DarkBramble.Somewhere, darkbrambleimage);
					CreateTrigger("DB_HubDimension_Body/Sector_HubDimension", Translation.SolarSystem.DarkBramble.Somewhere, darkbrambleimage);
					CreateTrigger("DB_PioneerDimension_Body/Sector_PioneerDimension", Translation.SolarSystem.DarkBramble.Somewhere, darkbrambleimage);
					CreateTrigger("DB_SmallNest_Body/Sector_SmallNestDimension", Translation.SolarSystem.DarkBramble.Somewhere, darkbrambleimage);
					CreateTrigger("DB_VesselDimension_Body/Sector_VesselDimension", Translation.SolarSystem.DarkBramble.Somewhere, darkbrambleimage);
					CreateTrigger("WhiteHole_Body/Sector_WhiteHole", Translation.SolarSystem.WhiteHole.Exploring, ImageKey.whitehole);
					CreateTrigger("WhiteholeStation_Body/Sector_WhiteholeStation", Translation.SolarSystem.WhiteHoleStation.Exploring, ImageKey.whitehole);
					CreateTrigger("FocalBody/Sector_HGT", Translation.SolarSystem.HourglassTwins.Exploring, ImageKey.hourglasstwins);
					CreateTrigger("Comet_Body/Sector_CO", Translation.SolarSystem.Interloper.Exploring, ImageKey.interloper);
					CreateTrigger("HearthianMapSatellite_Body/Sector_HearthianMapSatellite", Translation.SolarSystem.MapSatellite.Checking, ImageKey.outerwilds);
					CreateTrigger("OrbitalProbeCannon_Body/Sector_OrbitalProbeCannon", Translation.SolarSystem.GiantsDeep.Orbiting, ImageKey.orbitalprobecannon);
					CreateTrigger("GabbroShip_Body/Sector_GabbroShip", Translation.SolarSystem.GiantsDeep.GabbrosShip, ImageKey.ship);
					CreateTrigger("StatueIsland_Body/Sector_StatueIsland", Translation.SolarSystem.GiantsDeep.StatueIsland, giantdeepimage);
					CreateTrigger("GabbroIsland_Body/Sector_GabbroIsland", Translation.SolarSystem.GiantsDeep.GabbroIsland, giantdeepimage);
					CreateTriggerVolume("GabbroIsland_Body/Sector_GabbroIsland/Interactables_GabbroIsland/Traveller_HEA_Gabbro", new Vector3(-0.09f, 1.21f, 0), 5, Translation.SolarSystem.GiantsDeep.Gabbro, giantdeepimage);
					CreateTrigger("ConstructionYardIsland_Body/Sector_ConstructionYard", Translation.SolarSystem.GiantsDeep.Yard, giantdeepimage);
					CreateTrigger("BrambleIsland_Body/Sector_BrambleIsland", Translation.SolarSystem.GiantsDeep.BrambleIsland, giantdeepimage);
					CreateTrigger("QuantumIsland_Body/Sector_QuantumIsland", Translation.SolarSystem.GiantsDeep.Somewhere, giantdeepimage);
					CreateTrigger("CannonBarrel_Body/Sector_CannonDebrisMid", Translation.SolarSystem.GiantsDeep.Orbiting, ImageKey.orbitalprobecannon);
					CreateTrigger("CannonMuzzle_Body/Sector_CannonDebrisTip", Translation.SolarSystem.GiantsDeep.Orbiting, ImageKey.orbitalprobecannon);
					CreateTrigger("Satellite_Body", Translation.SolarSystem.TimberHearth.SkyShutter, ImageKey.skyshutter);
					CreateTrigger("BackerSatellite_Body/Sector_BackerSatellite", Translation.SolarSystem.BackerSatellite.Checking, ImageKey.outerwilds);
					_shipPresence = MakePresence(Translation.SolarSystem.Ship.Inside, ImageKey.ship);
					CreateTrigger("Ship_Body/ShipSector", _shipPresence);
					AddObservatoryHemisphere();
					if (_outsider) ModHelper.Events.Unity.RunWhen(() => SearchUtilities.Find("PowerStation/SectorDB_PowerStation/SectorTrigger_PowerStation", false, false) != null, () => CreateTrigger("PowerStation/SectorDB_PowerStation", Translation.SolarSystem.DarkBramble.Orbiting, ImageKey.powerstation));
					SetRootPresence(Translation.SolarSystem.Exploring, ImageKey.sun);
					break;
				case OWScene.EyeOfTheUniverse:
					SetRootPresence(Translation.EyeOfTheUniverse, ImageKey.eyeoftheuniverse);
					break;
				case OWScene.Credits_Fast:
					SetRootPresence(Translation.CreditsFast, ImageKey.outerwilds);
					break;
				case OWScene.Credits_Final:
					SetRootPresence(Translation.CreditsFinal, ImageKey.outerwilds);
					break;
				case OWScene.PostCreditsScene:
					SetRootPresence(Translation.PostCreditScene, ImageKey.outerwilds);
					break;
				case OWScene.None:
				case OWScene.Undefined:
				default:
					SetRootPresence(Translation.Unknown, ImageKey.outerwilds);
					break;
			}
			SetPresence(_presenceStack.Peek());
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

		public static void SetPresence(string details, ImageKey imageKey) => SetPresence(MakePresence(details, imageKey));
		public static void SetPresence(RichPresence richPresence)
		{
			foreach (var handler in Instance.handlers)
			{
				handler(richPresence.Details, richPresence.Assets.LargeImageKey, richPresence.Assets.LargeImageText);
			}
			Instance.client.SetPresence(richPresence);
		}

		public static void RegisterHandler(Action<string, string, string> handler)
		{
			Instance.handlers.Add(handler);
		}
	}
}