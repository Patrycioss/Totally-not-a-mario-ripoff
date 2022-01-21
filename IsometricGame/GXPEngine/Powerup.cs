﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GXPEngine.Core;
using GXPEngine.Enemies;
using GXPEngine.Managers;

namespace GXPEngine
{
    public class Powerup : Collectible
    {
        protected Timer useTimer;
        public Powerup(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames)
        {
        }

        protected override void Action()
        {
            _myGame.player.currentPowerup = this;
        }

        public virtual void Use()
        {
            
        }
    }

    public class FireBulletShooter : Powerup
    {
        public float speed;
        public int coolDown;
        public int damage;
        private FireBullet _fireBullet;
        
        public FireBulletShooter() : base("sprites/collectibles/powerups/fire_pickup_big.png", 4, 1, 4)
        {
            SetXY(_myGame.player.x,_myGame.player.y);
            visible = true;

            speed = 0.5f;
            coolDown = 1000;
            damage = 1;
        }

        public override void Use()
        {
            if (useTimer == null || useTimer.finished)
            {
                useTimer = new Timer(coolDown);
                
                _fireBullet = new FireBullet(speed, damage,_myGame.player.mirrored);
                _myGame.StageLoader.stageContainer.AddChild(_fireBullet);
            }
        }
        
    }

    public class FireBullet : Entity
    {
        private Collision _collision;
        
        public int bulletOffset;

        private ParticleGenerator _particleGenerator;
        
        public FireBullet(float speed, int damage, bool mirrored) : base("sprites/collectibles/powerups/fire_bullet.png", 4, 1, 4)
        {
            collider.isTrigger = true;
            
            SetScaleXY(2);

            bulletOffset = 5;
            attackDamage = damage;
            
            if (mirrored)
            {
                Mirror(true, false);
                SetXY(_myGame.player.x - bulletOffset - _myGame.player.width, _myGame.player.y + 10);
                this.speed = -speed;
            }
            else
            {
                this.speed = speed;
                SetXY(_myGame.player.x + _myGame.player.width + bulletOffset, _myGame.player.y + 10);

            } 
        }

        public override void Update()
        {
            _collision = MoveUntilCollision(speed * Time.deltaTime, 0, true);
            
            
            AnimateFixed();
            
            if (_collision != null)
            {
                if (_collision.other != _myGame.player)
                {
                    
                    _collision.other.Damage(attackDamage);
                }
                Kill();
            }
 
            foreach (GameObject gameObject in GetCollisions())
            {
                if (gameObject is Enemy)
                {
                    gameObject.Damage(attackDamage);
                    Explode();

                    break;
                }
            }


            if (DistanceTo(_myGame.player) > _myGame.width)
            {
                Kill();
            }
        }

        private void Explode()
        {
            this.alpha = 0;

            _particleGenerator = new ParticleGenerator(Color.DarkOrange, 100);
            



        }
        
        
    }
}