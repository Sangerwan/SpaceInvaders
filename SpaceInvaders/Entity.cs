using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Entity
    {
        HashSet<Component> components;

        public Entity()
        {
            this.components = new HashSet<Component>();
        }

        public void addComponent(Component component)
        {
            components.Add(component);
        }

        public void removeComponent(Type componentType)
        {
            foreach (Component component in components)
            {
                if (component.GetType() == componentType)
                {
                    components.Remove(component);
                    return;
                }
            }
        }

        public Component GetComponent(Type componentType)
        {
            
            foreach(Component component in components)
            {
                if (component.GetType() == componentType) return component;
            }
            return null;
        }
    }
}
