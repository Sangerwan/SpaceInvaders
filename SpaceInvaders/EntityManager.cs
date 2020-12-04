using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace SpaceInvaders
{
    /// <summary>
    /// Class to manage entites
    /// </summary>
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
        /// <param name="gameEngine">Current game</param>
        void InitEnemy(GameEngine gameEngine)
        {
            Entity enemyBlock = new Entity();

            //creation of the components
            HitboxComponent enemyBlockHitboxComponent = new HitboxComponent(gameEngine.gameSize.Width / 2, 0);
            PositionComponent enemyBlockPositionComponent = new PositionComponent(0, 0);
            VelocityComponent enemyBlockVelocityComponent = new VelocityComponent(50, 0);
            EnemyBlockComponent enemyBlockComponent = new EnemyBlockComponent();
            enemyBlock.addComponent(enemyBlockHitboxComponent, enemyBlockPositionComponent, enemyBlockVelocityComponent, enemyBlockComponent);

            //creation of enemy lines
            AddEnemyLine(enemyBlock, 5, SpaceInvaders.Properties.Resources.ship9);
            AddEnemyLine(enemyBlock, 4, SpaceInvaders.Properties.Resources.ship8);
            AddEnemyLine(enemyBlock, 5, SpaceInvaders.Properties.Resources.ship7);
            AddEnemyLine(enemyBlock, 4, SpaceInvaders.Properties.Resources.ship6);
            AddEnemyLine(enemyBlock, 6, SpaceInvaders.Properties.Resources.ship5);


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
            HitboxComponent enemyBlockHitboxComponent = (HitboxComponent)enemyBlock.GetComponent(typeof(HitboxComponent));
            PositionComponent enemyBlockPositionComponent = (PositionComponent)enemyBlock.GetComponent(typeof(PositionComponent));

            double enemyBlockpositionX = enemyBlockPositionComponent.PositionX;
            double enemyBlockpositionY = enemyBlockPositionComponent.PositionY;
            int enemyBlockbaseWidth = enemyBlockHitboxComponent.Width;
            int enemyBlockbaseHeight = enemyBlockHitboxComponent.Height;

            for (int i = 0; i < nbShips; i++)
            {
                Entity spaceShip = createSpaceShip(shipImage, EntitySide.Side.Enemy,
                    enemyBlockpositionX + ((double)i / nbShips) * enemyBlockbaseWidth + (enemyBlockbaseWidth / nbShips - shipImage.Width) / 2,
                    enemyBlockpositionY + enemyBlockbaseHeight);

                gameObjects.Add(spaceShip);
            }

            enemyBlockHitboxComponent.Height += shipImage.Height + 10;
        }

        /// <summary>
        /// Create a spaceship
        /// </summary>
        /// <param name="image">The spaceship's image</param>
        /// <param name="side">The spaceship's side</param>
        /// <param name="positionX">The spaceship's x position</param>
        /// <param name="positionY">The spaceship's y position</param>
        /// <param name="health">The spaceship's health point</param>
        /// <returns>
        /// The created spaceship
        /// </returns>
        Entity createSpaceShip(Bitmap image, EntitySide.Side side, double positionX = 0, double positionY = 0, int health = 1)
        {
            Entity spaceShip = new Entity();

            SpaceShipComponent spaceShipComponent = new SpaceShipComponent();
            ImageComponent imageComponent = new ImageComponent(image);
            HitboxComponent hitboxComponent = new HitboxComponent(imageComponent.Image);
            PositionComponent positionComponent = new PositionComponent(positionX, positionY);
            HealthComponent healthComponent = new HealthComponent(health);
            VelocityComponent velocityComponent = new VelocityComponent(0, 0);
            SideComponent sideComponent = new SideComponent(side);
            CanShootComponent canShootComponent = new CanShootComponent();

            spaceShip.addComponent(imageComponent, hitboxComponent, positionComponent, healthComponent, velocityComponent, sideComponent, canShootComponent, spaceShipComponent);
            return spaceShip;
        }

        /// <summary>
        /// Add an input action to the entity
        /// </summary>
        /// <param name="entity">The entity on which the action is added</param>
        /// <param name="key">The key that activates the action</param>
        /// <param name="action">The action to perform on the entity</param>
        void addInput(Entity entity, System.Windows.Forms.Keys key, Action<Entity> action)
        {
            InputComponent inputComponent = (InputComponent)entity.GetComponent(typeof(InputComponent));
            if (inputComponent == null)
            {
                inputComponent = new InputComponent();
                entity.addComponent(inputComponent);
            }
            inputComponent.Input.Add(key, action);
        }


        /// <summary>
        /// Initialize the player
        /// </summary>
        /// <param name="gameEngine">Current game</param>
        void InitPlayer(GameEngine gameEngine)
        {
            Entity player = createSpaceShip(SpaceInvaders.Properties.Resources.ship3, EntitySide.Side.Ally, 0, gameEngine.gameSize.Height - 75, 3);
            PlayerComponent playerComponent = new PlayerComponent();
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
            addInput(player, System.Windows.Forms.Keys.Left,
                    entity =>
                {
                    VelocityComponent velocity = (VelocityComponent)entity.GetComponent(typeof(VelocityComponent));
                    velocity.VelocityX -= 100;
                });
            addInput(player, System.Windows.Forms.Keys.Right,
                entity =>
                {
                    VelocityComponent velocity = (VelocityComponent)entity.GetComponent(typeof(VelocityComponent));
                    velocity.VelocityX += 100;
                });
            addInput(player, System.Windows.Forms.Keys.Space,
                entity =>
                {
                    if (entity.GetComponent(typeof(CanShootComponent)) != null)
                    {
                        createMissile(entity);
                        entity.removeComponent(typeof(CanShootComponent));
                    }
                });

            player.addComponent(playerComponent);
            player.addComponent(inputComponent);

            GameObjects.Add(player);
        }

        /// <summary>
        /// Create a missile
        /// </summary>
        /// <param name="positionX">Missile's x position</param>
        /// <param name="positionY">Missile's y position</param>
        /// <param name="healthPoint">Missile's health point</param>
        /// <param name="side">Missile's side</param>
        /// <returns>The created missile</returns>
        public Entity createMissile(double positionX, double positionY, int healthPoint = 15, EntitySide.Side side = EntitySide.Side.Neutral)
        {
            Entity missile = new Entity();

            MissileComponent missileComponent = new MissileComponent();
            ImageComponent imageComponent = new ImageComponent(SpaceInvaders.Properties.Resources.shoot1);
            HitboxComponent hitboxComponent = new HitboxComponent(imageComponent.Image);
            PositionComponent positionComponent = new PositionComponent(positionX, positionY);
            HealthComponent healthComponent = new HealthComponent(15);
            VelocityComponent velocityComponent = new VelocityComponent(0, 0, 0);
            OnCollisionComponent onCollisionComponent = new OnCollisionComponent();
            SideComponent sideComponent = new SideComponent(side);
            if (side == EntitySide.Side.Ally)
                velocityComponent.VelocityY = -100;
            else if (side == EntitySide.Side.Enemy)
                velocityComponent.VelocityY = 100;

            missile.addComponent(missileComponent, imageComponent, hitboxComponent, positionComponent, healthComponent, velocityComponent, onCollisionComponent, sideComponent);
            return missile;
        }

        /// <summary>
        /// Create a missile entity from a given entity
        /// </summary>
        /// <param name="entity">The entity from which the missile is created</param>
        public void createMissile(Entity entity)
        {
            PositionComponent entityPositionComponent = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
            ImageComponent entityImageComponent = (ImageComponent)entity.GetComponent(typeof(ImageComponent));
            SideComponent entitySideComponent = (SideComponent)entity.GetComponent(typeof(SideComponent));

            Entity missile = createMissile(entityPositionComponent.PositionX + entityImageComponent.Image.Width / 2,
                entityPositionComponent.PositionY,
                side: entitySideComponent.Side
                );

            OnDeathComponent onDeathComponent = new OnDeathComponent(() => entity.addComponent(new CanShootComponent()));
            missile.addComponent(onDeathComponent);

            GameObjects.Add(missile);
        }

        #region test

        /// <summary>
        /// rotating missile
        /// </summary>
        /// <param name="entity"></param>
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
            VelocityComponent velocity = new VelocityComponent(0, 0, 100);
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
        #endregion

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
                foreach (Type componentType in components)
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
                    HealthComponent playerHealthComponent = (HealthComponent)entity.GetComponent(typeof(HealthComponent));
                    return playerHealthComponent.HP;
                }
            }
            return 0;// probably dead
        }

        /// <summary>
        /// Get the list of enemy ships
        /// </summary>
        /// <returns>list of enemy ships</returns>
        public HashSet<Entity> getListOfEnemyShips()
        {
            HashSet<Entity> entities = GetEntities(typeof(SideComponent), typeof(SpaceShipComponent));
            HashSet<Entity> enemyList = new HashSet<Entity>();

            foreach (Entity entity in entities)
            {
                SideComponent side = (SideComponent)entity.GetComponent(typeof(SideComponent));
                if (side == null) continue;

                if (side.Side == EntitySide.Side.Enemy)
                {
                    enemyList.Add(entity);
                }

            }
            return enemyList;
        }

        /// <summary>
        /// Get the enemy block entity
        /// </summary>
        /// <param name="gameEngine"></param>
        /// <returns>Retrun enemyblock if found, else null</returns>
        public Entity getEnemyBlock()
        {
            foreach (Entity entity in gameObjects)
            {
                if (entity.GetComponent(typeof(EnemyBlockComponent)) != null)
                    return entity;
            }
            return null;
        }

        /// <summary>
        /// Get the player
        /// </summary>
        /// <param name="gameEngine"></param>
        /// <returns>Retrun player if found, else null</returns>
        public Entity getPlayer()
        {
            foreach (Entity entity in gameObjects)
            {
                if (entity.GetComponent(typeof(PlayerComponent)) != null)
                    return entity;
            }
            return null;
        }
    }
}
