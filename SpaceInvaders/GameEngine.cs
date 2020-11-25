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
        /// <summary>
        /// Set of all game objects currently in the game
        /// </summary>
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        private HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        
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
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }


        /*/// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
            if (state == SpaceInvaders.GameState.Pause)
                g.DrawString("pause", defaultFont, blackBrush, 0, 0);

            renderSystem = systemManager.get
            systemManager.update(this, g);
            *//*foreach (GameObject gameObject in gameObjects)
                gameObject.Draw(this, g);       *//*
        }*/
        

        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            Console.WriteLine(gameState);
            systemManager.update(this, deltaT);
            return;
            // add new game objects
            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();

            if (keyPressed.Contains(Keys.P))
            {                
                if (gameState == GameState.state.Play)
                    gameState = SpaceInvaders.GameState.state.Pause;
                else if (gameState == SpaceInvaders.GameState.state.Pause)
                    gameState = SpaceInvaders.GameState.state.Play;
                ReleaseKey(Keys.P);
            }
            if (gameState == SpaceInvaders.GameState.state.Pause)
            {
                //pause
                return;
            }

            

            // update each game object
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(this, deltaT);
            }

            // remove dead objects
            gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
        }
        #endregion
    }
}
