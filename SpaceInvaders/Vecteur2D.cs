using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Vecteur2D
    {
        private double x;
        private double y;
        public Vecteur2D(double x = 0, double y = 0)
        {
            this.x = x;
            this.y = y;
        }
        public double Norme
        {
            get
            {
                return Math.Sqrt(x * x + y * y);
            }
            set
            {

            }
        }
    }
}
