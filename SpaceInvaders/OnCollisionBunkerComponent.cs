using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class OnCollisionBunkerComponent:Component
    {
        Action<Entity> onCollision;

        public OnCollisionBunkerComponent(Action<Entity> onCollision)
        {
            this.onCollision = onCollision;
        }

        public Action<Entity> OnCollision { get => onCollision;}
    }
}
