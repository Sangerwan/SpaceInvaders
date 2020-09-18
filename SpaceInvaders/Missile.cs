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
