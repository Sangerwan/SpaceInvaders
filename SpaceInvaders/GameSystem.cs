using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// Abstract class for systems in the game
    /// </summary>
    abstract class GameSystem
    {
        /// <summary>
        /// Generic method to update the system
        /// </summary>
        /// <param name="gameEngine">Current game</param>
        /// <param name="deltaT">Time elapsed since last update</param>
        public virtual void update(GameEngine gameEngine, double deltaT)
        {

        }

    }
}
