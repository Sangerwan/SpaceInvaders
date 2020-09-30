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
            this.enemyShips = new HashSet<SpaceShip>();
            this.baseWidth = baseWidth;
            this.Position = position;
        }


        public void AddLine(int nbShips, int nbLives, Bitmap shipImage)
        {
            //playerShip = new PlayerSpaceShip(0, gameSize.Height - 50, 3, SpaceInvaders.Properties.Resources.ship3);

            for(int i = 0; i < nbShips; i++)
            {
                enemyShips.Add(new SpaceShip(((double)i / nbShips) * baseWidth+ (baseWidth/nbShips-shipImage.Width)/2, 0, 1, shipImage));
            }
            Console.WriteLine(enemyShips.Count);
        }

        void UpdateSize()
        {

        }

        public override void Update(Game gameInstance, double deltaT)
        {
            foreach (SpaceShip spaceShip in enemyShips)
            {
                spaceShip.Update(gameInstance, deltaT);
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
           
        }
    }
}
