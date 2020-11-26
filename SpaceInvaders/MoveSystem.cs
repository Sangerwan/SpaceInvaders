using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class MoveSystem: GameSystem
    {
        Entity enemyBlock;
        public MoveSystem(GameEngine gameEngine)
        {
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(EnemyBlockComponent)) != null)
                {
                    this.enemyBlock = entity;
                    return;
                }
            }
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
            updateEnemyVelocity(gameEngine, deltaT, movableEntities);
            
            foreach (Entity entity in movableEntities)
            {
                if (entity.GetComponent(typeof(EnemyBlockComponent)) != null) continue;
                moveEntity(entity,deltaT);
                
                if (outOfBonds(gameEngine, entity))
                {
                    if (entity.GetComponent(typeof(MissileComponent)) != null)
                    {
                        HealthComponent health = (HealthComponent)entity.GetComponent(typeof(HealthComponent));
                        health.Life = 0;
                    }
                    else
                    {
                        moveBackEntity(entity, deltaT);
                    }
                }

                    
            }
        }

        void moveEntity(Entity entity, double deltaT)
        {
            PositionComponent position = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
            VelocityComponent velocity = (VelocityComponent)entity.GetComponent(typeof(VelocityComponent));

            position.PositionX += velocity.VelocityX * deltaT;
            position.PositionY += velocity.VelocityY * deltaT;
        }

        void moveBackEntity(Entity entity, double deltaT)
        {
            PositionComponent position = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
            VelocityComponent velocity = (VelocityComponent)entity.GetComponent(typeof(VelocityComponent));


            position.PositionX -= velocity.VelocityX * deltaT;
            position.PositionY -= velocity.VelocityY * deltaT;
        }

        bool outOfBonds(GameEngine gameEngine, Entity entity)
        {
            PositionComponent position = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
            HitboxComponent hitbox = (HitboxComponent)entity.GetComponent(typeof(HitboxComponent));

            return position.PositionY < 0 
                || position.PositionY + hitbox.Size.Height > gameEngine.gameSize.Height 
                || position.PositionX < 0 
                || position.PositionX + hitbox.Size.Width > gameEngine.gameSize.Width;
        }
        void updateEnemyVelocity(GameEngine gameEngine,double deltaT ,HashSet<Entity> movableEntities)
        {            
            HashSet<Entity> enemyList = getEnemies(movableEntities);
            updateEnemyBlock(enemyList);
            VelocityComponent enemyBlockVelocity = (VelocityComponent)enemyBlock.GetComponent(typeof(VelocityComponent));
            PositionComponent enemyBlockPosition = (PositionComponent)enemyBlock.GetComponent(typeof(PositionComponent));
            moveEntity(enemyBlock, deltaT);
            if(outOfBonds(gameEngine,enemyBlock))
            {
                moveBackEntity(enemyBlock, deltaT);
                enemyBlockVelocity.VelocityX *= -1.01;
                enemyBlockVelocity.VelocityY = 200;
            }
            else
            {
                moveBackEntity(enemyBlock, deltaT);
                enemyBlockVelocity.VelocityY = 0; 
            }
            foreach(Entity entity in enemyList)
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
            foreach(Entity entity in enemyList)
            {
                HitboxComponent entityHitbox = (HitboxComponent)entity.GetComponent(typeof(HitboxComponent));
                PositionComponent entityPosition = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
                if (entityPosition.PositionX < xMin) xMin = entityPosition.PositionX;
                if (entityPosition.PositionX + entityHitbox.Width > xMax) xMax = entityPosition.PositionX + entityHitbox.Width;
                if (entityPosition.PositionY < yMin) yMin = entityPosition.PositionY;
                if (entityPosition.PositionY + entityHitbox.Height > yMax) yMax = entityPosition.PositionY + entityHitbox.Height;
            }
            enemyBlockHitbox.Width = (int)(xMax-xMin);
            enemyBlockHitbox.Height = (int)(yMax-yMin);
            enemyBlockPosition.PositionX = xMin;
            enemyBlockPosition.PositionY = yMin;
            
        }
        HashSet<Entity> getEnemies(HashSet<Entity> movableEntities)
        {
            
            HashSet<Entity> enemyList = new HashSet<Entity>();
            foreach (Entity entity in movableEntities)
            {
                SideComponent side = (SideComponent)entity.GetComponent(typeof(SideComponent));
                if (side != null)
                {
                    if (side.Side == EntitySide.Side.Enemy)
                    {
                        enemyList.Add(entity);
                    }
                }
            }
            return enemyList;
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
