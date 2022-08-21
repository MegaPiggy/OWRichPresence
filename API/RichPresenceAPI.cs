using DiscordRPC;
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputRemoting;

namespace OWRichPresence.API;

public class RichPresenceAPI : IRichPresenceAPI
{
	public void SetRichPresence(string message, int imageKey)
		=> OWRichPresence.SetPresence(message, (ImageKey)imageKey);

	public void SetRichPresence(string message, string imageKey)
		=> OWRichPresence.SetPresence(message, (ImageKey)Enum.Parse(typeof(ImageKey), imageKey));

	public void SetTriggerActivation(bool active)
		=> OWRichPresence.TriggersActive = active;

	public GameObject CreateTrigger(GameObject parent, string message, string imageKey)
		=> OWRichPresence.CreateTrigger(parent, message, (ImageKey)Enum.Parse(typeof(ImageKey), imageKey))?.gameObject;

	public GameObject CreateTrigger(GameObject parent, Sector sector, string message, string imageKey)
	{
		var trigger = OWRichPresence.CreateTrigger(parent, message, (ImageKey)Enum.Parse(typeof(ImageKey), imageKey));
		trigger.SetSector(sector);
		return trigger.gameObject;
	}

	public void SetCurrentRootPresence(string message, string imageKey)
		=> OWRichPresence.Instance.SetRootPresence(message, (ImageKey)Enum.Parse(typeof(ImageKey), imageKey));
}
