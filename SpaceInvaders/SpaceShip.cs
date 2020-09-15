using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class SpaceShip : GameObject
    {
        private float positionX;
        private float positionY;
        private int lives;
        private Bitmap image = SpaceInvaders.Properties.Resources.ship3;
        private double speedPixelPerSecond;

        public SpaceShip(float positionX, float positionY, int lives, Bitmap image)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.Lives = lives;
            this.Image = image;
        }

        public Vecteur2D Position { get => new Vecteur2D(positionX, positionY); }        
        public int Lives { get => lives; set => lives = value; }
        public Bitmap Image { get => image; set => image = value; }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            throw new NotImplementedException();
        }

        public override bool IsAlive()
        {
            throw new NotImplementedException();
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            throw new NotImplementedException();
        }
    }
}
