using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    /// <summary>
    /// Component for an image
    /// </summary>
    class ImageComponent:Component
    {
        Bitmap image;
        public ImageComponent(Bitmap image)
        {
            this.Image = image;
        }

        public Bitmap Image { get => image; set => image = value; }
    }
}
