using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{

    /// <summary>
    /// System to manage the inputs
    /// </summary>
    class InputSystem : GameSystem
    {

        /// <summary>
        /// State of the keyboard
        /// </summary>
        HashSet<Keys> keyPressed;

        public HashSet<Keys> KeyPressed { get => keyPressed; set => keyPressed = value; }

        public InputSystem(GameEngine gameEngine)
        {
            KeyPressed = new HashSet<Keys>();
        }


        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        void ReleaseKey(Keys key)
        {
            KeyPressed.Remove(key);
        }

        public override void update(GameEngine gameEngine, double deltaT)
        {

            updateGameState(gameEngine);

            HashSet<Entity> inputableEntities = gameEngine.entityManager.GetEntities(typeof(InputComponent));
            foreach (Entity entity in inputableEntities)// only 1 player atm
            {

                InputComponent inputComponent = (InputComponent)entity.GetComponent(typeof(InputComponent));
                VelocityComponent velocityComponent = (VelocityComponent)entity.GetComponent(typeof(VelocityComponent));

                if (inputComponent == null || velocityComponent == null) continue;

                velocityComponent.VelocityY = 0;
                velocityComponent.VelocityX = 0;
                foreach (Keys key in inputComponent.Input.Keys)
                {
                    if (KeyPressed.Contains(key))
                    {
                        inputComponent.Input[key](entity);
                    }
                }
            }
        }

        /// <summary>
        /// Update game state
        /// </summary>
        /// <param name="gameEngine"></param>
        void updateGameState(GameEngine gameEngine)
        {
            //pause
            if (KeyPressed.Contains(Keys.P))
            {
                if (gameEngine.currentGameState == GameState.state.Play)
                    gameEngine.currentGameState = GameState.state.Pause;
                else if (gameEngine.currentGameState == GameState.state.Pause)
                    gameEngine.currentGameState = GameState.state.Play;
                ReleaseKey(Keys.P);
            }

            //replay
            if (gameEngine.currentGameState == GameState.state.Win
                || gameEngine.currentGameState == GameState.state.Loose)
            {
                if (KeyPressed.Contains(Keys.Space))
                    gameEngine.Init();

            }
        }

    }
}
