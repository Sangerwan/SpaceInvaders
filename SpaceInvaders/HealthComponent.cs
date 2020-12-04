using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// Component for health
    /// </summary>
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
