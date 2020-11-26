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
        HashSet<Keys> keyPressed;

        public HashSet<Keys> KeyPressed { get => keyPressed; set => keyPressed = value; }

        public InputSystem(GameEngine gameEngine)
        {
            KeyPressed = new HashSet<Keys>();

            /*HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> inputableEntities = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(InputComponent)) != null)
                    inputableEntities.Add(entity);
            }
            this.Entities = inputableEntities;*/
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

        public override void update(GameEngine gameEngine,double deltaT)
        {
            if (KeyPressed.Contains(Keys.P))
            {
                if (gameEngine.currentGameState == GameState.state.Play)
                    gameEngine.currentGameState = GameState.state.Pause;
                else if (gameEngine.currentGameState == GameState.state.Pause)
                    gameEngine.currentGameState = GameState.state.Play;
                ReleaseKey(Keys.P);
            }

            if(gameEngine.currentGameState == GameState.state.Win
                || gameEngine.currentGameState == GameState.state.Loose)
            {
                if(KeyPressed.Contains(Keys.Space))
                    gameEngine.Init();
                
            }
            HashSet<Entity> inputableEntities = getEntities(gameEngine);
            foreach (Entity entity in inputableEntities)// only 1 player atm
            {
                
                InputComponent input = (InputComponent)entity.GetComponent(typeof(InputComponent));
                VelocityComponent velocity = (VelocityComponent)entity.GetComponent(typeof(VelocityComponent));

                velocity.VelocityY = 0;
                velocity.VelocityX = 0;
                foreach (Keys key in input.Input.Keys)
                {
                    if (KeyPressed.Contains(key))
                    {
                        input.Input[key](entity);
                    }
                }
/*
                VelocityComponent velocity = (VelocityComponent)entity.GetComponent(typeof(VelocityComponent));

                velocity.VelocityY = 0;
                velocity.VelocityX = 0;
                if (KeyPressed.Contains(Keys.Up)) Console.WriteLine("up");
                if (KeyPressed.Contains(Keys.Down)) Console.WriteLine("down");
                if (KeyPressed.Contains(Keys.Left)) Console.WriteLine("left");
                if (KeyPressed.Contains(Keys.Right)) Console.WriteLine("right");


                if (KeyPressed.Contains(Keys.Up)) velocity.VelocityY -= 100;
                if (KeyPressed.Contains(Keys.Down)) velocity.VelocityY += 100;
                if (KeyPressed.Contains(Keys.Left)) velocity.VelocityX -= 100;
                if (KeyPressed.Contains(Keys.Right)) velocity.VelocityX += 100;

                if (KeyPressed.Contains(Keys.Space)&& entity.GetComponent(typeof(CanShootComponent)) != null)
                {
                    gameEngine.entityManager.createMissile(entity);
                    //entity.removeComponent(typeof(CanShootComponent));
                }*/
            }

        }
        protected override HashSet<Entity> getEntities(GameEngine gameEngine)
        {
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> inputableEntities = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(InputComponent)) != null)
                    inputableEntities.Add(entity);
            }
            return inputableEntities;
        }
    }
}
