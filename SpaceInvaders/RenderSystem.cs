﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
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
            /*HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> renderableEntities = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(ImageComponent)) != null)
                    renderableEntities.Add(entity);
            }
            this.Entities = renderableEntities;*/
        }

        public void update(GameEngine gameEngine, Graphics graphics)
        {
            if(drawGameState(gameEngine, graphics))
            {
                drawPlayerLife(gameEngine, graphics);
                drawEntities(gameEngine, graphics);
            }   
            
            

            
            {
                /*foreach (Entity entity in gameEngine.entityManager.GameObjects)
                    if (entity.GetComponent(typeof(EnemyBlockComponent)) != null)
                    {
                        // Create a new pen.
                        Pen skyBluePen = new Pen(Brushes.DeepSkyBlue);

                        // Set the pen's width.
                        skyBluePen.Width = 4.0F;

                        // Set the LineJoin property.
                        skyBluePen.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;
                        HitboxComponent enemyBlockHitbox = (HitboxComponent)entity.GetComponent(typeof(HitboxComponent));
                        PositionComponent enemyBlockPosition = (PositionComponent)entity.GetComponent(typeof(PositionComponent));

                        
                        // Draw a rectangle.
                        graphics.DrawRectangle(skyBluePen,
                            new Rectangle((int)enemyBlockPosition.PositionX, (int)enemyBlockPosition.PositionY, enemyBlockHitbox.Width, enemyBlockHitbox.Height));

                        //Dispose of the pen.
                        skyBluePen.Dispose();

                    }*/
            }
        }

        private void drawEntities(GameEngine gameEngine, Graphics graphics)
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

        void drawPlayerLife(GameEngine gameEngine, Graphics graphics)
        {
            int life = gameEngine.entityManager.getPlayerLife();
            graphics.DrawString("life : " + life.ToString(), defaultFont, blackBrush, 0, gameEngine.gameSize.Height - 24);
        }

        bool drawGameState(GameEngine gameEngine, Graphics graphics)
        {
            
            if (gameEngine.currentGameState == GameState.state.Pause)
            {
                graphics.DrawString("pause", defaultFont, blackBrush, 0, 0);
                return false;
            }


            if (gameEngine.currentGameState == GameState.state.Loose)
            {
                graphics.DrawString("Loose", defaultFont, blackBrush, 0, 0);
                return false;
            }

            if (gameEngine.currentGameState == GameState.state.Win)
            {
                graphics.DrawString("Win", defaultFont, blackBrush, 0, 0);
                return false;
            }
            return true;
        }

        


    }
}
