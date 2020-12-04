using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceInvaders
{
    /// <summary>
    /// Class that connects the elements of the game together
    /// </summary>
    class GameEngine
    {       

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;


        /// <summary>
        /// State of the game
        /// </summary>
        GameState.state gameState;

        
        /// <summary>
        /// Entity manager
        /// </summary>
        public EntityManager entityManager;

        /// <summary>
        /// System manager
        /// </summary>
        public SystemManager systemManager;

        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static GameEngine game { get; private set; }

        /// <summary>
        /// Property for game state
        /// </summary>
        public GameState.state currentGameState { get => gameState; set => gameState = value; }

        #endregion


        #region constructors
        /// <summary>
        /// Create and initialize the game
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// <returns>The created game</returns>
        public static GameEngine CreateGame(Size gameSize)
        {
            if (game == null)
                game = new GameEngine(gameSize);
            game.Init();
            return game;
        }

        /// <summary>
        /// Initialize the game
        /// </summary>
        public void Init()
        {
            gameState = GameState.state.Play;
            entityManager = new EntityManager(this);
            systemManager = new SystemManager(this);
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private GameEngine(Size gameSize)
        {            
            this.gameSize = gameSize;
        }

        #endregion

        #region methods
        /// <summary>
        /// Update the game
        /// </summary>
        public void Update(double deltaT)
        {
            systemManager.update(this, deltaT);                      
        }
        #endregion
    }
}
