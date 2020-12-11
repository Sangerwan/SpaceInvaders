using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;

namespace SpaceInvaders
{
    /// <summary>
    /// System to manage sound
    /// </summary>
    class SoundSystem: GameSystem
    {
        SoundPlayer backgroundSound;
        GameState.state lastState;

        /// <summary>
        /// Enable or diable sound
        /// </summary>
        bool playSound;
        
        public SoundSystem(GameEngine gameEngine, bool playSound = true)
        {
            this.PlaySound = playSound;
            backgroundSound = new SoundPlayer(SpaceInvaders.Properties.Resources.background);
            backgroundSound.LoadAsync();
            if (playSound)
            {
                backgroundSound.PlayLooping();
            }
        }

        public bool PlaySound { get => playSound; set => playSound = value; }

        public override void update(GameEngine gameEngine, double deltaT)
        {
            if(playSound)
                updateBackgroundSound(gameEngine);            
            lastState = gameEngine.currentGameState;
            #region test
            /*HashSet<Entity> soundEntities = gameEngine.entityManager.GetEntities(typeof(SoundComponent));
            foreach(Entity entity in soundEntities)
            {
                SoundComponent soundComponent = (SoundComponent)entity.GetComponent(typeof(SoundComponent));
                if(soundComponent != null)
                {
                    SoundPlayer sound = new SoundPlayer(soundComponent.SoundPath);
                    sound.LoadAsync();
                    sound.Play();
                    entity.removeComponent(typeof(SoundComponent));
                }
            }*/
            #endregion
        }

        /// <summary>
        /// Update background sound
        /// </summary>
        /// <param name="gameEngine"></param>
        void updateBackgroundSound(GameEngine gameEngine)
        {
       
            if (lastState != gameEngine.currentGameState)
            {
                if (gameEngine.currentGameState == GameState.state.Play)
                {
                    backgroundSound.PlayLooping();
                }
                if (gameEngine.currentGameState == GameState.state.Pause)
                {
                    backgroundSound.Stop();
                }
                if (gameEngine.currentGameState == GameState.state.Win)
                {
                    backgroundSound.Stop();
                }
                if (gameEngine.currentGameState == GameState.state.Loose)
                {
                    backgroundSound.Stop();
                }
            }
        }
    }
}
