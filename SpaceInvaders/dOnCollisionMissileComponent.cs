using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class dOnCollisionMissileComponent:Component
    {
        Action<Entity> onCollision;
        
        public dOnCollisionMissileComponent(Action<Entity> onCollision)
        {
            this.onCollision = onCollision;
        }

        public Action<Entity> OnCollision { get => onCollision; }
    }
}
