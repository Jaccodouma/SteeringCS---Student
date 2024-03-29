﻿using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class Group_CohesionBehaviour : SteeringBehaviour
    {
        public List<BaseGameEntity> targets { get; set; }
        public double neighbourhoodRadius;

        public Group_CohesionBehaviour(MovingEntity me, List<BaseGameEntity> targets) : base(me)
        {
            this.targets = targets;
            this.neighbourhoodRadius = 40;
            base.name = "Cohesion";
        }

        public override Vector2D Calculate()
        {
            // Get center of mass of all neigbouring agents
            Vector2D CenterOfMass = new Vector2D();
            Vector2D SteeringForce = new Vector2D();
            int neighbourCount = 0;
            foreach (BaseGameEntity target in targets.ToList())
            {
                if ((target.Pos - ME.Pos).Length() < neighbourhoodRadius && target != ME)
                {
                    CenterOfMass += target.Pos;
                    neighbourCount++;
                }
            }

            if (neighbourCount > 0)
            {
                CenterOfMass /= neighbourCount;

                SteeringForce = CenterOfMass - ME.Pos;
            }
            this.lastSteeringForce = SteeringForce;
            return SteeringForce;
        }

        public override void Render(Graphics g)
        {
            double leftCorner = ME.Pos.X - neighbourhoodRadius;
            double rightCorner = ME.Pos.Y - neighbourhoodRadius;
            double size = neighbourhoodRadius * 2;
            Pen p = new Pen(Color.FromArgb(125,0,255,0), 2);
            g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));

            foreach (BaseGameEntity target in targets)
            {
                if ((target.Pos - ME.Pos).Length() < neighbourhoodRadius && target != ME)
                {
                    g.DrawLine(p, new Point((int)ME.Pos.X, (int)ME.Pos.Y), new Point((int)target.Pos.X, (int)target.Pos.Y));
                }
            }
        }
    }
}
