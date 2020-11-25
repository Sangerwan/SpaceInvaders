using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class MoveSystem: GameSystem
    {
        public MoveSystem(GameEngine gameEngine)
        {
            /*HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> movableEntities = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(VelocityComponent)) != null)
                    movableEntities.Add(entity);
            }
            this.Entities = movableEntities;*/
        }

        public override void update(GameEngine gameEngine, double deltaT)
        {
            HashSet<Entity> movableEntities = getEntities(gameEngine);
            foreach (Entity entity in movableEntities)
            {
                PositionComponent position = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
                VelocityComponent velocity = (VelocityComponent)entity.GetComponent(typeof(VelocityComponent));

                position.PositionX += velocity.VelocityX * deltaT;
                position.PositionY += velocity.VelocityY * deltaT;
                if (position.PositionY < 0) 
                    gameEngine.entityManager.GameObjects.Remove(entity);
            }
        }
        protected override HashSet<Entity> getEntities(GameEngine gameEngine)
        {
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> movableEntities = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(VelocityComponent)) != null)
                    movableEntities.Add(entity);
            }
            return movableEntities;
        }
    }
}
