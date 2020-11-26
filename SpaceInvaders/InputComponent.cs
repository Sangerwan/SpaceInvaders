using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace SpaceInvaders
{
    class InputComponent:Component
    {
        Dictionary<Keys, Action<Entity>> input;



        public InputComponent()
        {
            this.input= new Dictionary<Keys, Action<Entity>>();
        }
        public Dictionary<Keys, Action<Entity>> Input { get => input; set => input = value; }
    }
}
