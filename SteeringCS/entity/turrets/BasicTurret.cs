using SteeringCS.FuzzyLogic;
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
            FuzzyVariable distance = fm.CreateFLV("DISTANCE");
            FzSet cDistance = distance.AddLeftShoulderSet("CLOSE", 0, 30, 50);
            FzSet mDistance = distance.AddTriangle("MEDIUM", 30, 50, 70);
            FzSet fDistance = distance.AddRightShoulderSet("FAR", 50, 70, 100);

            FuzzyVariable hp = fm.CreateFLV("HP");
            FzSet lhp = hp.AddLeftShoulderSet("LOW", 0, 30, 50);
            FzSet mhp = hp.AddTriangle("MEDIUM", 30, 50, 70);
            FzSet hhp = hp.AddLeftShoulderSet("HIGH", 50, 70, 100);

            FuzzyVariable desirebility = fm.CreateFLV("desirebility");
            FzSet undesire = desirebility.AddLeftShoulderSet("UNDESIRABLE", 0, 30, 50);
            FzSet ddesire = desirebility.AddTriangle("DESIRABLE", 30, 50, 70);
            FzSet vddesire = desirebility.AddRightShoulderSet("VERY DESIRABLE", 50, 70, 100);

            fm.Addrule(new FzAND(cDistance, lhp), vddesire);
            fm.Addrule(new FzAND(cDistance, mhp), ddesire);
            fm.Addrule(new FzAND(cDistance, hhp), ddesire);

            fm.Addrule(new FzAND(mDistance, lhp), vddesire);
            fm.Addrule(new FzAND(mDistance, mhp), ddesire);
            fm.Addrule(new FzAND(mDistance, hhp), ddesire);

            fm.Addrule(new FzAND(fDistance, lhp), vddesire);
            fm.Addrule(new FzAND(fDistance, mhp), ddesire);
            fm.Addrule(new FzAND(fDistance, hhp), undesire);

            fm.Addrule(new FzOR(cDistance, lhp), vddesire); 
        }

        private double CalculateDesireablity(double distance, double hp)
        {
            this.fm.Fuzzify("DISTANCE", distance);
            this.fm.Fuzzify("HP", hp);

            return fm.DeFuzzify("desirebility");
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
                double bestScore = -1;
                Zombie bestZombie = null;

                zombies.ForEach(z =>
                {

                    Vector2D distance = z.Pos - this.Pos;
                    double len = distance.LengthSquared();
                    //if (len < nearestZombieDistance.LengthSquared() && len < Math.Pow(range, 2))
                    //{
                    //    nearestZombie = (Zombie)z;
                    //    nearestZombieDistance = distance;
                    //}

                    if (len < Math.Pow(range, 2))
                    {
                        nearestZombie = (Zombie)z;
                        nearestZombieDistance = distance;
                        double score = CalculateDesireablity(distance.Length(), nearestZombie.GetHealth());
                        if(score > bestScore)
                        {
                            bestZombie = nearestZombie;
                            bestScore = score;
                        }
                        Console.WriteLine(score);
                    }
                });

                // Set target
                if (bestZombie != null)
                {
                    target = bestZombie;
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
