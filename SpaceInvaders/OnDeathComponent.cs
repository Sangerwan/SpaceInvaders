using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class OnDeathComponent:Component
    {
        Action action;
        public OnDeathComponent()
        {

        }
        public OnDeathComponent(Action action)
        {
            this.Action= action;
        }

        public Action Action { get => action; set => action = value; }
    }
}
