using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        public static Vecteur2D operator+(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.x + v2.x, v1.y + v2.y);
        }
        public static Vecteur2D operator-(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.x - v2.x, v1.y - v2.y);
        }
        public static Vecteur2D operator-(Vecteur2D v1)
        {
            return new Vecteur2D(-v1.x, -v1.y);
        }
        public static Vecteur2D operator*(double k, Vecteur2D v1)
        {
            return new Vecteur2D(v1.x * k, v1.y * k);
        }
        public static Vecteur2D operator *(Vecteur2D v1, double k)
        {
            return v1*k;
        }
        public static Vecteur2D operator /(Vecteur2D v1, double k)
        {
            return new Vecteur2D(v1.x/k, v1.y/k);
        }
    }
}
