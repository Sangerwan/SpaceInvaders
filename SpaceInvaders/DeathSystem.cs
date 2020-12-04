using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// System to manage dead entities
    /// </summary>
    class DeathSystem : GameSystem
    {
        public DeathSystem(GameEngine gameEngine)
        {

        }
        public override void update(GameEngine gameEngine, double deltaT)
        {
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> candDieEntities = gameEngine.entityManager.GetEntities(typeof(HealthComponent));
            foreach (Entity entity in candDieEntities)
            {
                HealthComponent healthComponent = (HealthComponent)entity.GetComponent(typeof(HealthComponent));
                if (healthComponent.HP <= 0)
                {
                    OnDeathComponent onDeathComponent = (OnDeathComponent)entity.GetComponent(typeof(OnDeathComponent));
                    if (onDeathComponent != null)
                        onDeathComponent.Action();
                    entities.Remove(entity);
                }
            }

        }


    }
}
