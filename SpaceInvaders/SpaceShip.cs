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

        private double speedPixelPerSecond = 100;
        private Missile missile;
        public SpaceShip(double positionX, double positionY, int lives, Bitmap image) : base( positionX,  positionY,  lives,  image)
        {

        }
    

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(base.Image, (float)PositionX, (float)PositionY, base.Image.Width, base.Image.Height);
        }

        public override bool IsAlive()
        {
            return (base.Lives != 0) ? true : false;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            if (gameInstance.keyPressed.Contains(Keys.Right))
            {
                PositionX += speedPixelPerSecond * deltaT;
                if (PositionX + base.Image.Width > gameInstance.gameSize.Width)
                    PositionX -= speedPixelPerSecond * deltaT;
            }
            if (gameInstance.keyPressed.Contains(Keys.Left))
            {
                PositionX -= speedPixelPerSecond * deltaT;
                if (PositionX < 0)
                    PositionX += speedPixelPerSecond * deltaT;
            }
            if (gameInstance.keyPressed.Contains(Keys.Space))
            {
                shoot(gameInstance);
            }
        }

        public void shoot(Game gameInstance)
        {
            if (missile == null || !missile.IsAlive())
            {
                missile = new Missile(PositionX + base.Image.Width / 2, PositionY, 1, Properties.Resources.shoot1);
                gameInstance.AddNewGameObject(missile);
            }
        }
        public override void Collision(Missile m)
        {

        }
    }
}
