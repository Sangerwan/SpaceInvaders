using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Missile : SimpleObject
    {
        
        private double vitesse = 150;

        

        public Missile(double positionX, double positionY, int lives, Bitmap image):base(positionX - image.Width / 2, positionY - image.Height, lives,image)
        {
            PositionX = positionX;
            PositionY = positionY;
            Lives = lives;
            Image = image;
        }



        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(Image, (float) PositionX, (float) PositionY, Image.Width, Image.Height);
        }

        public override bool IsAlive()
        {
            return (Lives != 0) ? true : false;
        }

        public override void Update(Game gameInstance, double deltaT)
        {   
            PositionY -= vitesse * deltaT;
            if (PositionY < 0)
                Lives = 0;
            foreach (GameObject gameObject in gameInstance.gameObjects)
            {
                gameObject.Collision(this);
            }
        }
        public override void Collision(Missile m)
        {
            
        }

    }
}
