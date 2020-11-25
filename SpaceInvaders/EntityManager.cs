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
                ImageComponent image = new ImageComponent(SpaceInvaders.Properties.Resources.ship3);
                PositionComponent position = new PositionComponent(0, 0);
                HealthComponent life = new HealthComponent(1);
                VelocityComponent velocity = new VelocityComponent(0, 0, 0);
                SideComponent side = new SideComponent(EntitySide.Side.Enemy);
                HitboxComponent collision = new HitboxComponent(image.Image);
                SpaceShipComponent spaceShip = new SpaceShipComponent();

                player.addComponent(image);
                player.addComponent(position);
                player.addComponent(life);
                player.addComponent(velocity);
                player.addComponent(side);
                player.addComponent(collision);
                player.addComponent(spaceShip);




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
            HealthComponent health = new HealthComponent(1000);
            

            bunker.addComponent(bunkerComp);
            bunker.addComponent(bunkerImage);
            bunker.addComponent(bunkerHitbox);
            bunker.addComponent(bunkerPosition);
            bunker.addComponent(bunkerSide);
            bunker.addComponent(health);

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
            InputComponent input = new InputComponent();
            CanShootComponent shoot = new CanShootComponent();
            player.addComponent(isPlayer);
            player.addComponent(image);
            player.addComponent(hitbox);
            player.addComponent(position);
            player.addComponent(life);
            player.addComponent(velocity);
            player.addComponent(side);
            player.addComponent(input);
            player.addComponent(shoot);
            GameObjects.Add(player);
            

        }
        public void createMissile(Entity entity)
        {
            PositionComponent entityPosition = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
            ImageComponent entityImage = (ImageComponent)entity.GetComponent(typeof(ImageComponent));
            SideComponent entitySide = (SideComponent)entity.GetComponent(typeof(SideComponent));

            Entity missile = new Entity();
            MissileComponent missileComponent = new MissileComponent();
            ImageComponent image = new ImageComponent(SpaceInvaders.Properties.Resources.shoot1);
            HitboxComponent hitbox = new HitboxComponent(image.Image);
            PositionComponent position = new PositionComponent(entityPosition.PositionX + entityImage.Image.Width / 2, entityPosition.PositionY);
            HealthComponent life = new HealthComponent(1);
            VelocityComponent velocity = new VelocityComponent(0, 0, 0);
            if(entitySide.Side == EntitySide.Side.Ally)            
                velocity.VelocityY =-100;
            else if (entitySide.Side == EntitySide.Side.Enemy)
                velocity.VelocityY = 100;
            SideComponent side = new SideComponent(entitySide.Side);
            OnCollisionComponent collision = new OnCollisionComponent();
            missile.addComponent(missileComponent);
            missile.addComponent(image);
            missile.addComponent(hitbox);
            missile.addComponent(position);
            missile.addComponent(life);
            missile.addComponent(velocity);
            missile.addComponent(side);
            missile.addComponent(collision);

            GameObjects.Add(missile);
            

        }

    }
}
