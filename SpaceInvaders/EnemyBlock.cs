using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class EnemyBlock:GameObject
    {
        private HashSet<SpaceShip> enemyShips;
        int baseWidth;
        private Size size;
        private Vecteur2D position;
        private double speedPixelPerSecond;
        private bool tmp;
        private Size Size { get => size; set => size = value; }
        private Vecteur2D Position { get => position; set => position = value;}
        private double randomShootProbability;


        public EnemyBlock(int baseWidth, Vecteur2D position):base(Side.Enemy)
        {
            this.enemyShips = new HashSet<SpaceShip>();
            this.baseWidth = baseWidth;
            this.Position = position;
            this.size.Width = baseWidth;
            this.size.Height = 0;
            this.speedPixelPerSecond = 0.1;
            this.randomShootProbability = 10;
            tmp = true;
        }


        public void AddLine(int nbShips, int nbLives, Bitmap shipImage)
        {
            //playerShip = new PlayerSpaceShip(0, gameSize.Height - 50, 3, SpaceInvaders.Properties.Resources.ship3);

            for(int i = 0; i < nbShips; i++)
            {
                enemyShips.Add(new SpaceShip(((double)i / nbShips) * baseWidth+ (baseWidth/nbShips-shipImage.Width)/2, size.Height, nbLives, shipImage,Side.Enemy));
            }
            size.Height += shipImage.Height+5;
            
            
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            Random rnd = new Random();
            double r = rnd.NextDouble();
            
            if(r <= randomShootProbability * deltaT)
            {
                int randomShipIndex = rnd.Next(0, enemyShips.Count);
                enemyShips.ElementAt(randomShipIndex).shoot(gameInstance);
            }
            double Yshift=0;
            if (speedPixelPerSecond > 0)
            {
                if (Position.X + size.Width + speedPixelPerSecond * deltaT > gameInstance.gameSize.Width)
                {
                    Yshift = 20;
                    if (Math.Abs(speedPixelPerSecond) < 20)
                    {
                        speedPixelPerSecond *= -1.1;
                    }
                    else
                    {
                        speedPixelPerSecond *= -1;
                    }
                        
                }
            }
            else
            {
                if (Position.X + speedPixelPerSecond * deltaT < 0)
                {
                    Yshift = 20;
                    if(Math.Abs(speedPixelPerSecond)<20)
                    {
                        speedPixelPerSecond *= -1.1;
                    }
                    else
                    {
                        speedPixelPerSecond *= -1;
                    }

                }
            }
            
            if (tmp)
            {
                if(Position.Y> gameInstance.gameSize.Height)
                {
                    tmp = false;
                    Yshift*=-1;
                }
            }
            else
            {
                if (Position.Y < 0)
                {
                    tmp = true;
                }
                else
                {
                    Yshift*=-1;
                }
            }
            Position.X += speedPixelPerSecond;
            Position.Y += Yshift;
            enemyShips.RemoveWhere(enemyShips => !enemyShips.IsAlive());
            foreach (SpaceShip spaceShip in enemyShips)
            {
                spaceShip.PositionY += Yshift;
                spaceShip.Update(gameInstance, speedPixelPerSecond);
            }
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            foreach (SpaceShip spaceShip in enemyShips)
            {
                spaceShip.Draw(gameInstance, graphics);
            }
        }

        public override bool IsAlive()
        {
            
            foreach (SpaceShip spaceShip in enemyShips)
            {
                if (spaceShip.IsAlive())
                    return true;
            }
            return false;
        }

        public override void Collision(Missile m)
        {
            foreach (SpaceShip spaceShip in enemyShips)
            {
                spaceShip.Collision(m);
            }
        }
    }
}
