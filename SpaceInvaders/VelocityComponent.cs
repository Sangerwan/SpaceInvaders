using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// Component to hold the entity's velocity
    /// </summary>
    class VelocityComponent : Component
    {
        double velocityX;
        double velocityY;
        double angularVelocity;

        public VelocityComponent(double velocityX, double velocityY) : this(velocityX, velocityY, 0)
        {
        }

        public VelocityComponent(double velocityX, double velocityY, double angularVelocity)
        {
            this.VelocityX = velocityX;
            this.VelocityY = velocityY;
            this.AngularVelocity = angularVelocity;
        }

        public double VelocityX { get => velocityX; set => velocityX = value; }
        public double VelocityY { get => velocityY; set => velocityY = value; }
        public double AngularVelocity { get => angularVelocity; set => angularVelocity = value; }
    }
}
