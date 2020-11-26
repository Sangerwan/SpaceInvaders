using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class EnemyShootSystem:GameSystem
    {
        int shootProbability;
        Random random;
        public EnemyShootSystem(GameEngine gameEngine)
        {
            this.shootProbability = 1;
            this.random = new Random();
        }

        public override void update(GameEngine gameEngine, double deltaT)
        {
            /*HashSet<Entity> enemyList1 = getEntities(gameEngine);
            Entity entity1 = enemyList1.ElementAt(0);
            gameEngine.entityManager.createMissile(entity1);*/
            
            double randomNumber = random.NextDouble();

            if (randomNumber <= shootProbability * deltaT)
            {
                HashSet<Entity> enemyList = getEntities(gameEngine);
                int enemyCount = enemyList.Count();
                int randomShipIndex = random.Next(0, enemyCount);

                Entity entity = enemyList.ElementAt(randomShipIndex);
                if (entity.GetComponent(typeof(CanShootComponent)) != null)
                {
                    gameEngine.entityManager.createMissile(entity);
                    entity.removeComponent(typeof(CanShootComponent));
                }
            }
        }

        protected override HashSet<Entity> getEntities(GameEngine gameEngine)
        {
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;            
            HashSet<Entity> enemyList = new HashSet<Entity>();
            foreach (Entity entity in entities)
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
    }
}
