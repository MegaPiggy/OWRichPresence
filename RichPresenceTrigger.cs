using DiscordRPC;

namespace OWRichPresence
{
    public class RichPresenceTrigger : SectoredMonoBehaviour
    {
        public RichPresence presence;

        public override void OnSectorOccupantAdded(SectorDetector detector)
        {
            if (detector.GetOccupantType() == DynamicOccupant.Player)
            {
                OWRichPresence.Push(presence);
            }
        }

        public override void OnSectorOccupantRemoved(SectorDetector detector)
        {
            if (detector.GetOccupantType() == DynamicOccupant.Player)
            {
                OWRichPresence.Remove(presence);
            }
        }
    }
}
