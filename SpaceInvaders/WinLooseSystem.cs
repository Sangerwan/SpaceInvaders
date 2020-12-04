using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// System to update the game state if the game is won or lost
    /// </summary>
    class WinLooseSystem : GameSystem
    {

        public WinLooseSystem(GameEngine gameEngine)
        {

        }

        public override void update(GameEngine gameEngine, double deltaT)
        {

            if (noPlayerLeft(gameEngine))
                gameEngine.currentGameState = GameState.state.Loose;
            if (enemyBlockReached(gameEngine))
                gameEngine.currentGameState = GameState.state.Loose;
            if (noEnemyLeft(gameEngine))
                gameEngine.currentGameState = GameState.state.Win;
        }

        /// <summary>
        /// Test if there are enemies remaining
        /// </summary>
        /// <param name="gameEngine"></param>
        /// <returns>Return true if there are no enemies remaining, else false</returns>
        bool noEnemyLeft(GameEngine gameEngine)
        {
            bool noEnemyLeft = true;
            HashSet<Entity> spaceShip = gameEngine.entityManager.GetEntities(typeof(SpaceShipComponent));
            foreach (Entity entity in spaceShip)
            {
                SideComponent sideComponent = (SideComponent)entity.GetComponent(typeof(SideComponent));
                if (sideComponent != null && sideComponent.Side == EntitySide.Side.Enemy) noEnemyLeft = false;
            }
            return noEnemyLeft;
        }

        /// <summary>
        /// Test if there is a player remaining 
        /// </summary>
        /// <param name="gameEngine"></param>
        /// <returns>return false if there's a player, else true</returns>
        bool noPlayerLeft(GameEngine gameEngine)
        {
            return gameEngine.entityManager.getPlayer() != null ? false : true;
        }

        /// <summary>
        /// Test if enemy block has reached the bunkers
        /// </summary>
        /// <param name="gameEngine">Current game</param>
        /// <returns>Return true if enemy block reached bunkers, else false</returns>
        bool enemyBlockReached(GameEngine gameEngine)
        {
            bool enemyBlockReached = true;
            Entity enemyBlock = gameEngine.entityManager.getEnemyBlock();
            PositionComponent enemyBlockPositionComponent = (PositionComponent)enemyBlock.GetComponent(typeof(PositionComponent));
            HitboxComponent enemyBlockHitboxComponent = (HitboxComponent)enemyBlock.GetComponent(typeof(HitboxComponent));

            if (enemyBlockPositionComponent.PositionY + enemyBlockHitboxComponent.Height < gameEngine.gameSize.Height - 150)
                enemyBlockReached = false;
            return enemyBlockReached;
        }




    }
}
