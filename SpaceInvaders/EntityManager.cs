using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class EntityManager
    {
        HashSet<Entity> gameObjects;

        public HashSet<Entity> GameObjects { get => gameObjects; set => gameObjects = value; }

        
        public EntityManager(GameEngine gameEngine)
        {
            this.GameObjects= new HashSet<Entity>();

            #region testo
            {
                Entity player = new Entity();
                Component image = new ImageComponent(SpaceInvaders.Properties.Resources.ship3);
                Component position = new PositionComponent(0, 0);
                HealthComponent life = new HealthComponent(3);
                Component velocity = new VelocityComponent(0, 0, 0);
                Component side = new SideComponent(EntitySide.Side.Ally);



                player.addComponent(image);
                player.addComponent(position);
                player.addComponent(life);
                player.addComponent(velocity);
                player.addComponent(side);
                
                

                
                GameObjects.Add(player);
            }
            Init(gameEngine);
            #endregion
        }

        void Init(GameEngine gameEngine)
        {
            InitPlayer(gameEngine);
            InitEnemy(gameEngine);
            InitBunker(gameEngine);
            InitSound(gameEngine);
        }

        void InitSound(GameEngine gameEngine)
        {
            //background sound
        }

        void InitBunker(GameEngine gameEngine)
        {
            GameObjects.Add(CreateBunker(50, gameEngine.gameSize.Height - 150));
            GameObjects.Add(CreateBunker(gameEngine.gameSize.Width / 2 - 50, gameEngine.gameSize.Height - 150));
            GameObjects.Add(CreateBunker(gameEngine.gameSize.Width - 150, gameEngine.gameSize.Height - 150));
        }

        Entity CreateBunker(double positionX, double positionY)
        {
            Entity bunker = new Entity();
            BunkerComponent bunkerComp = new BunkerComponent();
            ImageComponent bunkerImage = new ImageComponent(SpaceInvaders.Properties.Resources.bunker);
            HitboxComponent bunkerHitbox = new HitboxComponent(bunkerImage.Image);
            PositionComponent bunkerPosition = new PositionComponent(positionX, positionY);
            SideComponent bunkerSide = new SideComponent(EntitySide.Side.Neutral);

            bunker.addComponent(bunkerComp);
            bunker.addComponent(bunkerImage);
            bunker.addComponent(bunkerHitbox);
            bunker.addComponent(bunkerPosition);
            bunker.addComponent(bunkerSide);

            return bunker;
        }
        void InitEnemy(GameEngine gameEngine)
        {
            
        }

        void InitPlayer(GameEngine gameEngine)
        {
            Entity player = new Entity();
            PlayerComponent isPlayer = new PlayerComponent();
            ImageComponent image = new ImageComponent(SpaceInvaders.Properties.Resources.ship3);
            HitboxComponent hitbox = new HitboxComponent(image.Image);
            PositionComponent position = new PositionComponent(0, gameEngine.gameSize.Height - 50);
            HealthComponent life = new HealthComponent(3);
            VelocityComponent velocity = new VelocityComponent(0, 0, 0);
            SideComponent side = new SideComponent(EntitySide.Side.Ally);

            player.addComponent(isPlayer);
            player.addComponent(image);
            player.addComponent(hitbox);
            player.addComponent(position);
            player.addComponent(life);
            player.addComponent(velocity);
            player.addComponent(side);

            GameObjects.Add(player);
            

        }
    }
}
