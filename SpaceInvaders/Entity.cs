using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// An Entity is a class that hold components
    /// </summary>
    class Entity
    {
        /// <summary>
        /// List of Component
        /// </summary>
        HashSet<Component> components;

        /// <summary>
        /// Simple Contructor
        /// </summary>
        public Entity()
        {
            this.components = new HashSet<Component>();
        }

        /// <summary>
        /// Add component(s) to the entity
        /// </summary>
        /// <param name="componentList">The list of components to be added</param>
        public void addComponent(params Component[] componentList)
        {
            foreach (Component component in componentList)
            {
                components.Add(component);
            }

        }

        /// <summary>
        /// Remove a component from the entity
        /// </summary>
        /// <param name="componentType">The type of the component to be removed</param>
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

        /// <summary>
        /// Return the component of the input type
        /// </summary>
        /// <param name="componentType">The type of the component</param>
        /// <returns>Return the component if found, else null</returns>
        public Component GetComponent(Type componentType)
        {

            foreach (Component component in components)
            {
                if (component.GetType() == componentType) return component;
            }
            return null;
        }
    }
}
