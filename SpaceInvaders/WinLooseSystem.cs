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
            bool noPlayerLeft = true;
            bool noEnemyLeft = true;
            bool enemyBlockReached = true;
            Entity enemyBlock = getEnemyBlock(gameEngine);
            PositionComponent enemyBlockPosition = (PositionComponent)enemyBlock.GetComponent(typeof(PositionComponent));
            HitboxComponent enemyBlockHitbox = (HitboxComponent)enemyBlock.GetComponent(typeof(HitboxComponent));
            if (enemyBlockPosition.PositionY + enemyBlockHitbox.Height < gameEngine.gameSize.Height - 150) enemyBlockReached = false;
            foreach (Entity entity in spaceShip)
            {
                if (entity.GetComponent(typeof(PlayerComponent)) != null) noPlayerLeft = false;
                SideComponent side = (SideComponent)entity.GetComponent(typeof(SideComponent));
                if (side != null && side.Side == EntitySide.Side.Enemy) noEnemyLeft = false; 
            }
            if (noPlayerLeft) gameEngine.currentGameState = GameState.state.Loose;
            if (enemyBlockReached) gameEngine.currentGameState = GameState.state.Loose;
            if (noEnemyLeft) gameEngine.currentGameState = GameState.state.Win;
        }

        Entity getEnemyBlock(GameEngine gameEngine)
        {
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(EnemyBlockComponent)) != null)
                    return entity;
            }
            return null;
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
