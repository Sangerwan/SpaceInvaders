using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    abstract class SimpleObject:GameObject
    {
        protected double positionX;
        protected double positionY;
        protected int lives;
        protected Bitmap image;

        protected SimpleObject(double positionX, double positionY, int lives, Bitmap image)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.lives = lives;
            this.image = image;
        }
    }
}
