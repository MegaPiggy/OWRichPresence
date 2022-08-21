namespace OWRichPresence;

public class RichPresenceAPI
{
	public void SetRichPresence(string message, int imageKey)
		=> OWRichPresence.SetPresence(message, (ImageKey)imageKey);

	public void SetTriggerActivation(bool active)
		=> OWRichPresence.TriggersActive = active;
}
