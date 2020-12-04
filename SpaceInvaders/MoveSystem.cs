using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// System to move entities
    /// </summary>
    class MoveSystem : GameSystem
    {
        /// <summary>
        /// Keep enemy block for simple access
        /// </summary>
        Entity enemyBlock;
        public MoveSystem(GameEngine gameEngine)
        {
            HashSet<Entity> enemyBlocks = gameEngine.entityManager.GetEntities(typeof(EnemyBlockComponent));

            //only one atm
            this.enemyBlock = enemyBlocks.ElementAt(0);
        }

        public override void update(GameEngine gameEngine, double deltaT)
        {
            HashSet<Entity> movableEntities = gameEngine.entityManager.GetEntities(typeof(VelocityComponent));

            updateEnemyVelocity(gameEngine, deltaT, movableEntities);

            foreach (Entity entity in movableEntities)
            {

                moveEntity(entity, deltaT);

                if (outOfBonds(gameEngine, entity))
                {
                    if (entity.GetComponent(typeof(MissileComponent)) != null)
                    {
                        HealthComponent health = (HealthComponent)entity.GetComponent(typeof(HealthComponent));
                        health.HP = 0;
                    }
                    else
                    {
                        moveBackEntity(entity, deltaT);
                    }
                }
            }
        }

        /// <summary>
        /// Move an entity
        /// </summary>
        /// <param name="entity">Entity to move</param>
        /// <param name="deltaT">Time elapsed since last update</param>
        void moveEntity(Entity entity, double deltaT)
        {
            PositionComponent position = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
            VelocityComponent velocity = (VelocityComponent)entity.GetComponent(typeof(VelocityComponent));

            position.PositionX += velocity.VelocityX * deltaT;
            position.PositionY += velocity.VelocityY * deltaT;
        }

        /// <summary>
        /// Move back an entity
        /// </summary>
        /// <param name="entity">Entity to move back</param>
        /// <param name="deltaT">Time elapsed since last update</param>
        void moveBackEntity(Entity entity, double deltaT)
        {
            PositionComponent position = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
            VelocityComponent velocity = (VelocityComponent)entity.GetComponent(typeof(VelocityComponent));

            position.PositionX -= velocity.VelocityX * deltaT;
            position.PositionY -= velocity.VelocityY * deltaT;
        }

        /// <summary>
        /// Test if an entity is out of the screen
        /// </summary>
        /// <param name="gameEngine">Current game</param>
        /// <param name="entity">Entity to check</param>
        /// <returns>true if is out of the screen, else false</returns>
        bool outOfBonds(GameEngine gameEngine, Entity entity)
        {
            PositionComponent position = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
            HitboxComponent hitbox = (HitboxComponent)entity.GetComponent(typeof(HitboxComponent));

            return position.PositionY < 0
                || position.PositionY + hitbox.Size.Height > gameEngine.gameSize.Height
                || position.PositionX < 0
                || position.PositionX + hitbox.Size.Width > gameEngine.gameSize.Width;
        }

        /// <summary>
        /// Update enemy velocity based on the enemy block
        /// </summary>
        /// <param name="gameEngine"></param>
        /// <param name="deltaT"></param>
        /// <param name="movableEntities"></param>
        void updateEnemyVelocity(GameEngine gameEngine, double deltaT, HashSet<Entity> movableEntities)
        {
            HashSet<Entity> enemyList = gameEngine.entityManager.getListOfEnemyShips();
            updateEnemyBlock(enemyList);
            VelocityComponent enemyBlockVelocity = (VelocityComponent)enemyBlock.GetComponent(typeof(VelocityComponent));
            PositionComponent enemyBlockPosition = (PositionComponent)enemyBlock.GetComponent(typeof(PositionComponent));

            moveEntity(enemyBlock, deltaT);

            if (outOfBonds(gameEngine, enemyBlock))
            {
                moveBackEntity(enemyBlock, deltaT);
                enemyBlockVelocity.VelocityX *= -1.02;
                enemyBlockVelocity.VelocityY = 1500;
            }
            else
            {
                moveBackEntity(enemyBlock, deltaT);
                enemyBlockVelocity.VelocityY = 0;
            }
            foreach (Entity entity in enemyList)
            {
                VelocityComponent entityVelocity = (VelocityComponent)entity.GetComponent(typeof(VelocityComponent));
                entityVelocity.VelocityX = enemyBlockVelocity.VelocityX;
                entityVelocity.VelocityY = enemyBlockVelocity.VelocityY;
                entityVelocity.AngularVelocity = enemyBlockVelocity.AngularVelocity;
            }
        }
        void updateEnemyBlock(HashSet<Entity> enemyList)
        {
            HitboxComponent enemyBlockHitbox = (HitboxComponent)enemyBlock.GetComponent(typeof(HitboxComponent));
            PositionComponent enemyBlockPosition = (PositionComponent)enemyBlock.GetComponent(typeof(PositionComponent));
            double xMin = int.MaxValue;
            double xMax = int.MinValue;
            double yMin = int.MaxValue;
            double yMax = int.MinValue;
            foreach (Entity entity in enemyList)
            {
                HitboxComponent entityHitbox = (HitboxComponent)entity.GetComponent(typeof(HitboxComponent));
                PositionComponent entityPosition = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
                if (entityPosition.PositionX < xMin) xMin = entityPosition.PositionX;
                if (entityPosition.PositionX + entityHitbox.Width > xMax) xMax = entityPosition.PositionX + entityHitbox.Width;
                if (entityPosition.PositionY < yMin) yMin = entityPosition.PositionY;
                if (entityPosition.PositionY + entityHitbox.Height > yMax) yMax = entityPosition.PositionY + entityHitbox.Height;
            }
            enemyBlockHitbox.Width = (int)(xMax - xMin);
            enemyBlockHitbox.Height = (int)(yMax - yMin);
            enemyBlockPosition.PositionX = xMin;
            enemyBlockPosition.PositionY = yMin;

        }




    }
}
