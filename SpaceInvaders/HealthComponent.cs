using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class HealthComponent:Component
    {
        int life;
        public HealthComponent(int life)
        {
            this.Life = life;
        }

        public int Life { get => life; set => life = value; }
    }
}
