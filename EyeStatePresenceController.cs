using DiscordRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OWRichPresence
{
    public class EyeStatePresenceController : MonoBehaviour
    {
        private void Awake() => GlobalMessenger<EyeState>.AddListener("EyeStateChanged", OnEyeStateChanged);

        private void OnDestroy() => GlobalMessenger<EyeState>.RemoveListener("EyeStateChanged", OnEyeStateChanged);

        private void Start() => OnEyeStateChanged(Locator.GetEyeStateManager().GetState());

        private RichPresence _currentPresence;

        private void OnEyeStateChanged(EyeState state)
        {
            var details = "Exploring";
            switch (state)
            {
                case EyeState.AboardVessel:
                    details = "Aboard Vessel";
                    break;
                case EyeState.WarpedToSurface:
                    details = "Warped to Surface";
                    break;
                case EyeState.IntoTheVortex:
                    details = "Into the Vortex";
                    break;
                case EyeState.Observatory:
                    details = "Observatory";
                    break;
                case EyeState.ZoomOut:
                    details = "Zoom Out";
                    break;
                case EyeState.ForestOfGalaxies:
                    details = "Forest of Galaxies";
                    break;
                case EyeState.ForestIsDark:
                    details = "Forest is Dark";
                    break;
                case EyeState.InstrumentHunt:
                    details = "Instrument Hunt";
                    break;
                case EyeState.JamSession:
                    details = "Jam Session";
                    break;
                case EyeState.BigBang:
                    details = "Big Bang";
                    break;
            }

            OWRichPresence.Instance._presenceStack.Pop();
            OWRichPresence.Instance._presenceStack.Push(_currentPresence);
            _currentPresence = OWRichPresence.MakePresence(details, ImageKey.eyeoftheuniverse);
            OWRichPresence.SetPresence(OWRichPresence.Instance._presenceStack.Peek());
        }
    }
}
