using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// Component to hold the entity's side
    /// </summary>
    class SideComponent:Component
    {
        
        EntitySide.Side side;

        public SideComponent(EntitySide.Side entitySide)
        {
            Side = entitySide;
        }

        public EntitySide.Side Side { get => side; set => side = value; }
    }
}
