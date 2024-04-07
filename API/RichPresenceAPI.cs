using DiscordRPC;
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputRemoting;
using OWML.Utils;

namespace OWRichPresence.API;

public class RichPresenceAPI : IRichPresenceAPI
{
	public void SetRichPresence(string message, int imageKey)
		=> OWRichPresence.SetPresence(message, (ImageKey)imageKey);

	public void SetRichPresence(string message, string imageKey)
		=> OWRichPresence.SetPresence(message, EnumUtils.Parse<ImageKey>(imageKey, true, ImageKey.sun));

	public void SetTriggerActivation(bool active)
		=> OWRichPresence.TriggersActive = active;

	public GameObject CreateTrigger(GameObject parent, string message, string imageKey) => CreateTrigger(parent, message, imageKey, nameof(ImageKey.sun));
	public GameObject CreateTrigger(GameObject parent, string message, string imageKey, string fallback)
	{
		var imageKeyEnum = EnumUtils.Parse<ImageKey>(imageKey, true, EnumUtils.Parse<ImageKey>(fallback, true, ImageKey.sun));

		return OWRichPresence.CreateTrigger(parent, message, imageKeyEnum)?.gameObject;
	}

	public GameObject CreateTrigger(GameObject parent, Sector sector, string message, string imageKey) => CreateTrigger(parent, sector, message, imageKey, nameof(ImageKey.sun));

	public GameObject CreateTrigger(GameObject parent, Sector sector, string message, string imageKey, string fallback)
	{
		var imageKeyEnum = EnumUtils.Parse<ImageKey>(imageKey, true, EnumUtils.Parse<ImageKey>(fallback, true, ImageKey.sun));

		var trigger = OWRichPresence.CreateTrigger(parent, message, imageKeyEnum);
		trigger.SetSector(sector);
		return trigger.gameObject;
	}

	public void CreateTriggerVolume(OWTriggerVolume triggerVolume, string message, string imageKey) => CreateTriggerVolume(triggerVolume, message, imageKey, nameof(ImageKey.sun));

	public void CreateTriggerVolume(OWTriggerVolume triggerVolume, string message, string imageKey, string fallback)
	{
		var imageKeyEnum = EnumUtils.Parse<ImageKey>(imageKey, true, EnumUtils.Parse<ImageKey>(fallback, true, ImageKey.sun));

		OWRichPresence.CreateTriggerVolume(triggerVolume, message, imageKeyEnum);
	}

	public GameObject CreateTriggerVolume(GameObject parent, float radius, string message, string imageKey) => CreateTriggerVolume(parent, radius, message, imageKey, nameof(ImageKey.sun));

	public GameObject CreateTriggerVolume(GameObject parent, float radius, string message, string imageKey, string fallback)
	{
		var imageKeyEnum = EnumUtils.Parse<ImageKey>(imageKey, true, EnumUtils.Parse<ImageKey>(fallback, true, ImageKey.sun));

		return OWRichPresence.CreateTriggerVolume(parent, radius, message, imageKeyEnum)?.gameObject;
	}

	public GameObject CreateTriggerVolume(GameObject parent, Vector3 localPosition, float radius, string message, string imageKey) => CreateTriggerVolume(parent, localPosition, radius, message, imageKey, nameof(ImageKey.sun));

	public GameObject CreateTriggerVolume(GameObject parent, Vector3 localPosition, float radius, string message, string imageKey, string fallback)
	{
		var imageKeyEnum = EnumUtils.Parse<ImageKey>(imageKey, true, EnumUtils.Parse<ImageKey>(fallback, true, ImageKey.sun));

		return OWRichPresence.CreateTriggerVolume(parent, localPosition, radius, message, imageKeyEnum)?.gameObject;
	}

	public void SetCurrentRootPresence(string message, string imageKey)
		=> OWRichPresence.Instance.SetRootPresence(message, EnumUtils.Parse<ImageKey>(imageKey, true, ImageKey.sun));

	public void RegisterHandler(Action<string, string, string> handler)
		=> OWRichPresence.RegisterHandler(handler);
}
