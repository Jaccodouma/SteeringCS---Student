﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    class Turret : BaseGameEntity
    {
        private int size = 20;
        private int range = 100; 
        private Zombie target;
        private int checkInterval = 10;
        private int currentCheck = 0; 

        public Turret(Vector2D pos, World w) : base(pos, w) { }

        public override void Update(float delta)
        {
            if (true) // CHECK INTERVAL
            {
                // Find nearest zombie
                List<MovingEntity> zombies = this.MyWorld.getZombies();
                Zombie nearestZombie = null;
                Vector2D nearestZombieDistance = new Vector2D(range, range);

                zombies.ForEach(z =>
                {
                    Vector2D distance = z.Pos - this.Pos;
                    double len = distance.LengthSquared();
                    if (len < nearestZombieDistance.LengthSquared() && len < Math.Pow(range,2))
                    {
                        Console.WriteLine((z.Pos - this.Pos).LengthSquared() + " - " + nearestZombieDistance.LengthSquared());
                        nearestZombie = (Zombie)z;
                        nearestZombieDistance = distance;
                    }
                });

                // Set target
                if (nearestZombie != null)
                {
                    target = nearestZombie;
                } else
                {
                    target = null; // Set target to null if out of range too
                }
            }

            if (target != null)
            {
                // Set direction
                Dir = (target.Pos - this.Pos).Normalize();
            } else
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