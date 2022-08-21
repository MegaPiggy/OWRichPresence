using DiscordRPC;
using System;
using UnityEngine;

namespace OWRichPresence.API;

public interface IRichPresenceAPI
{
    public void SetRichPresence(string message, int imageKey);
    public void SetTriggerActivation(bool active);
    public void CreateTrigger(GameObject parent, Sector sector, string message, string imageKey);
    public void SetCurrentRootPresence(string message, string imageKey);
}
