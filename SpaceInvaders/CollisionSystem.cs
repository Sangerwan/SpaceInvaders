using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace SpaceInvaders
{
    /// <summary>
    /// System to handle collision
    /// </summary>
    class CollisionSystem : GameSystem
    {


        public CollisionSystem(GameEngine gameEngine)
        {

        }

        /// <summary>
        /// Simple test to check if there's an intersection
        /// </summary>
        /// <param name="positionComponentA">Position of the first entity</param>
        /// <param name="hitboxComponentA">Hitbox of the first entity</param>
        /// <param name="positionComponentB">Position of the second entity</param>
        /// <param name="hitboxComponentB">Hitbox of the second entity</param>
        /// <returns>true if there's an intersection, else false</returns>
        bool TestHitboxCollision(PositionComponent positionComponentA, HitboxComponent hitboxComponentA, PositionComponent positionComponentB, HitboxComponent hitboxComponentB)
        {
            bool AIsLeftOfB = positionComponentA.PositionX + hitboxComponentA.Size.Width < positionComponentB.PositionX;
            bool AIsRightOfB = positionComponentA.PositionX > positionComponentB.PositionX + hitboxComponentB.Size.Width;
            bool AIsAboveB = positionComponentA.PositionY + hitboxComponentA.Size.Height < positionComponentB.PositionY;
            bool AIsBelowB = positionComponentA.PositionY > positionComponentB.PositionY + hitboxComponentB.Size.Height;
            return !(AIsLeftOfB || AIsRightOfB || AIsAboveB || AIsBelowB);
        }

        /// <summary>
        /// Pixel test for collision
        /// </summary>
        /// <param name="gameEngine">Current game</param>
        /// <param name="collider">Entity collider</param>
        /// <param name="collided">Entity collided</param>
        void ImagePixelCollisionTest(GameEngine gameEngine, Entity collider, Entity collided)
        {

            PositionComponent colliderPositionComponent = (PositionComponent)collider.GetComponent(typeof(PositionComponent));
            ImageComponent colliderImageComponent = (ImageComponent)collider.GetComponent(typeof(ImageComponent));
            PositionComponent collidedPositionComponent = (PositionComponent)collided.GetComponent(typeof(PositionComponent));
            ImageComponent collidedImageComponent = (ImageComponent)collided.GetComponent(typeof(ImageComponent));

            //Find collision action
            Action<Entity, Entity, int, int> collision = null;
            if (collided.GetComponent(typeof(BunkerComponent)) != null) collision = OnCollisionBunker;
            if (collided.GetComponent(typeof(MissileComponent)) != null) collision = OnCollisionMissile;
            if (collided.GetComponent(typeof(SpaceShipComponent)) != null) collision = OnCollisionSpaceShip;
            if (collision == null) return;

            HealthComponent colliderHealthComponent = (HealthComponent)collider.GetComponent(typeof(HealthComponent));
            HealthComponent collidedHealthComponent = (HealthComponent)collided.GetComponent(typeof(HealthComponent));

            //pixel check
            for (int j = 0; j < colliderImageComponent.Image.Height; j++)
            {
                for (int i = 0; i < colliderImageComponent.Image.Width; i++)
                {
                    int x = (int)(colliderPositionComponent.PositionX + i - collidedPositionComponent.PositionX);
                    int y = (int)(colliderPositionComponent.PositionY + j - collidedPositionComponent.PositionY);
                    if (!(x < 0 || y < 0 || x >= collidedImageComponent.Image.Width || y >= collidedImageComponent.Image.Height))
                    {

                        if (colliderHealthComponent.HP <= 0 || collidedHealthComponent.HP <= 0)
                        {
                            return;
                        }

                        if (colliderImageComponent.Image.GetPixel(i, j) != Color.FromArgb(0, 255, 255, 255) &&
                                collidedImageComponent.Image.GetPixel(x, y) != Color.FromArgb(0, 255, 255, 255))
                        {
                            collision(collider, collided, x, y);
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Handle collision with bunker
        /// </summary>
        /// <param name="collider">Entity collider</param>
        /// <param name="bunker">Entity bunker collided</param>
        /// <param name="x">Collision x position</param>
        /// <param name="y">Collision y position</param>
        void OnCollisionBunker(Entity collider, Entity bunker, int x, int y)
        {
            HealthComponent colliderHealthComponent = (HealthComponent)collider.GetComponent(typeof(HealthComponent));
            ImageComponent imageBunkerComponent = (ImageComponent)bunker.GetComponent(typeof(ImageComponent));

            imageBunkerComponent.Image.SetPixel(x, y, Color.FromArgb(0, 255, 255, 255));
            colliderHealthComponent.HP--;
        }

        /// <summary>
        /// Handle collision with missile
        /// </summary>
        /// <param name="collider">Entity collider</param>
        /// <param name="missile">Entity missile collided</param>
        /// <param name="x">Collision x position</param>
        /// <param name="y">Collision y position</param>
        void OnCollisionMissile(Entity collider, Entity missile, int x, int y)
        {
            HealthComponent colliderHealthComponent = (HealthComponent)collider.GetComponent(typeof(HealthComponent));
            HealthComponent missileHealthComponent = (HealthComponent)missile.GetComponent(typeof(HealthComponent));

            colliderHealthComponent.HP = 0;
            missileHealthComponent.HP = 0;

        }

        /// <summary>
        /// Handle collision with spaceship
        /// </summary>
        /// <param name="collider">Entity collider</param>
        /// <param name="spaceShip">Entity spaceship collided</param>
        /// <param name="x">Collision x position</param>
        /// <param name="y">Collision y position</param>
        void OnCollisionSpaceShip(Entity collider, Entity spaceShip, int x, int y)
        {
            HealthComponent colliderHealthComponent = (HealthComponent)collider.GetComponent(typeof(HealthComponent));
            HealthComponent spaceShipHealthComponent = (HealthComponent)spaceShip.GetComponent(typeof(HealthComponent));

            colliderHealthComponent.HP = 0;
            spaceShipHealthComponent.HP--;
        }


        public override void update(GameEngine gameEngine, double deltaT)
        {
            HashSet<Entity> collidableEntities = gameEngine.entityManager.GetEntities(typeof(HitboxComponent));
            foreach (Entity collider in collidableEntities)
            {
                if (collider.GetComponent(typeof(OnCollisionComponent)) == null) continue;   // only missile has collision atm

                HitboxComponent colliderHitboxComponent = (HitboxComponent)collider.GetComponent(typeof(HitboxComponent));
                if (colliderHitboxComponent != null)
                {
                    PositionComponent colliderPositionComponent = (PositionComponent)collider.GetComponent(typeof(PositionComponent));
                    if (colliderPositionComponent == null) continue;

                    SideComponent colliderSideComponent = (SideComponent)collider.GetComponent(typeof(SideComponent));
                    if (colliderSideComponent == null) continue;

                    foreach (Entity collided in collidableEntities)
                    {
                        if (collided == collider) continue;

                        SideComponent collidedSideComponent = (SideComponent)collided.GetComponent(typeof(SideComponent));
                        if (collidedSideComponent == null || colliderSideComponent.Side == collidedSideComponent.Side) continue;

                        HitboxComponent collidedHitBoxComponent = (HitboxComponent)collided.GetComponent(typeof(HitboxComponent));
                        if (collidedHitBoxComponent == null) continue;

                        PositionComponent collidedPositionComponent = (PositionComponent)collided.GetComponent(typeof(PositionComponent));
                        if (collidedPositionComponent == null) continue;

                        if (TestHitboxCollision(colliderPositionComponent, colliderHitboxComponent, collidedPositionComponent, collidedHitBoxComponent))
                        {
                            ImageComponent colliderImageComponent = (ImageComponent)collider.GetComponent(typeof(ImageComponent));
                            ImageComponent collidedImageComponent = (ImageComponent)collided.GetComponent(typeof(ImageComponent));
                            if (colliderImageComponent == null || collidedImageComponent == null) continue;
                            ImagePixelCollisionTest(gameEngine, collider, collided);
                        }
                    }
                }
            }
        }

    }
}



