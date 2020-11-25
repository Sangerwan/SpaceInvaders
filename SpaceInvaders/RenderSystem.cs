using System;
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
            if (gameEngine.currentGameState == GameState.state.Pause)
                graphics.DrawString("pause", defaultFont, blackBrush, 0, 0);

            HashSet<Entity> renderableEntities = getEntities(gameEngine);

            foreach (Entity entity in renderableEntities)
            {
                PositionComponent position = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
                ImageComponent image = (ImageComponent)entity.GetComponent(typeof(ImageComponent));

                if (position == null || image == null) return;

                graphics.DrawImage(image.Image, (float)position.PositionX, (float)position.PositionY, image.Image.Width, image.Image.Height);
            }
        }

        protected override HashSet<Entity> getEntities(GameEngine gameEngine)
        {
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> renderableEntities = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(ImageComponent)) != null)
                    renderableEntities.Add(entity);
            }
            return renderableEntities;
        }
        public override void update(GameEngine gameEngine, double deltaT)
        {
            
        }
    }
}
