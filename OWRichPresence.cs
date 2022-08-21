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
		public BaseRichPresence CurrentPresence { get; private set; }

		private void Awake()
		{
			Instance = this;
		}

		private void OnClose(object sender, DiscordRPC.Message.CloseMessage e)
		{
			ConsoleWriteLine($"Closed! {e.Code}: {e.Reason}", MessageType.Warning);
		}

		private void Start()
		{
			// Starting here, you'll have access to OWML's mod helper.
			ConsoleWriteLine($"My mod {nameof(OWRichPresence)} is loaded!", MessageType.Success);

			var logger = new OWConsoleLogger(MessageType.Debug);
			client = new DiscordRpcClient("1010346259757944882", -1, logger, false, new UnityNamedPipe(logger));

			client.OnConnectionEstablished += OnConnectionEstablished;

			client.OnConnectionFailed += OnConnectionFailed;

			client.OnError += OnError;

			client.OnSpectate += OnSpectate;

			client.OnJoinRequested += OnJoinRequested;

			client.OnJoin += OnJoin;

			client.OnReady += OnReady;

			client.OnPresenceUpdate += OnPresenceUpdate;

			client.OnClose += OnClose;

			client.OnSubscribe += OnSubscribe;

			client.OnUnsubscribe += OnUnsubscribe;

			client.Initialize();

			OnSceneLoad(OWScene.TitleScreen);

			LoadManager.OnCompleteSceneLoad += (originalScene, loadScene) => OnSceneLoad(loadScene);
		}

		private void OnSceneLoad(OWScene loadScene)
		{
			RichPresence richPresence;
			switch (loadScene)
			{
				case OWScene.TitleScreen:
					richPresence = MakePresence("Title Screen", ImageKey.outerwilds);
					break;
				case OWScene.SolarSystem:
					CreateTrigger(SearchUtilities.Find("TimberHearth_Body/Sector_TH"), "Exploring Timber Hearth", ImageKey.timberhearth);
					CreateTrigger(SearchUtilities.Find("Moon_Body/Sector_THM"), "Exploring the Attlerock", ImageKey.attlerock);
					CreateTrigger(SearchUtilities.Find("BrittleHollow_Body/Sector_BH"), "Exploring Brittle Hollow", ImageKey.brittlehollow);
					CreateTrigger(SearchUtilities.Find("VolcanicMoon_Body/Sector_VM"), "Exploring Hollow's Lantern", ImageKey.hollowslantern);
					CreateTrigger(SearchUtilities.Find("Sun_Body/Sector_SUN"), "Burning up near the Sun", ImageKey.sun);
					CreateTrigger(SearchUtilities.Find("SunStation_Body/Sector_SunStation"), "Exploring Sun Station", ImageKey.sunstation);
					CreateTrigger(SearchUtilities.Find("TowerTwin_Body/Sector_TowerTwin"), "Exploring Ash Twin", ImageKey.ashtwin);
					CreateTrigger(SearchUtilities.Find("CaveTwin_Body/Sector_CaveTwin"), "Exploring Ember Twin", ImageKey.embertwin);
					CreateTrigger(SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon"), "Exploring Quantum Moon", ImageKey.quantummoon);
					CreateTrigger(SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld"), "Exploring Dreamworld", ImageKey.dreamworld);
					CreateTrigger(SearchUtilities.Find("RingWorld_Body/Sector_RingWorld"), "Exploring Stranger", ImageKey.stranger);
					CreateTrigger(SearchUtilities.Find("GiantsDeep_Body/Sector_GD"), "Exploring Giant's Deep", ImageKey.giantsdeep);
					CreateTrigger(SearchUtilities.Find("DarkBramble_Body/Sector_DB"), "Exploring Dark Bramble", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_AnglerNestDimension_Body/Sector_AnglerNestDimension"), "Exploring Angler Nest Dimension", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_ClusterDimension_Body/Sector_ClusterDimension"), "Exploring Cluster Dimension", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_Elsinore_Body/Sector_ElsinoreDimension"), "Exploring Elsinore Dimension", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_EscapePodDimension_Body/Sector_EscapePodDimension"), "Exploring Escape Pod Dimension", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_ExitOnlyDimension_Body/Sector_ExitOnlyDimension"), "Exploring Exit Only Dimension", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_HubDimension_Body/Sector_HubDimension"), "Exploring Hub Dimension", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_PioneerDimension_Body/Sector_PioneerDimension"), "Exploring Pioneer Dimension", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_SmallNest_Body/Sector_SmallNestDimension"), "Exploring Small Nest Dimension", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("DB_VesselDimension_Body/Sector_VesselDimension"), "Exploring Vessel Dimension", ImageKey.darkbramble);
					CreateTrigger(SearchUtilities.Find("WhiteHole_Body/Sector_WhiteHole"), "Exploring White Hole", ImageKey.whitehole);
					CreateTrigger(SearchUtilities.Find("WhiteholeStation_Body/Sector_WhiteholeStation"), "Exploring White Hole Station", ImageKey.whitehole);
					CreateTrigger(SearchUtilities.Find("FocalBody/Sector_HGT"), "Exploring Hourglass Twins", ImageKey.hourglasstwins);
					CreateTrigger(SearchUtilities.Find("Comet_Body/Sector_CO"), "Exploring Interloper", ImageKey.interloper);
					CreateTrigger(SearchUtilities.Find("HearthianMapSatellite_Body/Sector_HearthianMapSatellite"), "Checking on Map Satellite", ImageKey.outerwilds);
					CreateTrigger(SearchUtilities.Find("OrbitalProbeCannon_Body/Sector_OrbitalProbeCannon"), "Exploring Orbital Probe Cannon", ImageKey.orbitalprobecannon);
					CreateTrigger(SearchUtilities.Find("GabbroShip_Body/Sector_GabbroShip"), "Checking on Gabbro's Ship", ImageKey.ship);
					CreateTrigger(SearchUtilities.Find("StatueIsland_Body/Sector_StatueIsland"), "Exploring Statue Island", ImageKey.giantsdeep);
					CreateTrigger(SearchUtilities.Find("GabbroIsland_Body/Sector_GabbroIsland"), "Exploring Gabbro's Island", ImageKey.giantsdeep);
					CreateTrigger(SearchUtilities.Find("ConstructionYardIsland_Body/Sector_ConstructionYard"), "Exploring Construction Yard", ImageKey.giantsdeep);
					CreateTrigger(SearchUtilities.Find("BrambleIsland_Body/Sector_BrambleIsland"), "Exploring Bramble Island", ImageKey.giantsdeep);
					CreateTrigger(SearchUtilities.Find("QuantumIsland_Body/Sector_QuantumIsland"), "Exploring Quantum Tower", ImageKey.giantsdeep);
					CreateTrigger(SearchUtilities.Find("CannonBarrel_Body/Sector_CannonDebrisMid"), "Exploring Orbital Probe Cannon Barrel", ImageKey.orbitalprobecannon);
					CreateTrigger(SearchUtilities.Find("CannonMuzzle_Body/Sector_CannonDebrisTip"), "Exploring Orbital Probe Cannon Muzzle", ImageKey.orbitalprobecannon);
					CreateTrigger(SearchUtilities.Find("Satellite_Body"), "Checking on \"Sky Shutter\" Satellite", ImageKey.skyshutter);
					CreateTrigger(SearchUtilities.Find("Ship_Body/ShipSector"), "Inside their ship", ImageKey.ship);
					CreateTrigger(SearchUtilities.Find("BackerSatellite_Body/Sector_BackerSatellite"), "Checking on Backer Satellite", ImageKey.outerwilds);
					richPresence = MakePresence("Exploring the solar system", ImageKey.sun);
					break;
				case OWScene.EyeOfTheUniverse:
					new GameObject("EyeStatePresenceController", typeof(EyeStatePresenceController));
					richPresence = MakePresence("Exploring", ImageKey.eyeoftheuniverse);
					break;
				case OWScene.Credits_Fast:
					richPresence = MakePresence("Credits", ImageKey.outerwilds);
					break;
				case OWScene.Credits_Final:
					richPresence = MakePresence("Beat the game", ImageKey.outerwilds);
					break;
				case OWScene.PostCreditsScene:
					richPresence = MakePresence("14.6 billion years later", ImageKey.outerwilds);
					break;
				case OWScene.None:
				case OWScene.Undefined:
				default:
					richPresence = MakePresence("Unknown", ImageKey.outerwilds);
					break;
			}
			client.SetPresence(richPresence);
		}

		private void Update() => client.Invoke();

		private void OnApplicationQuit() => client.Deinitialize();

		private void OnUnsubscribe(object sender, DiscordRPC.Message.UnsubscribeMessage e)
		{
			ConsoleWriteLine($"Unsubscribed to event {e.Event}", MessageType.Info);
		}

		private void OnSubscribe(object sender, DiscordRPC.Message.SubscribeMessage e)
		{
			ConsoleWriteLine($"Subscribed to event {e.Event}", MessageType.Info);
		}

		private void OnJoin(object sender, DiscordRPC.Message.JoinMessage e)
		{
			ConsoleWriteLine($"Joined with secret {e.Secret}", MessageType.Info);
		}

		private void OnJoinRequested(object sender, DiscordRPC.Message.JoinRequestMessage e)
		{
			ConsoleWriteLine($"Join requested from user {e.User.Username}", MessageType.Info);
		}

		private void OnSpectate(object sender, DiscordRPC.Message.SpectateMessage e)
		{
			ConsoleWriteLine($"Spectating with secret {e.Secret}", MessageType.Info);
		}

		private void OnError(object sender, DiscordRPC.Message.ErrorMessage e)
		{
			ConsoleWriteLine($"{e.Code}: {e.Message}", MessageType.Error);
		}

		private void OnConnectionEstablished(object sender, DiscordRPC.Message.ConnectionEstablishedMessage e)
		{
			ConsoleWriteLine($"Connection has been established with pipe #{e.ConnectedPipe}!", MessageType.Success);
		}

		private void OnConnectionFailed(object sender, DiscordRPC.Message.ConnectionFailedMessage e)
		{
			ConsoleWriteLine($"Failed to connect with pipe #{e.FailedPipe}!", MessageType.Error);
		}

		private void OnReady(object sender, DiscordRPC.Message.ReadyMessage e)
		{
			ConsoleWriteLine($"Received Ready from user {e.User.Username}", MessageType.Info);
		}

		private void OnPresenceUpdate(object sender, DiscordRPC.Message.PresenceMessage e)
		{
			CurrentPresence = e.Presence;
		}

#if DEBUG
		private static bool debug = true;
#else
		private static bool debug = false;
#endif

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
			rpt.details = details;
			rpt.imageKey = imageKey;
			rpo.SetActive(true);
			return rpt;
		}

		public static RichPresence MakePresence(string details, ImageKey imageKey) => new RichPresence
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
