using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class EntityManager
    {
        public HashSet<Entity> gameObjects;
        enum Side
        {
            Ally,
            Enemy,
            Neutral
        }
        public EntityManager()
        {
            this.gameObjects= new HashSet<Entity>();

            #region testo
            {
                Entity player = new Entity();
                Component image = new ImageComponent(SpaceInvaders.Properties.Resources.ship3);
                Component position = new PositionComponent(0, 0);
                HealthComponent life = new HealthComponent(3);
                Component velocity = new VelocityComponent(0, 0, 0);
                Component side = new SideComponent(EntitySide.Side.Ally);

                OnCollisionBunkerComponent a = new OnCollisionBunkerComponent();
                player.addComponent(a);

                OnCollisionBunkerComponent b = (OnCollisionBunkerComponent)player.GetComponent(typeof(OnCollisionBunkerComponent));
                OnCollisionBunkerComponent b2 = (OnCollisionBunkerComponent)player.GetComponent(typeof(CollisionComponent));

                Console.WriteLine(b);
                Console.WriteLine(b2);


                player.addComponent(image);
                player.addComponent(position);
                player.addComponent(life);
                player.addComponent(velocity);
                player.addComponent(side);
                
                Entity player2 = new Entity();
                DamageComponent dmg = new DamageComponent(1);
                player2.addComponent(dmg);
                OnCollisionBunkerComponent c = (OnCollisionBunkerComponent)player.GetComponent(typeof(OnCollisionBunkerComponent));
                

                Console.WriteLine(life.Life);
                gameObjects.Add(player);
            }
            
            #endregion
        }

        public void Init(GameEngine gameEngine)
        {
            InitPlayer(gameEngine);
            InitEnemy(gameEngine);
            InitBunker(gameEngine);
            InitSound(gameEngine);
        }

        public void InitSound(GameEngine gameEngine)
        {
            //background sound
        }

        public void InitBunker(GameEngine gameEngine)
        {
            Entity bunker1 = new Entity();

        }

        public void InitEnemy(GameEngine gameEngine)
        {
            throw new NotImplementedException();
        }

        public void InitPlayer(GameEngine gameEngine)
        {
            Entity player = new Entity();
            PlayerComponent isPlayer = new PlayerComponent();
            ImageComponent image = new ImageComponent(SpaceInvaders.Properties.Resources.ship3);
            PositionComponent position = new PositionComponent(0, gameEngine.gameSize.Height - 50);
            HealthComponent life = new HealthComponent(3);
            VelocityComponent velocity = new VelocityComponent(0, 0, 0);
            SideComponent side = new SideComponent(EntitySide.Side.Ally);

            player.addComponent(isPlayer);
            player.addComponent(image);
            player.addComponent(position);
            player.addComponent(life);
            player.addComponent(velocity);
            player.addComponent(side);
            gameObjects.Add(player);
            

        }
    }
}
