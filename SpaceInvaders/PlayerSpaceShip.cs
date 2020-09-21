using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{

    class PlayerSpaceShip : SpaceShip
    {
        public PlayerSpaceShip(double positionX, double positionY, int lives, Bitmap image) : base(positionX, positionY, lives, image)
        {
        }
        public override void Update(Game gameInstance, double deltaT)
        {
            if (gameInstance.keyPressed.Contains(Keys.Right))
            {
                PositionX += speedPixelPerSecond * deltaT;
                if (PositionX + Image.Width > gameInstance.gameSize.Width)
                    PositionX -= speedPixelPerSecond * deltaT;
            }
            if (gameInstance.keyPressed.Contains(Keys.Left))
            {
                PositionX -= speedPixelPerSecond * deltaT;
                if (PositionX < 0)
                    PositionX += speedPixelPerSecond * deltaT;
            }
            if (gameInstance.keyPressed.Contains(Keys.Up))
            {
                PositionY -= speedPixelPerSecond * deltaT;
                if (PositionY < 0)
                    PositionY += speedPixelPerSecond * deltaT;
            }
            if (gameInstance.keyPressed.Contains(Keys.Down))
            {
                PositionY += speedPixelPerSecond * deltaT;
                if (PositionY + Image.Width > gameInstance.gameSize.Height)
                    PositionY -= speedPixelPerSecond * deltaT;
            }
            if (gameInstance.keyPressed.Contains(Keys.Space))
            {
                shoot(gameInstance);
            }

        }
    }
}
