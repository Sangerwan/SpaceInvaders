using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class DamageComponent:Component
    {
        int damage;

        public DamageComponent(int damage)
        {
            this.Damage = damage;
        }

        public int Damage { get => damage; set => damage = value; }
    }
}
