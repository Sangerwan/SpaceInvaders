using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// Component for hitbox
    /// Collisions are checked with the entity's hitbox
    /// It can be smaller or bigger than the entity's image if it has one
    /// </summary>
    class HitboxComponent:Component
    {
        Size size;

        public HitboxComponent(int width, int heigth)
        {
        this.size = new Size(width, heigth);            
        }

        public HitboxComponent(Bitmap image)
        {
            this.size = image.Size;
        }

        public Size Size { get => size; set => size = value; }

        public int Height { get => size.Height; set => size.Height = value; }

        public int Width { get => size.Width; set => size.Width = value; }
    }
}
