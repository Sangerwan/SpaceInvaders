using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Missile : SimpleObject
    {
        
        private double vitesse;

        

        public Missile(double positionX, double positionY, int lives, Bitmap image,Side side) : base(positionX - image.Width / 2, positionY - image.Height, lives,image,side)
        {
            this.vitesse = 150;
            if (side == Side.Enemy)
            {
                this.vitesse = -150;
            }
            
        }



        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(Image, (float) PositionX, (float) PositionY, Image.Width, Image.Height);
        }

        public override bool IsAlive()
        {
            return (Lives > 0) ? true : false;
        }

        public override void Update(Game gameInstance, double deltaT)
        {   
            PositionY -= vitesse * deltaT;
            if(vitesse > 0)
                if (PositionY < 0)
                    Lives = 0;
            if (vitesse < 0)
                if (PositionY > gameInstance.gameSize.Height)
                    Lives = 0;
            foreach (GameObject gameObject in gameInstance.gameObjects)
            {
                if (gameObject == this) continue;
                gameObject.Collision(this);
            }
        }
        protected override void OnCollision(Missile m, int x, int y)
        {
            this.Lives = 0;
            m.Lives = 0;
        }
    }
}
