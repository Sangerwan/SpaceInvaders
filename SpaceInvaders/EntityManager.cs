using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace SpaceInvaders
{
    class EntityManager
    {
        /// <summary>
        /// HashSet of all the entities in the game
        /// </summary>
        HashSet<Entity> gameObjects;

        /// <summary>
        /// Simple property to access gameObjects
        /// </summary>
        public HashSet<Entity> GameObjects { get => gameObjects; private set => gameObjects = value; }

        /// <summary>
        /// Simple Constructor
        /// </summary>
        /// <param name="gameEngine">Instance of the current game</param>
        public EntityManager(GameEngine gameEngine)
        {
            this.GameObjects = new HashSet<Entity>();
            Init(gameEngine);            
        }

        /// <summary>
        /// Initialize the base entities
        /// </summary>
        /// <param name="gameEngine">Instance of the current game</param>
        void Init(GameEngine gameEngine)
        {
            InitPlayer(gameEngine);
            InitEnemy(gameEngine);
            InitBunker(gameEngine);
        }

        /// <summary>
        /// Initialize the bunkers
        /// </summary>
        /// <param name="gameEngine">Instance of the current game</param>
        void InitBunker(GameEngine gameEngine)
        {
            GameObjects.Add(CreateBunker(50, gameEngine.gameSize.Height - 150));
            GameObjects.Add(CreateBunker(gameEngine.gameSize.Width / 2 - 50, gameEngine.gameSize.Height - 150));
            GameObjects.Add(CreateBunker(gameEngine.gameSize.Width - 150, gameEngine.gameSize.Height - 150));
        }
        /// <summary>
        /// Create a bunker
        /// </summary>
        /// <param name="positionX">bunker x position</param>
        /// <param name="positionY">bunker y position</param>
        /// <returns>
        /// An Entity of the created bunker
        /// </returns>
        Entity CreateBunker(double positionX, double positionY)
        {
            Entity bunker = new Entity();
            BunkerComponent bunkerComponent = new BunkerComponent();
            ImageComponent bunkerImageComponent = new ImageComponent(SpaceInvaders.Properties.Resources.bunker);
            HitboxComponent bunkerHitboxComponent = new HitboxComponent(bunkerImageComponent.Image);
            PositionComponent bunkerPositionComponent = new PositionComponent(positionX, positionY);
            SideComponent bunkerSideComponent = new SideComponent(EntitySide.Side.Neutral);
            HealthComponent bunkerHealthComponent = new HealthComponent(1000);

            bunker.addComponent(bunkerComponent, bunkerImageComponent, bunkerHitboxComponent, bunkerPositionComponent, bunkerSideComponent, bunkerHealthComponent);

            return bunker;
        }

        /// <summary>
        /// Initialize the enemies
        /// </summary>
        /// <param name="gameEngine"></param>
        void InitEnemy(GameEngine gameEngine)
        {
            Entity enemyBlock = new Entity();

            //creation of the components
            HitboxComponent enemyBlockHitboxComponent = new HitboxComponent(gameEngine.gameSize.Width,0);
            PositionComponent enemyBlockPositionComponent = new PositionComponent(0, 0);
            VelocityComponent enemyBlockVelocityComponent = new VelocityComponent(50, 0);//50
            EnemyBlockComponent enemyBlockComponent = new EnemyBlockComponent();
            enemyBlock.addComponent(enemyBlockHitboxComponent, enemyBlockPositionComponent, enemyBlockVelocityComponent, enemyBlockComponent);

            //creation of enemy lines
            AddEnemyLine(enemyBlock, 5, SpaceInvaders.Properties.Resources.ship9);
            AddEnemyLine(enemyBlock, 4, SpaceInvaders.Properties.Resources.ship8);
            AddEnemyLine(enemyBlock, 5, SpaceInvaders.Properties.Resources.ship7);
            AddEnemyLine(enemyBlock, 4, SpaceInvaders.Properties.Resources.ship6);
            AddEnemyLine(enemyBlock, 6, SpaceInvaders.Properties.Resources.ship5);
            AddEnemyLine(enemyBlock, 3, SpaceInvaders.Properties.Resources.ship4);
            AddEnemyLine(enemyBlock, 4, SpaceInvaders.Properties.Resources.ship2);
            AddEnemyLine(enemyBlock, 5, SpaceInvaders.Properties.Resources.ship1);

            gameObjects.Add(enemyBlock);
        }

        /// <summary>
        /// Add enemy lines
        /// </summary>
        /// <param name="enemyBlock">The enemyBlock where enemis are added</param>
        /// <param name="nbShips">Number of ship to add</param>
        /// <param name="shipImage">Image of the enemy ship</param>
        /// <param name="nbHP">Number of life for the ship, by default 3 health point</param>
        public void AddEnemyLine(Entity enemyBlock, int nbShips, Bitmap shipImage, int nbHP = 3)
        {
            HitboxComponent enemyBlockHitbox = (HitboxComponent)enemyBlock.GetComponent(typeof(HitboxComponent));
            PositionComponent enemyBlockPosition = (PositionComponent)enemyBlock.GetComponent(typeof(PositionComponent));

            double positionX = enemyBlockPosition.PositionX;
            double positionY = enemyBlockPosition.PositionY;
            int baseWidth = enemyBlockHitbox.Width;
            int baseHeight = enemyBlockHitbox.Height;

            for (int i = 0; i < nbShips; i++)
            {                
                Entity spaceShip = createSpaceShip(shipImage, EntitySide.Side.Enemy, 
                    positionX + ((double)i / nbShips) * baseWidth + (baseWidth / nbShips - shipImage.Width) / 2,
                    positionY + baseHeight);                
                gameObjects.Add(spaceShip);
            }

            enemyBlockHitbox.Height += shipImage.Height + 5;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="side"></param>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        /// <param name="health"></param>
        /// <returns>
        /// 
        /// </returns>
        Entity createSpaceShip(Bitmap image, EntitySide.Side side, double positionX=0, double positionY=0, int health=3)
        {
            Entity spaceShip = new Entity();
            SpaceShipComponent spaceShipComponent = new SpaceShipComponent();
            ImageComponent imageComponent = new ImageComponent(image);
            HitboxComponent hitboxComponent = new HitboxComponent(imageComponent.Image);
            PositionComponent positionComponent = new PositionComponent(positionX,positionY);
            HealthComponent healthComponent = new HealthComponent(health);
            VelocityComponent velocityComponent = new VelocityComponent(0, 0, 0);
            SideComponent sideComponent = new SideComponent(side);
            CanShootComponent canShootComponent = new CanShootComponent();
            spaceShip.addComponent(imageComponent, hitboxComponent, positionComponent, healthComponent, velocityComponent, sideComponent, canShootComponent, spaceShipComponent);
            return spaceShip;
        }

        void InitPlayer(GameEngine gameEngine)
        {
            Entity player = createSpaceShip(SpaceInvaders.Properties.Resources.ship3, EntitySide.Side.Ally,0, gameEngine.gameSize.Height - 75);
            PlayerComponent playerComponent = new PlayerComponent();
            /*ImageComponent imageComponent = new ImageComponent(SpaceInvaders.Properties.Resources.ship3);
            HitboxComponent hitboxComponent = new HitboxComponent(imageComponent.Image);
            PositionComponent positionComponent = new PositionComponent(0, gameEngine.gameSize.Height - 50);
            HealthComponent healthComponent = new HealthComponent(3);
            VelocityComponent velocityComponent = new VelocityComponent(0, 0, 0);
            SideComponent sideComponent = new SideComponent(EntitySide.Side.Ally);*/
            InputComponent inputComponent = new InputComponent();
            /*inputComponent.Input.Add(System.Windows.Forms.Keys.Up, 
                entity => 
                { 
                    VelocityComponent velocity = (VelocityComponent)entity.GetComponent(typeof(VelocityComponent)); 
                    velocity.VelocityY -= 100; 
                });
            inputComponent.Input.Add(System.Windows.Forms.Keys.Down,
                entity => 
                {
                    VelocityComponent velocity = (VelocityComponent)entity.GetComponent(typeof(VelocityComponent));
                    velocity.VelocityY += 100;
                });*/
            inputComponent.Input.Add(System.Windows.Forms.Keys.Left,
                entity => 
                {
                    VelocityComponent velocity = (VelocityComponent)entity.GetComponent(typeof(VelocityComponent));
                    velocity.VelocityX -= 100;
                });
            inputComponent.Input.Add(System.Windows.Forms.Keys.Right,
                entity => 
                {
                    VelocityComponent velocity = (VelocityComponent)entity.GetComponent(typeof(VelocityComponent));
                    velocity.VelocityX += 100;
                });
            inputComponent.Input.Add(System.Windows.Forms.Keys.Space, 
                entity =>
                {
                    if (entity.GetComponent(typeof(CanShootComponent)) != null)
                    {
                        createMissile(entity);
                        entity.removeComponent(typeof(CanShootComponent));
                    }
                    
                }
                );

            //CanShootComponent canShootComponent = new CanShootComponent();
            player.addComponent(playerComponent);
            /*player.addComponent(imageComponent);
            player.addComponent(hitboxComponent);
            player.addComponent(positionComponent);
            player.addComponent(healthComponent);
            player.addComponent(velocityComponent);
            player.addComponent(canShootComponent);
            player.addComponent(sideComponent);*/
            player.addComponent(inputComponent);
            
            GameObjects.Add(player);
        }

        /// <summary>
        /// Create a missile
        /// </summary>
        /// <param name="positionX">Missile x position</param>
        /// <param name="positionY">Missile y position</param>
        /// <param name="healthPoint">Missile health point</param>
        /// <param name="side">Missile side</param>
        /// <returns>The created missile</returns>
        public Entity createMissile(double positionX, double positionY,int healthPoint=15,EntitySide.Side side = EntitySide.Side.Neutral)
        {
            Entity missile = new Entity();
            MissileComponent missileComponent = new MissileComponent();
            ImageComponent imageComponent = new ImageComponent(SpaceInvaders.Properties.Resources.shoot1);
            HitboxComponent hitboxComponent = new HitboxComponent(imageComponent.Image);
            PositionComponent positionComponent = new PositionComponent(positionX, positionY);
            HealthComponent HealthComponent = new HealthComponent(15);
            VelocityComponent velocityComponent = new VelocityComponent(0, 0, 0);            
            SideComponent sideComponent = new SideComponent(side);
            OnCollisionComponent onCollisionComponent = new OnCollisionComponent();
            if (side == EntitySide.Side.Ally)
                velocityComponent.VelocityY = -100;
            else if (side == EntitySide.Side.Enemy)
                velocityComponent.VelocityY = 100;

            missile.addComponent(missileComponent, imageComponent, hitboxComponent, positionComponent, HealthComponent, velocityComponent, sideComponent, onCollisionComponent);
            return missile;
        }

        /// <summary>
        /// Create a missile entity from a given entity
        /// </summary>
        /// <param name="entity">The entity from which the missile is created</param>
        public void createMissile(Entity entity)
        {
            PositionComponent entityPosition = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
            ImageComponent entityImage = (ImageComponent)entity.GetComponent(typeof(ImageComponent));
            SideComponent entitySide = (SideComponent)entity.GetComponent(typeof(SideComponent));

            Entity missile =  createMissile(entityPosition.PositionX + entityImage.Image.Width / 2,
                entityPosition.PositionY,
                side : entitySide.Side
                );
            
            OnDeathComponent onDeathComponent = new OnDeathComponent(() => entity.addComponent(new CanShootComponent()));
            missile.addComponent(onDeathComponent) ;

            GameObjects.Add(missile);


        }
        public void createMissile2(Entity entity)
        {
            PositionComponent entityPosition = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
            ImageComponent entityImage = (ImageComponent)entity.GetComponent(typeof(ImageComponent));
            SideComponent entitySide = (SideComponent)entity.GetComponent(typeof(SideComponent));

            Entity missile = new Entity();
            MissileComponent missileComponent = new MissileComponent();
            ImageComponent image = new ImageComponent(SpaceInvaders.Properties.Resources.shoot1);
            HitboxComponent hitbox = new HitboxComponent(image.Image);
            PositionComponent position = new PositionComponent(entityPosition.PositionX + entityImage.Image.Width / 2, entityPosition.PositionY);
            HealthComponent life = new HealthComponent(15);
            VelocityComponent velocity = new VelocityComponent(0,0, 100);
            OnDeathComponent onDeathComponent = new OnDeathComponent(() => entity.addComponent(new CanShootComponent()));
            
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
            missile.addComponent(onDeathComponent);
            
            GameObjects.Add(missile);
        }

        /// <summary>
        /// Filter the list of entities with the list of components they must contain
        /// </summary>
        /// <param name="components">Component type list</param>
        /// <returns>The filtered list of entities</returns>
        public HashSet<Entity> GetEntities(params Type[] components)
        {            
            HashSet<Entity> filteredEntities = new HashSet<Entity>();
            foreach (Entity entity in gameObjects)
            {
                bool hasAllComponents = true;
                foreach(Type componentType in components)
                {
                    if (entity.GetComponent(componentType) == null)
                    {
                        hasAllComponents = false;
                        break;
                    }
                        
                }
                if (hasAllComponents)
                    filteredEntities.Add(entity);
            }            
            return filteredEntities;
        }

        /// <summary>
        /// Get the player's health point
        /// </summary>
        /// <returns>The player's health point</returns>
        public int getPlayerLife()
        {            
            foreach (Entity entity in gameObjects)
            {
                if (entity.GetComponent(typeof(PlayerComponent)) != null)
                {
                    HealthComponent playerHealth = (HealthComponent)entity.GetComponent(typeof(HealthComponent));
                    return playerHealth.HP;
                }
            }
            return 0;// probably dead
        }
    }
}
