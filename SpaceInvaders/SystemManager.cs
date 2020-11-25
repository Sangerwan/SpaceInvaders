using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class SystemManager
    {
        List<GameSystem> systems;
        public List<GameSystem> Systems { get => systems;}

        public SystemManager(GameEngine gameEngine)
        {
            this.systems = new List<GameSystem>();
            InitSystem(gameEngine);
        }

        void InitSystem(GameEngine gameEngine)
        {
            InputSystem inputSystem = new InputSystem(gameEngine);
            MoveSystem moveSystem = new MoveSystem(gameEngine);
            CollisionSystem collisionSystem = new CollisionSystem(gameEngine);
            RenderSystem renderSystem = new RenderSystem(gameEngine);
            systems.Add(inputSystem);
            systems.Add(moveSystem);
            systems.Add(collisionSystem);
            systems.Add(renderSystem);
        }
        void InitInput(GameEngine gameEngine)
        {
            /*InputSystem inputSystem = new InputSystem();
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> inputableEntities = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(InputComponent)) != null)
                    inputableEntities.Add(entity);
            }
            inputSystem.Entities = inputableEntities;*/
        }
        void InitMove(GameEngine gameEngine)
        {
            /*MoveSystem moveSystem = new MoveSystem();
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> movableEntities = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(VelocityComponent)) != null)
                    movableEntities.Add(entity);
            }
            moveSystem.Entities = movableEntities;*/
        }
        void InitCollision(GameEngine gameEngine)
        {
           /* CollisionSystem collisionSystem = new CollisionSystem();
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> collidableEntities = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(OnCollisionComponent)) != null)
                    collidableEntities.Add(entity);
            }
            collisionSystem.Entities = collidableEntities;*/
        }
        
        void InitRender(GameEngine gameEngine)
        {

        }
        public void update(GameEngine gameEngine,double deltaT)
        {
            systems[0].update(gameEngine, deltaT);// input system
            if (gameEngine.currentGameState == GameState.state.Play)
            {
                for(int i = 0; i < systems.Count-1; i++)
                {
                    systems[i].update(gameEngine, deltaT);
                }
            }
        }

        public void update(GameEngine gameEngine)
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
