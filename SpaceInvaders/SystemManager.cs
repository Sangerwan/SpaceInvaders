using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class SystemManager
    {
        HashSet<GameSystem> systems;
        public HashSet<GameSystem> Systems { get => systems;}

        public SystemManager(GameEngine gameEngine)
        {
            this.systems = new HashSet<GameSystem>();
            InitSystem(gameEngine);
        }

        void InitSystem(GameEngine gameEngine)
        {
            InitCollision(gameEngine);
            InitMove(gameEngine);
            InitCollision(gameEngine);
            InitRender(gameEngine);
            
        }
        void InitInput(GameEngine gameEngine)
        {
            InputSystem inputSystem = new InputSystem();
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> inputableEntities = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(InputComponent)) != null)
                    inputableEntities.Add(entity);
            }
            inputSystem.Entities = inputableEntities;
        }
        void InitMove(GameEngine gameEngine)
        {
            MoveSystem moveSystem = new MoveSystem();
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> movableEntities = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(VelocityComponent)) != null)
                    movableEntities.Add(entity);
            }
            moveSystem.Entities = movableEntities;
        }
        void InitCollision(GameEngine gameEngine)
        {
            CollisionSystem collisionSystem = new CollisionSystem();
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> collidableEntities = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(OnCollisionComponent)) != null)
                    collidableEntities.Add(entity);
            }
            collisionSystem.Entities = collidableEntities;
        }
        
        void InitRender(GameEngine gameEngine)
        {

        }


        public GameSystem GetSystem(Type gameSystem)
        {

            foreach (GameSystem system in systems)
            {
                if (system.GetType() == gameSystem) return system;
            }
            return null;
        }
    }
}
