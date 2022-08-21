using DiscordRPC;
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputRemoting;

namespace OWRichPresence.API;

public class RichPresenceAPI : IRichPresenceAPI
{
	public void SetRichPresence(string message, int imageKey)
		=> OWRichPresence.SetPresence(message, (ImageKey)imageKey);

	public void SetTriggerActivation(bool active)
		=> OWRichPresence.TriggersActive = active;

	public void CreateTrigger(GameObject parent, Sector sector, string message, string imageKey)
	{
		var trigger = OWRichPresence.CreateTrigger(parent, message, (ImageKey)Enum.Parse(typeof(ImageKey), imageKey));
		trigger.SetSector(sector);
	}

	public void SetCurrentRootPresence(string message, string imageKey)
		=> OWRichPresence.Instance.SetRootPresence(message, (ImageKey)Enum.Parse(typeof(ImageKey), imageKey));
}
