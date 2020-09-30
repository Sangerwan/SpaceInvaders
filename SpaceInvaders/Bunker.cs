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
        public override void Collision(Missile m)//mb change to GO
        {
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
                        if (!(x < 0 || y < 0 || x >= Image.Width || y >= Image.Height)) //test if out of bonds 
                        {
                            if (m.Image.GetPixel(j, i) == Image.GetPixel(x, y))
                            {
                                Image.SetPixel(x, y, Color.FromArgb(0, 255, 255, 255));
                                m.Lives--;
                                if (!m.IsAlive())
                                    return;
                            }

                            Console.WriteLine(" i:" + i + "x" + (int)(missilePositionX + j - PositionX) + " j: " + j + "y:" + (int)(missilePositionY + i - PositionY));
                        }
                    }
                }

                //Image.SetPixel(0, 0, Color.FromArgb(255, 0, 0, 0)); //noir
                //
                //Image = SpaceInvaders.Properties.Resources.shoot3;
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
