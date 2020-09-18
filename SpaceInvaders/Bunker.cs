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
            graphics.DrawImage(image, (float)positionX, (float)positionY, image.Width, image.Height);
        }

        public override bool IsAlive()
        {
            return (lives != 0) ? true : false;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            
        }
        public override void Collision(Missile m)
        {

        }
    }
}
