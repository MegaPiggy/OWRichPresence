using UnityEngine;

namespace OWRichPresence;

public class RichPresenceAPI
{
	public void SetRichPresence(string message, int imageKey)
		=> OWRichPresence.SetPresence(message, (ImageKey)imageKey);

	public void SetRichPresence(string message, ImageKey imageKey)
		=> OWRichPresence.SetPresence(message, imageKey);

	public GameObject CreateTrigger(GameObject parent, string message, ImageKey imageKey)
		=> OWRichPresence.CreateTrigger(parent, message, imageKey)?.gameObject;

	public void SetTriggerActivation(bool active)
		=> OWRichPresence.TriggersActive = active;
}
