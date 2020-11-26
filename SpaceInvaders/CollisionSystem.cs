using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace SpaceInvaders
{
    class CollisionSystem:GameSystem
    {       

        public CollisionSystem(GameEngine gameEngine)
        {            
            /*HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> collidableEntities = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(OnCollisionComponent)) != null)
                    collidableEntities.Add(entity);
            }
            this.Entities = collidableEntities;*/
        }

        
        public void update2(GameEngine gameEngine)
        {
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            foreach(Entity a in entities)
            {
                if (a.GetComponent(typeof(OnCollisionComponent)) == null) continue;   // only missile has collision atm                

                HealthComponent healthA = (HealthComponent)a.GetComponent(typeof(HealthComponent));
                if (healthA == null || healthA.Life <= 0) continue;

                HitboxComponent hitboxA = (HitboxComponent)a.GetComponent(typeof(HitboxComponent));
                if (hitboxA != null)
                {
                    PositionComponent positionA = (PositionComponent)a.GetComponent(typeof(PositionComponent));
                    EntitySide.Side sideA = ((SideComponent)a.GetComponent(typeof(SideComponent))).Side;
                    foreach (Entity b in entities)
                    {
                        if (b == a) continue;

                        HealthComponent healthB = (HealthComponent)b.GetComponent(typeof(HealthComponent));
                        if (healthB == null || healthB.Life <= 0) continue;

                        HitboxComponent hitboxB = (HitboxComponent)b.GetComponent(typeof(HitboxComponent));
                        if (hitboxB == null) continue;

                        PositionComponent positionB = (PositionComponent)b.GetComponent(typeof(PositionComponent));
                        if (positionB == null) continue;

                        EntitySide.Side sideB = ((SideComponent)b.GetComponent(typeof(SideComponent))).Side;
                        if (sideA == sideB) continue;
                        
                        
                        
                        if (TestHitboxCollision(positionA, hitboxA, positionB, hitboxB)) 
                        {                            
                            ImageComponent imageA = (ImageComponent)a.GetComponent(typeof(ImageComponent));
                            ImageComponent imageB = (ImageComponent)b.GetComponent(typeof(ImageComponent));                            
                            if (imageA == null || imageB == null) continue;

                            ImagePixelCollisionTest(gameEngine, a, b);
                        }
                        
                        /*if (e.GetComponent(typeof(BunkerComponent)) != null) OnCollisionBunker(m, e);
                        else if (e.GetComponent(typeof(DamageComponent)) != null) OnCollisionMissile(m, e);
                        else if (e.GetComponent(typeof(SpaceShipComponent)) != null) OnCollisionSpaceShip(m, e);*/

                    }
                }
            }
        }

        bool TestHitboxCollision(PositionComponent positionA, HitboxComponent hitboxA, PositionComponent positionB, HitboxComponent hitboxB)
        {
            bool AIsLeftOfB = positionA.PositionX + hitboxA.Size.Width < positionB.PositionX;
            bool AIsRightOfB = positionA.PositionX > positionB.PositionX + hitboxB.Size.Width;
            bool AIsAboveB = positionA.PositionY + hitboxA.Size.Height < positionB.PositionY;
            bool AIsBelowB = positionA.PositionY > positionB.PositionY + hitboxB.Size.Height;
            return !(AIsLeftOfB || AIsRightOfB || AIsAboveB || AIsBelowB);
        }

        void ImagePixelCollisionTest(GameEngine gameEngine, Entity a, Entity b)
        {
            
            PositionComponent positionA = (PositionComponent)a.GetComponent(typeof(PositionComponent));            
            ImageComponent imageA = (ImageComponent)a.GetComponent(typeof(ImageComponent));
            PositionComponent positionB = (PositionComponent)b.GetComponent(typeof(PositionComponent));
            ImageComponent imageB = (ImageComponent)b.GetComponent(typeof(ImageComponent));
            Action<Entity,Entity, int ,int > collision = null;
            if (b.GetComponent(typeof(BunkerComponent)) != null) collision = OnCollisionBunker;
            if (b.GetComponent(typeof(MissileComponent)) != null) collision = OnCollisionMissile;
            if (b.GetComponent(typeof(SpaceShipComponent)) != null) collision = OnCollisionSpaceShip;

            if (collision == null) return;
            HealthComponent aHp = (HealthComponent)a.GetComponent(typeof(HealthComponent));
            HealthComponent bHp = (HealthComponent)b.GetComponent(typeof(HealthComponent));
            for (int j = 0; j < imageA.Image.Height; j++)
            {
                for (int i = 0; i < imageA.Image.Width; i++)
                {
                    int x = (int)(positionA.PositionX + i - positionB.PositionX);
                    int y = (int)(positionA.PositionY + j - positionB.PositionY);
                    if (!(x < 0 || y < 0 || x >= imageB.Image.Width || y >= imageB.Image.Height))
                    {
                        
                        if (aHp.Life <= 0 || bHp.Life <= 0)
                        {
                            /*if (aHp.Life <= 0) gameEngine.entityManager.GameObjects.Remove(a);
                            if (bHp.Life <= 0) gameEngine.entityManager.GameObjects.Remove(b);*/
                            return;
                        }

                        if (imageA.Image.GetPixel(i, j)== Color.FromArgb(255, 0, 0, 0) &&
                                imageB.Image.GetPixel(x, y)!= Color.FromArgb(0, 255, 255, 255))
                        {
                            
                            collision(a, b, x, y);                            
                            
                        }
                    }
                }
            }
            
        }
        

        void OnCollisionBunker(Entity missile, Entity bunker, int x, int y)
        {
            ImageComponent imageBunker = (ImageComponent)bunker.GetComponent(typeof(ImageComponent));
            HealthComponent missileHp = (HealthComponent)missile.GetComponent(typeof(HealthComponent));

            imageBunker.Image.SetPixel(x, y, Color.FromArgb(0, 255, 255, 255));
            missileHp.Life--;
        }

        void OnCollisionMissile(Entity missile, Entity otherMissile, int x, int y)
        {
            HealthComponent missileHp = (HealthComponent)missile.GetComponent(typeof(HealthComponent));            
            HealthComponent otherMissileHp = (HealthComponent)otherMissile.GetComponent(typeof(HealthComponent));

            missileHp.Life = 0;
            otherMissileHp.Life = 0;
            
        }

        void OnCollisionSpaceShip(Entity missile, Entity spaceShip, int x, int y)
        {
            HealthComponent spaceShipHp = (HealthComponent)spaceShip.GetComponent(typeof(HealthComponent));
            HealthComponent missileHp = (HealthComponent)missile.GetComponent(typeof(HealthComponent));
            missileHp.Life = 0;
            spaceShipHp.Life--;
        }

        public override void update(GameEngine gameEngine, double deltaT)
        {
            HashSet<Entity> collidableEntities = getEntities(gameEngine);
            foreach (Entity missile in collidableEntities)
            {
                if (missile.GetComponent(typeof(OnCollisionComponent)) == null) continue;   // only missile has collision atm
                HitboxComponent missileHitbox = (HitboxComponent)missile.GetComponent(typeof(HitboxComponent));
                if (missileHitbox != null)
                {
                    PositionComponent missilePosition = (PositionComponent)missile.GetComponent(typeof(PositionComponent));
                    EntitySide.Side missileSide = ((SideComponent)missile.GetComponent(typeof(SideComponent))).Side;
                    foreach (Entity entity in collidableEntities)
                    {
                        if (entity == missile) continue;

                        EntitySide.Side entitySide = ((SideComponent)entity.GetComponent(typeof(SideComponent))).Side;
                        if (missileSide == entitySide) continue;

                        HitboxComponent entityHitbox = (HitboxComponent)entity.GetComponent(typeof(HitboxComponent));
                        if (entityHitbox == null) continue;

                        PositionComponent entityPosition = (PositionComponent)entity.GetComponent(typeof(PositionComponent));
                        if (entityPosition == null) continue;

                        if (TestHitboxCollision(missilePosition, missileHitbox, entityPosition, entityHitbox))
                        {
                            ImageComponent missileImage = (ImageComponent)missile.GetComponent(typeof(ImageComponent));
                            ImageComponent entityImage = (ImageComponent)entity.GetComponent(typeof(ImageComponent));
                            if (missileImage == null || entityImage == null) continue;

                            ImagePixelCollisionTest(gameEngine, missile, entity);
                        }
                    }
                }
            }
        }

        protected override HashSet<Entity> getEntities(GameEngine gameEngine)
        {
            HashSet<Entity> entities = gameEngine.entityManager.GameObjects;
            HashSet<Entity> collidableEntities = new HashSet<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity.GetComponent(typeof(HitboxComponent)) != null)
                    collidableEntities.Add(entity);
            }
            return collidableEntities;
        }
    }
}
        

    
