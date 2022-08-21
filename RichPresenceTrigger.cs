using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRichPresence
{
    public class RichPresenceTrigger : SectoredMonoBehaviour
    {
        public string details;
        public ImageKey imageKey;

        public override void OnSectorOccupantAdded(SectorDetector detector)
        {
            if (detector.GetOccupantType() == DynamicOccupant.Player)
            {
                OWRichPresence.SetPresence(details, imageKey);
            }
        }

        public override void OnSectorOccupantRemoved(SectorDetector detector)
        {
            if (detector.GetOccupantType() == DynamicOccupant.Player)
            {
                OWRichPresence.SetPresence("Exploring the solar system", ImageKey.sun);
            }
        }
    }
}
