﻿using GXPEngine.Entities;
using GXPEngine.Extras;
using GXPEngine.StageManagement;

namespace GXPEngine.SpecialObjects.Collectibles
{
    public class Powerup : Collectible
    {
        protected Timer useTimer;
        protected Powerup(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames)
        {
            SetCycle(0,4,7);
            
        }

        protected override void Action()
        {
            myGame.player.currentPowerup = this;
        }

        public virtual void Use()
        {
            
        }
    }

    public class FireBulletShooter : Powerup
    {
        private readonly float speed;
        private readonly int coolDown;
        private readonly int damage;

        
        public FireBulletShooter() : base("sprites/collectibles/powerups/fire_pickup_big.png", 4, 1, 4)
        {
            SetXY(myGame.player.x,myGame.player.y);
            visible = true;

            speed = 1f;
            coolDown = 1000;
            damage = 1;
        }

        public override void Use()
        {
            if (useTimer == null || useTimer.finished)
            {
                useTimer = new Timer(coolDown);

                StageLoader.AddObjectAtLayer(new FireBullet(speed, damage, myGame.player.mirrored),1);
            }
        }
    }
}