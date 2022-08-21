using DiscordRPC;

namespace OWRichPresence
{
    public class RichPresenceTrigger : SectoredMonoBehaviour
    {
        public RichPresence presence;

        public override void OnSectorOccupantAdded(SectorDetector detector)
        {
            if (presence == null) return;
            if (detector.GetOccupantType() == DynamicOccupant.Player)
            {
                OWRichPresence.Instance._presenceStack.Push(presence);

                if (PlayerState.IsInsideShip() && presence != OWRichPresence.Instance._shipPresence)
				{
                    OWRichPresence.Instance._presenceStack.Remove(OWRichPresence.Instance._shipPresence);
                    OWRichPresence.Instance._presenceStack.Push(OWRichPresence.Instance._shipPresence);
                }

                if (OWRichPresence.TriggersActive)
				{
                    OWRichPresence.SetPresence(OWRichPresence.Instance._presenceStack.Peek());
                }
            }
        }

        public override void OnSectorOccupantRemoved(SectorDetector detector)
        {
            if (presence == null) return;
            if (detector.GetOccupantType() == DynamicOccupant.Player)
            {
                OWRichPresence.Instance._presenceStack.Remove(presence);

                if (PlayerState.IsInsideShip() && presence != OWRichPresence.Instance._shipPresence)
                {
                    OWRichPresence.Instance._presenceStack.Remove(OWRichPresence.Instance._shipPresence);
                    OWRichPresence.Instance._presenceStack.Push(OWRichPresence.Instance._shipPresence);
                }

                if (OWRichPresence.TriggersActive)
                {
                    OWRichPresence.SetPresence(OWRichPresence.Instance._presenceStack.Peek());
                }
            }
        }
    }
}
