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

                HitboxComponent hitbox = (HitboxComponent)entity.GetComponent(typeof(HitboxComponent));

                if (position.PositionY < 0 || position.PositionY + hitbox.Size.Height > gameEngine.gameSize.Height||
                    position.PositionX < 0 || position.PositionX+hitbox.Size.Width > gameEngine.gameSize.Width)
                {
                    if (entity.GetComponent(typeof(MissileComponent)) != null)
                    {
                        HealthComponent health = (HealthComponent)entity.GetComponent(typeof(HealthComponent));
                        health.Life = 0;
                    }
                    else
                    {
                        position.PositionX -= velocity.VelocityX * deltaT;
                        position.PositionY -= velocity.VelocityY * deltaT;
                    }
                }

                    
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
