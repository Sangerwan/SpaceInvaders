using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class SpaceShip : SimpleObject
    {

        protected double speedPixelPerSecond = 100;
        protected Missile missile;
        public SpaceShip(double positionX, double positionY, int lives, Bitmap image,Side side) : base( positionX,  positionY,  lives,  image,side)
        {

        }
    

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(base.Image, (float)PositionX, (float)PositionY, base.Image.Width, base.Image.Height);
        }

        public override bool IsAlive()
        {
            return (base.Lives > 0) ? true : false;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            PositionX += deltaT;
        }

        public void shoot(Game gameInstance)
        {
            if (missile == null || !missile.IsAlive())
            {
                missile = new Missile(PositionX + Image.Width / 2, PositionY, 5, Properties.Resources.shoot1,this.entitySide);
                gameInstance.AddNewGameObject(missile);
            }
        }
        protected override void OnCollision(Missile m, int x, int y)
        {
            this.Lives--;
            m.Lives--;
        }
    }
}
