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
            this.X = x;
            this.Y = y;
        }
        public double Norme
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y);
            }
            set
            {

            }
        }

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }

        public static Vecteur2D operator+(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.X + v2.X, v1.Y + v2.Y);
        }
        public static Vecteur2D operator-(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.X - v2.X, v1.Y - v2.Y);
        }
        public static Vecteur2D operator-(Vecteur2D v1)
        {
            return new Vecteur2D(-v1.X, -v1.Y);
        }
        public static Vecteur2D operator*(double k, Vecteur2D v1)
        {
            return new Vecteur2D(v1.X * k, v1.Y * k);
        }
        public static Vecteur2D operator *(Vecteur2D v1, double k)
        {
            return v1*k;
        }
        public static Vecteur2D operator /(Vecteur2D v1, double k)
        {
            return new Vecteur2D(v1.X/k, v1.Y/k);
        }
    }
}
