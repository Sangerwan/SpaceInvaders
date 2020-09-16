using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Missile : GameObject
    {
        private double positionX;
        private double positionY;
        private int lives;
        private Bitmap image;
        private double vitesse = 1000;

        public Missile(double positionX, double positionY, int lives, Bitmap image)
        {
            this.positionX = positionX-image.Width/2;
            this.positionY = positionY-image.Height;
            this.lives = lives;
            this.image = image;
        }


        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(image, (float) positionX, (float) positionY, image.Width, image.Height);
        }

        public override bool IsAlive()
        {
            return (lives != 0) ? true : false;
        }

        public override void Update(Game gameInstance, double deltaT)
        {   
                positionY -= vitesse * deltaT;
                if (positionY < 0)
                    lives = 0;          
        }

        
    }
}
