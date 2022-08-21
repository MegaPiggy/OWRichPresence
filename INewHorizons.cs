using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace OWRichPresence
{
    public interface INewHorizons
    {
        /// <summary>
        /// Retrieve the root GameObject of a custom planet made by creating configs. 
        /// Will only work if the planet has been created (see GetStarSystemLoadedEvent)
        /// </summary>
        GameObject GetPlanet(string name);

        /// <summary>
        /// The name of the current star system loaded.
        /// </summary>
        string GetCurrentStarSystem();

        /// <summary>
        /// An event invoked when NH has finished generating all planets for a new star system.
        /// Gives the name of the star system that was just loaded.
        /// </summary>
        UnityEvent<string> GetStarSystemLoadedEvent();
    }
}
