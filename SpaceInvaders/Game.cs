using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class Game
    {
        
        
        #region GameObjects management
        /// <summary>
        /// Set of all game objects currently in the game
        /// </summary>
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        private HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            pendingNewGameObjects.Add(gameObject);
        }
        #endregion

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;
        public PlayerSpaceShip playerShip;
        public Bunker bunker1;
        public Bunker bunker2;
        public Bunker bunker3;
        public EnemyBlock enemyBlock;
        enum GameState { Play, Pause};
        GameState state;
        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

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
        
        public static Game CreateGame(Size gameSize)
        {
            
            if (game == null)
                game = new Game(gameSize);
            return game;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {
            state = GameState.Play;
            playerShip = new PlayerSpaceShip(0, gameSize.Height - 50, 3, SpaceInvaders.Properties.Resources.ship3);
            AddNewGameObject(playerShip);
            bunker1 = new Bunker(50, gameSize.Height - 150, 1, SpaceInvaders.Properties.Resources.bunker);
            AddNewGameObject(bunker1);
            bunker2 = new Bunker(gameSize.Width/2 -50, gameSize.Height - 150, 1, SpaceInvaders.Properties.Resources.bunker);
            AddNewGameObject(bunker2);
            bunker3 = new Bunker(gameSize.Width-150, gameSize.Height - 150, 1, SpaceInvaders.Properties.Resources.bunker);
            AddNewGameObject(bunker3);

            enemyBlock = new EnemyBlock(gameSize.Width/2, new Vecteur2D(0, 0));
            enemyBlock.AddLine(5, 20, SpaceInvaders.Properties.Resources.ship1);
            enemyBlock.AddLine(4, 20, SpaceInvaders.Properties.Resources.ship2);
            enemyBlock.AddLine(3, 20, SpaceInvaders.Properties.Resources.ship3);
            enemyBlock.AddLine(2, 20, SpaceInvaders.Properties.Resources.ship4);
            enemyBlock.AddLine(1, 20, SpaceInvaders.Properties.Resources.ship5);
            AddNewGameObject(enemyBlock);
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


        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
            if (state == GameState.Pause)
                g.DrawString("pause", defaultFont, blackBrush,0,0);
            foreach (GameObject gameObject in gameObjects)
                gameObject.Draw(this, g);       
        }


        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            if (keyPressed.Contains(Keys.P))
            {
                if (state == GameState.Play)
                    state = GameState.Pause;
                else if (state == GameState.Pause)
                    state = GameState.Play;
                ReleaseKey(Keys.P);
                /*// create new BalleQuiTombe
                GameObject newObject = new BalleQuiTombe(gameSize.Width / 2, 0);
                
                // add it to the game
                AddNewGameObject(newObject);
                
                // release key space (no autofire)*/
                
            }
            if (state == GameState.Pause)
            {
                //pause
                return;
            }
            

            // add new game objects
            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();


            // if space is pressed
            

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
