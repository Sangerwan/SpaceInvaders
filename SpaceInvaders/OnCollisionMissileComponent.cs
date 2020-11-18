using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class OnCollisionMissileComponent:Component
    {
        Action<Entity> onCollision;
        
        public OnCollisionMissileComponent(Action<Entity> onCollision)
        {
            this.onCollision = onCollision;
        }

        public Action<Entity> OnCollision { get => onCollision; }
    }
}
