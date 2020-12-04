using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// Component for the entity's position
    /// </summary>
    class PositionComponent : Component
    {
        double positionX;
        double positionY;

        public PositionComponent(double positionX, double positionY)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
        }

        public double PositionX { get => positionX; set => positionX = value; }
        public double PositionY { get => positionY; set => positionY = value; }
    }
}
