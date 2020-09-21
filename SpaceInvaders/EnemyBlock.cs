using System;
using System.Collections.Generic;
using System.Drawing;
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

        private Size Size { get => size; set => size = value; }
        private Vecteur2D Position { get => position; set => position = value; }



        public EnemyBlock(int baseWidth, Vecteur2D position)
        {
            this.baseWidth = baseWidth;
            this.Position = position;
        }


        void AddLine(int nbShips, int nbLives, Bitmap shipImage)
        {
            //playerShip = new PlayerSpaceShip(0, gameSize.Height - 50, 3, SpaceInvaders.Properties.Resources.ship3);

            for(int i = 0; i < nbShips; i++)
            {
                enemyShips.Add(new SpaceShip(i / nbShips * size.Width, 0, 0, SpaceInvaders.Properties.Resources.ship3));
            }
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
