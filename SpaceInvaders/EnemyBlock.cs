using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class EnemyBlock:GameObject
    {
        private HashSet<SpaceShip> enemyShips;
        int baseWidth;
        Size size;
        Vecteur2D position;

        private Size Size1 { get => size; set => size = value; }
        internal Vecteur2D Position { get => position; set => position = value; }

        struct Size{
            int hauteur;
            int largeur;
        }

        public EnemyBlock(int baseWidth, Vecteur2D position)
        {
            this.baseWidth = baseWidth;
            this.Position = position;
        }


        void AddLine(int nbShips, int nbLives, Bitmap shipImage)
        {

        }

        void UpdateSize()
        {

        }

        public override void Update(Game gameInstance, double deltaT)
        {
            throw new NotImplementedException();
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            throw new NotImplementedException();
        }

        public override bool IsAlive()
        {
            throw new NotImplementedException();
        }

        public override void Collision(Missile m)
        {
            throw new NotImplementedException();
        }
    }
}
