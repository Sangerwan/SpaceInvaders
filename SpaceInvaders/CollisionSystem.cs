using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace SpaceInvaders
{
    class CollisionSystem
    {

        public void update(GameEngine ge)
        {
            HashSet<Entity> entities = ge.entityManager.gameObjects;
            foreach(Entity a in entities)
            {
                if (a.GetComponent(typeof(CollisionComponent)) == null) continue;   // only missile has collision atm
                HitboxComponent hitboxA =(HitboxComponent) a.GetComponent(typeof(HitboxComponent));
                if (hitboxA != null)
                {
                    PositionComponent positionA = (PositionComponent)a.GetComponent(typeof(PositionComponent));
                    EntitySide.Side aSide = ((SideComponent)a.GetComponent(typeof(SideComponent))).Side;
                    foreach (Entity b in entities)
                    {
                        if (b == a) continue;

                        EntitySide.Side bSide = ((SideComponent)b.GetComponent(typeof(SideComponent))).Side;
                        if (aSide == bSide) continue;

                        HitboxComponent hitboxB = (HitboxComponent)b.GetComponent(typeof(HitboxComponent));
                        if (hitboxB == null) continue;
                        
                        PositionComponent positionB = (PositionComponent)b.GetComponent(typeof(PositionComponent));
                        if (TestHitboxCollision(positionA, hitboxA, positionB, hitboxB)) 
                        {                            
                            ImageComponent imageA = (ImageComponent)a.GetComponent(typeof(ImageComponent));
                            ImageComponent imageB = (ImageComponent)b.GetComponent(typeof(ImageComponent));                            
                            if (imageA == null || imageB == null) continue;

                            ImagePixelCollisionTest(ge, a, b);
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
            return AIsLeftOfB || AIsRightOfB || AIsAboveB || AIsBelowB;
        }

        void ImagePixelCollisionTest(GameEngine ge, Entity a, Entity b)
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

            for (int i = 0; i < imageA.Image.Height; i++)
            {
                for (int j = 0; j < imageA.Image.Width; j++)
                {
                    int x = (int)(positionA.PositionX + j - positionB.PositionX);
                    int y = (int)(positionA.PositionY + i - positionB.PositionY);
                    if (!(x < 0 || y < 0 || x >= imageB.Image.Width || y >= imageB.Image.Height))
                    {
                        //black
                        if (imageA.Image.GetPixel(i, j)== Color.FromArgb(0, 0, 0, 0) &&
                                imageB.Image.GetPixel(x, y)== Color.FromArgb(0, 0, 0, 0))
                        {
                            collision(a, b, x, y);
                            
                            HealthComponent aHp = (HealthComponent)a.GetComponent(typeof(HealthComponent));
                            HealthComponent bHp = (HealthComponent)b.GetComponent(typeof(HealthComponent));
                            if(aHp.Life<=0 || bHp.Life <= 0)
                            {
                                if (aHp.Life <= 0) ge.entityManager.gameObjects.Remove(a);
                                if (bHp.Life <= 0) ge.entityManager.gameObjects.Remove(b);
                                return;
                            }
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

            spaceShipHp.Life--;
        }
    }
}
        

    
