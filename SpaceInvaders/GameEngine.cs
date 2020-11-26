using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class GameEngine
    {       

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;


        
        GameState.state gameState;

        

        public EntityManager entityManager;

        public SystemManager systemManager;

        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static GameEngine game { get; private set; }
        public GameState.state currentGameState { get => gameState; set => gameState = value; }

        /// <summary>
        /// A shared black brush
        /// </summary>
        private static Brush blackBrush = new SolidBrush(Color.Black);

        /// <summary>
        /// A shared simple font
        /// </summary>
        private static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        #endregion


        #region constructors
        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// 
        /// <returns></returns>
        public static GameEngine CreateGame(Size gameSize)
        {
            if (game == null)
                game = new GameEngine(gameSize);
            game.Init();
            return game;
        }
        /// <summary>
        /// Create all Entity for the game
        /// </summary>
        public void Init()
        {
            gameState = GameState.state.Play;
            entityManager = new EntityManager(this);
            //entityManager.Init(this);
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
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            Console.WriteLine(gameState);
            systemManager.update(this, deltaT);
            
        }
        #endregion
    }
}
