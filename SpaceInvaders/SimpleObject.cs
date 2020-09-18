using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    abstract class SimpleObject:GameObject
    {
        private double positionX;
        private double positionY;
        private int lives;
        private Bitmap image;

        public double PositionX { get => positionX; set => positionX = value; }
        public double PositionY { get => positionY; set => positionY = value; }
        public int Lives { get => lives; set => lives = value; }
        public Bitmap Image { get => image; set => image = value; }
        public Vecteur2D Position { get => new Vecteur2D(PositionX, PositionY); }

        protected SimpleObject(double positionX, double positionY, int lives, Bitmap image)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Lives = lives;
            this.Image = image;
        }
    }
}
