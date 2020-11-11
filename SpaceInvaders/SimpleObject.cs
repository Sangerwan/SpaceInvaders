using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    abstract class SimpleObject:GameObject
    {
        private double positionX;
        private double positionY;
        private int lives;
        private Bitmap image;

        public double PositionX { get => positionX; set => positionX = value; }
        public double PositionY { get => positionY; set => positionY = value; }
        public int Lives { get => lives; set => lives = value; }
        public Bitmap Image { get => image; set => image = value; }
        public Vecteur2D Position { get => new Vecteur2D(PositionX, PositionY); }

        protected SimpleObject(double positionX, double positionY, int lives, Bitmap image,Side side):base(side)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Lives = lives;
            this.Image = image;
        }

        public override void Collision(Missile m)
        {
            
            if (m.entitySide == this.entitySide) return;
            Console.WriteLine("X");
            double missilePositionX = m.PositionX;
            double missilePositionLX = m.Image.Width;
            double missilePositionY = m.PositionY;
            double missilePositionLY = m.Image.Height;
            if (!Disjoint(missilePositionX, missilePositionLX, missilePositionY, missilePositionLY))
            {
                for (int i = 0; i < m.Image.Height; i++)
                {
                    for (int j = 0; j < m.Image.Width; j++)
                    {
                        int y = (int)(missilePositionY + i - PositionY);
                        int x = (int)(missilePositionX + j - PositionX);
                        if (!(x < 0 || y < 0 || x >= Image.Width || y >= Image.Height))
                        {
                            if (m.Image.GetPixel(j, i) == Image.GetPixel(x, y))
                            {
                                OnCollision(m, x, y);
                                if (!m.IsAlive())
                                    return;
                            }
                        }
                    }    
                }
            }
        }
        protected abstract void OnCollision(Missile m, int x, int y);
        /// <summary>
        /// /Test if bunker is disjoint from parameters
        /// </summary>
        /// <param name="x2"></param>
        /// <param name="xl2"></param>
        /// <param name="y2"></param>
        /// <param name="ly2"></param>
        /// <returns></returns>
        private bool Disjoint(double x2, double xl2, double y2, double yl2)
        {
            return OnLeft(x2) || OnRight(x2, xl2) || Below(y2, yl2) || Above(y2);

        }
        private bool Above(double y2)
        {
            return y2 > PositionY + Image.Height;
        }

        private bool Below(double y2, double yl2)
        {
            return PositionY > y2 + yl2;
        }

        private bool OnRight(double x2, double xl2)
        {
            return PositionX > x2 + xl2;
        }
        /// <summary>
        /// Test if Bunker is on left
        /// </summary>
        /// <param name="x2"></param>
        /// <returns></returns>
        private bool OnLeft(double x2)
        {
            return x2 > PositionX + Image.Width;
        }
    }
}
