using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class HealthComponent:Component
    {
        int healthPoint;
        public HealthComponent(int life)
        {
            this.HP = life;
        }

        public int HP { get => healthPoint; set => healthPoint = value; }
    }
}
