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
	{
		if (!Enum.TryParse(imageKey, out ImageKey imageKeyEnum)) imageKeyEnum = ImageKey.sun;

		return OWRichPresence.CreateTrigger(parent, message, imageKeyEnum)?.gameObject;
	}

	public GameObject CreateTrigger(GameObject parent, Sector sector, string message, string imageKey)
	{
		if (!Enum.TryParse(imageKey, out ImageKey imageKeyEnum)) imageKeyEnum = ImageKey.sun;

		var trigger = OWRichPresence.CreateTrigger(parent, message, imageKeyEnum);
		trigger.SetSector(sector);
		return trigger.gameObject;
	}

	public void SetCurrentRootPresence(string message, string imageKey)
		=> OWRichPresence.Instance.SetRootPresence(message, (ImageKey)Enum.Parse(typeof(ImageKey), imageKey));
}
