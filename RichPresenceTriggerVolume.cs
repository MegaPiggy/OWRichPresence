using DiscordRPC;
using UnityEngine;

namespace OWRichPresence
{
    public class RichPresenceTriggerVolume : MonoBehaviour
    {
        public OWTriggerVolume triggerVolume;
        public RichPresence presence;

        public void Awake()
        {
            if (triggerVolume == null) triggerVolume = GetComponent<OWTriggerVolume>();

            triggerVolume.OnEntry += OnEntry;
            triggerVolume.OnExit += OnExit;
        }

        public void OnDestroy()
        {
            triggerVolume.OnEntry -= OnEntry;
            triggerVolume.OnExit -= OnExit;
        }

        public void OnEntry(GameObject hitObj)
        {
            if (hitObj.CompareTag("PlayerDetector"))
            {
                OWRichPresence.Push(presence);
            }
        }

        public void OnExit(GameObject hitObj)
        {
            if (hitObj.CompareTag("PlayerDetector"))
            {
                OWRichPresence.Remove(presence);
            }
        }

        public void SetTriggerVolume(OWTriggerVolume owtv)
        {
            if (owtv == null) return;

            if (triggerVolume != null)
            {
                triggerVolume.OnEntry -= OnEntry;
                triggerVolume.OnExit -= OnExit;
            }

            triggerVolume = owtv;
            owtv.OnEntry += OnEntry;
            owtv.OnExit += OnExit;
        }
    }
}
