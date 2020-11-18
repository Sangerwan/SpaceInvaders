using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
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
    }
}
