using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class WanderBehaviour : SteeringBehaviour
    {
        double wanderRadius;
        double wanderDistance;
        double wanderJitter;

        Vector2D wanderCircle; // Centre of the 'circle' the wanderTarget moves around
        Vector2D wanderTarget; // Deviation from the wanderCircle
        Vector2D worldTarget; // We save this to make drawing it easier

        Random rnd; 

        public WanderBehaviour(MovingEntity me, double wanderRadius, double wanderDistance, double wanderJitter) : base(me)
        {
            this.wanderRadius = wanderRadius;
            this.wanderDistance = wanderDistance;
            this.wanderJitter = wanderJitter;

            this.wanderCircle = new Vector2D();
            this.wanderTarget = me.Dir * wanderRadius;
            this.worldTarget = new Vector2D();

            rnd = me.MyWorld.rnd;
        }
        public override string ToString()
        {
            return "Wander";
        }

        public override Vector2D Calculate()
        {
            // WanderCircle Pos
            wanderCircle = ME.Pos + ME.Dir * wanderDistance;

            wanderTarget += new Vector2D((rnd.NextDouble() * 2 - 1) * wanderJitter, (rnd.NextDouble() * 2 - 1) * wanderJitter);
            wanderTarget = wanderTarget.Normalize() * wanderRadius;

            // Create wanderTarget ahead of entity        to ME    Ahead of ME  At wanderDistance
            worldTarget = wanderCircle + wanderTarget;

            Vector2D steeringForce = (worldTarget - ME.Pos).Normalize();
            this.lastSteeringForce = steeringForce;
            return steeringForce;
        }
        public override void Render(Graphics g)
        {
            // draw wander circle
            Pen p = new Pen(Color.Black,1);
            g.DrawEllipse(p, new Rectangle(
                (int)(wanderCircle.X-wanderRadius),
                (int)(wanderCircle.Y-wanderRadius),
                (int)wanderRadius*2, 
                (int)wanderRadius*2));

            // draw worldTarget
            g.DrawEllipse(p, new Rectangle(
                (int)(worldTarget.X - 2),
                (int)(worldTarget.Y - 2),
                (int)4,
                (int)4));

        }
    }
}
