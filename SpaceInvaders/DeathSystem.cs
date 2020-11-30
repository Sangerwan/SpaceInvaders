using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class DeathSystem : GameSystem
    {
        public DeathSystem(GameEngine gameEngine)
        {

        }
        public override void update(GameEngine gameEngine, double deltaT)
        {
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> candDieEntities = getEntities(gameEngine);
            foreach(Entity entity in candDieEntities)
            {
                HealthComponent health = (HealthComponent)entity.GetComponent(typeof(HealthComponent));
                if (health.HP <= 0)
                {
                    OnDeathComponent ondeath = (OnDeathComponent)entity.GetComponent(typeof(OnDeathComponent));
                    if(ondeath!=null)
                        ondeath.Action();
                    entities.Remove(entity);
                }
            }
            
        }

        protected override HashSet<Entity> getEntities(GameEngine gameEngine)
        {
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> candDieEntities = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(HealthComponent)) != null)
                    candDieEntities.Add(entity);
            }
            return candDieEntities;
        }
    }
}
