using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class InputSystem: GameSystem
    {
        /// <summary>
        /// State of the keyboard
        /// </summary>
        HashSet<Keys> keyPressed = new HashSet<Keys>();
    }
}
