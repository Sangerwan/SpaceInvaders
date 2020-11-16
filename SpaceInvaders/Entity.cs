using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Entity
    {
        HashSet<Component> components;

        public void addComponent(Component component)
        {
            components.Add(component);
        }

        public void removeComponent(Component component)
        {

        }
    }
}
