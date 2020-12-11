using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    /// <summary>
    /// System to render
    /// </summary>
    class RenderSystem: GameSystem
    {
        /// <summary>
        /// A shared black brush
        /// </summary>
    private static Brush blackBrush = new SolidBrush(Color.Black);

        /// <summary>
        /// A shared simple font
        /// </summary>
        private static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        public RenderSystem(GameEngine gameEngine)
        {
           
        }

        public void update(GameEngine gameEngine, Graphics graphics)
        {
            drawGameState(gameEngine, graphics);
            if (gameEngine.currentGameState == GameState.state.Play)
            {
                drawPlayerLife(gameEngine, graphics);
                drawEntities(gameEngine, graphics);
            }

        }


        /// <summary>
        /// Draw entities
        /// </summary>
        /// <param name="gameEngine">Current game</param>
        /// <param name="graphics">graphic object where to perform rendering</param>
        void drawEntities(GameEngine gameEngine, Graphics graphics)
        {
            HashSet<Entity> renderableEntities = gameEngine.entityManager.GetEntities(typeof(ImageComponent), typeof(PositionComponent));

            foreach (Entity entity in renderableEntities)
            {
                PositionComponent position = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
                ImageComponent image = (ImageComponent)entity.GetComponent(typeof(ImageComponent));

                if (position == null || image == null) return;

                graphics.DrawImage(image.Image, (float)position.PositionX, (float)position.PositionY, image.Image.Width, image.Image.Height);

            }
        }

        /// <summary>
        /// Draw the player's life
        /// </summary>
        /// <param name="gameEngine">Current game</param>
        /// <param name="graphics">graphic object where to perform rendering</param>
        void drawPlayerLife(GameEngine gameEngine, Graphics graphics)
        {
            int life = gameEngine.entityManager.getPlayerLife();
            graphics.DrawString("life : " + life.ToString(), defaultFont, blackBrush, 0, gameEngine.gameSize.Height - 24);
        }

        /// <summary>
        /// Draw certain state of the game
        /// </summary>
        /// <param name="gameEngine">Current game</param>
        /// <param name="graphics">graphic object where to perform rendering</param>
        void drawGameState(GameEngine gameEngine, Graphics graphics)
        {
            
            if (gameEngine.currentGameState == GameState.state.Pause)
            {
                graphics.DrawString("Pause", defaultFont, blackBrush, 0, 0);
                
            }
            if (gameEngine.currentGameState == GameState.state.Loose)
            {
                graphics.DrawString("Loose", defaultFont, blackBrush, 0, 0);
                
            }

            if (gameEngine.currentGameState == GameState.state.Win)
            {
                graphics.DrawString("Win", defaultFont, blackBrush, 0, 0);
                
            }
        }

        


    }
}
