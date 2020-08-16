﻿using SteeringCS.FuzzyLogic;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SteeringCS.entity
{
    class BasicTurret : TurretBase
    {
        FuzzyModule fm;

        private int size = 20;
        private Zombie target;
        private int checkInterval = 10;
        private int currentCheck = 0;
        private float timeBetweenShots = 10;
        private float nextShotTime = 0;

        public BasicTurret(Vector2D pos, World w) : base(pos, w) 
        {
            fm = new FuzzyModule();
            FuzzyVariable distance = fm.CreateFLV("distance");
            FuzzySet cDistance = distance.AddLeftShoulderSet("c", 0, 2000, 10000);
            FuzzySet mDistance = distance.AddTriangle("m", 2000, 10000, 18000);
            FuzzySet fDistance = distance.AddRightShoulderSet("f", 10000, 18000, 20000);

            FuzzyVariable hp = fm.CreateFLV("hp");
            FuzzySet lhp = hp.AddLeftShoulderSet("l", 0, 10, 40);
            FuzzySet mhp = hp.AddTriangle("m", 10, 40, 60);
            FuzzySet hhp  = hp.AddLeftShoulderSet("h", 40, 60, 100);

            FuzzyVariable desirebility = fm.CreateFLV("desirebility");
            FuzzySet uddesire = desirebility.AddLeftShoulderSet("ud", 0, 10, 40);
            FuzzySet ddesire = desirebility.AddTriangle("d", 10, 40, 60);
            FuzzySet vddesire = desirebility.AddRightShoulderSet("vd", 40, 60, 100);
        }

        public override void Update(float delta)
        {
            if (currentCheck == 0) // CHECK INTERVAL
            {
                // Find nearest zombie
                // TODO: REPLACE WITH FUZZY LOGIC
                List<BaseGameEntity> zombies = Zombie.zombies;
                Zombie nearestZombie = null;
                Vector2D nearestZombieDistance = new Vector2D(range, range);

                zombies.ForEach(z =>
                {
                    Vector2D distance = z.Pos - this.Pos;
                    double len = distance.LengthSquared();
                    if (len < nearestZombieDistance.LengthSquared() && len < Math.Pow(range, 2))
                    {
                        nearestZombie = (Zombie)z;
                        nearestZombieDistance = distance;
                    }
                });

                // Set target
                if (nearestZombie != null)
                {
                    target = nearestZombie;
                }
                else
                {
                    target = null; // Set target to null if out of range too
                }
            }
            currentCheck++;
            currentCheck %= checkInterval;

            if (target != null)
            {
                // Set direction
                Dir = (target.Pos - this.Pos).Normalize();

                // Shoot
                if (this.MyWorld.time > nextShotTime)
                {
                    target.doDamage(10);
                    nextShotTime += timeBetweenShots;
                }
            }
            else
            {
                // If no target, just rotate as 'scan' 
                Dir = Dir.Rotate(2);
            }
        }

        public override void Render(Graphics g)
        {
            Pen p;

            if (target != null)
            {
                p = new Pen(Color.FromArgb(150, 255, 0, 0), 1);
                // Draw target 'laser' 
                g.DrawLine(
                    p,
                    (int)Pos.X,
                    (int)Pos.Y,
                    (int)target.Pos.X,
                    (int)target.Pos.Y);
            }

            // Draw barrel
            p = new Pen(Color.RoyalBlue, 5);
            g.DrawLine(
                p,
                (int)Pos.X,
                (int)Pos.Y,
                (int)(Pos.X + Dir.X * size),
                (int)(Pos.Y + Dir.Y * size));

            // Draw circle
            g.FillEllipse(
                Brushes.DarkBlue,
                new Rectangle(
                    (int)(Pos.X - 0.5 * size),
                    (int)(Pos.Y - 0.5 * size),
                    size,
                    size));
        }
    }
}