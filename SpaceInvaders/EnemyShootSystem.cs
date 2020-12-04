using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// System to make enemy shoot randomly
    /// </summary>
    class EnemyShootSystem : GameSystem
    {

        double shootProbability;

        Random random;

        public EnemyShootSystem(GameEngine gameEngine)
        {
            this.ShootProbability = 0.05;
            this.random = new Random();
        }

        public double ShootProbability { get => shootProbability; set => shootProbability = value; }



        public override void update(GameEngine gameEngine, double deltaT)
        {

            double randomNumber = random.NextDouble();

            if (randomNumber <= ShootProbability * deltaT)
            {
                HashSet<Entity> enemyList = gameEngine.entityManager.getListOfEnemyShips();

                int enemyCount = enemyList.Count();
                int randomShipIndex = random.Next(0, enemyCount);

                Entity entity = enemyList.ElementAt(randomShipIndex);
                if (entity.GetComponent(typeof(CanShootComponent)) != null)
                {
                    gameEngine.entityManager.createMissile(entity);
                    entity.removeComponent(typeof(CanShootComponent));
                }
                
            }
            shootProbability += 0.01 * deltaT;
        }


    }
}
