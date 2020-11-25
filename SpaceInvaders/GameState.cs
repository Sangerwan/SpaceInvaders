using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
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
