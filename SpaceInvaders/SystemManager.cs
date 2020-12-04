using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// Class to manage systems
    /// </summary>
    class SystemManager
    {
        /// <summary>
        /// List of systems
        /// </summary>
        List<GameSystem> systems;

        /// <summary>
        /// Simple access for the system list
        /// </summary>
        public List<GameSystem> Systems { get => systems;}

        /// <summary>
        /// Simple Constructor
        /// </summary>
        /// <param name="gameEngine">Current game</param>
        public SystemManager(GameEngine gameEngine)
        {
            this.systems = new List<GameSystem>();
            InitSystem(gameEngine);
        }

        /// <summary>
        /// Initialize every system
        /// </summary>
        /// <param name="gameEngine">Current game</param>
        void InitSystem(GameEngine gameEngine)
        {
            InputSystem inputSystem = new InputSystem(gameEngine);
            EnemyShootSystem enemyShootSystem = new EnemyShootSystem(gameEngine);
            MoveSystem moveSystem = new MoveSystem(gameEngine);
            CollisionSystem collisionSystem = new CollisionSystem(gameEngine);
            RenderSystem renderSystem = new RenderSystem(gameEngine);
            DeathSystem deathSystem = new DeathSystem(gameEngine);
            WinLooseSystem winLooseSystem = new WinLooseSystem(gameEngine);

            //order is important
            addSystem(inputSystem, enemyShootSystem, moveSystem, collisionSystem, deathSystem, winLooseSystem, renderSystem);

        }

        /// <summary>
        /// Add the systems to the list of systems
        /// </summary>
        /// <param name="systemList">List of systems</param>
        void addSystem(params GameSystem[] systemList)
        {
            foreach (GameSystem system in systemList)
            {
                systems.Add(system);
            }

        }

        /// <summary>
        /// Update each system
        /// </summary>
        /// <param name="gameEngine">Current game</param>
        /// <param name="deltaT">Time elapsed since last update</param>
        public void update(GameEngine gameEngine,double deltaT)
        {
            systems[0].update(gameEngine, deltaT);// input system
            if (gameEngine.currentGameState == GameState.state.Play)
            {
                for(int i = 0; i < systems.Count-1; i++)
                {
                    systems[i].update(gameEngine, deltaT);
                }
            }
        }

        /// <summary>
        /// Simple getter
        /// </summary>
        /// <param name="systemType">Type of system to get</param>
        /// <returns>Return the system if found, else null</returns>
        public GameSystem GetSystem(Type systemType)
        {

            foreach (GameSystem system in systems)
            {
                if (system.GetType() == systemType) return system;
            }
            return null;
        }
    }
}
