using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class Bunker : SimpleObject
    {
        public Bunker(double positionX, double positionY, int lives, Bitmap image) : base(positionX, positionY, lives, image)
        { }


        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(Image, (float)PositionX, (float)PositionY, Image.Width, Image.Height);
        }

        public override bool IsAlive()
        {
            return (Lives != 0) ? true : false;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            
        }
        public override void Collision(Missile m)
        {
            double missilePositionX = m.PositionX;
            double missilePositionLX = m.PositionX + m.Image.Width;
            double missilePositionY = m.PositionY;
            double missilePositionLY = m.PositionY + m.Image.Height;
            if (!Disjoint())
            {

            }
        }
        /// <summary>
        /// /Test if bunker is disjoint from parameters
        /// </summary>
        /// <param name="x2"></param>
        /// <param name="xl2"></param>
        /// <param name="y2"></param>
        /// <param name="ly2"></param>
        /// <returns></returns>
        private bool Disjoint(double x2, double xl2, double y2, double ly2)
        {
            return OnLeft(x2) || OnRight(x2,xl2) || OnTop(y2,yl2) || OnBottom(y2)
        }


    }
}
