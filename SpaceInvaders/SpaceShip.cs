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
        /*private double positionX;
        private double positionY;
        private int lives;
        private Bitmap image;*/
        private double speedPixelPerSecond = 100;
        private Missile missile;
        public SpaceShip(double positionX, double positionY, int lives, Bitmap image) : base( positionX,  positionY,  lives,  image)
        {

        }
    

        public Vecteur2D Position { get => new Vecteur2D(positionX, positionY); }
        public int Lives { get => lives; set => lives = value; }
        public Bitmap Image { get => image; set => image = value; }

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
            if (gameInstance.keyPressed.Contains(Keys.Right))
            {
                positionX += speedPixelPerSecond * deltaT;
                if (positionX + image.Width > gameInstance.gameSize.Width)
                    positionX -= speedPixelPerSecond * deltaT;
            }
            if (gameInstance.keyPressed.Contains(Keys.Left))
            {
                positionX -= speedPixelPerSecond * deltaT;
                if (positionX < 0)
                    positionX += speedPixelPerSecond * deltaT;
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
                missile = new Missile(positionX + image.Width / 2, positionY, 1, SpaceInvaders.Properties.Resources.shoot1);
                gameInstance.AddNewGameObject(missile);
            }
        }
        public override void Collision(Missile m)
        {

        }
    }
}
