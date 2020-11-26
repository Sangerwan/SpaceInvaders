using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class WinLooseSystem:GameSystem
    {

        public WinLooseSystem(GameEngine gameEngine)
        {

        }

        public override void update(GameEngine gameEngine, double deltaT)
        {
            HashSet<Entity> spaceShip = getEntities(gameEngine);
            bool noPlayerLeft=true;
            bool noEnemyLeft=true;
            foreach (Entity entity in spaceShip)
            {
                if (entity.GetComponent(typeof(PlayerComponent)) != null) noPlayerLeft = false;
                SideComponent side = (SideComponent)entity.GetComponent(typeof(SideComponent));
                if (side != null && side.Side == EntitySide.Side.Enemy) noEnemyLeft = false; 
            }
            if (noPlayerLeft) gameEngine.currentGameState = GameState.state.Loose;
            if (noEnemyLeft) gameEngine.currentGameState = GameState.state.Win;
        }

            void updateGameState(GameEngine gameEngine)
        {

        }

        protected override HashSet<Entity> getEntities(GameEngine gameEngine)
        {
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> spaceShip = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(SpaceShipComponent)) != null)
                    spaceShip.Add(entity);
            }
            return spaceShip;
        }
    }
}
