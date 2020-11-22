using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    abstract class GameSystem
    {
        HashSet<Entity> entities = new HashSet<Entity>();

        public HashSet<Entity> Entities { get => entities; set => entities = value; }

        

    }
}
