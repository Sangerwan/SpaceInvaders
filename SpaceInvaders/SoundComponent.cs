using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// Component for sound
    /// </summary>
    class SoundComponent: Component
    {
        Stream soundPath;

        public SoundComponent(Stream soundPath)
        {
            this.SoundPath = soundPath;
        }

        public Stream SoundPath { get => soundPath; private set => soundPath = value; }
    }
}
