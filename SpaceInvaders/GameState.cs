using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// Static class for the game states
    /// </summary>
    static class GameState
    {
        public enum state { 
            Play, 
            Pause, 
            Win, 
            Loose, 
            Menu 
        };
    }
}
