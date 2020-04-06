using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class Group_SeperationBehaviour : SteeringBehaviour
    {
        public List<BaseGameEntity> targets { get; set; }
        public double neighbourhoodRadius { get; set; }

        public Group_SeperationBehaviour(MovingEntity me, List<BaseGameEntity> targets) : base(me)
        {
            this.targets = targets;
            this.neighbourhoodRadius = 40;
            this.name = "Seperation";
        }

        public override Vector2D Calculate()
        {
            Vector2D steeringForce = new Vector2D();

            foreach (BaseGameEntity target in targets)
            {
                if ((target.Pos - ME.Pos).Length() < neighbourhoodRadius && target != ME)
                {
                    Vector2D toAgent = ME.Pos - target.Pos;
                    steeringForce += (toAgent.Clone().Normalize() / toAgent.Length() )* 200;
                }
            }

            this.lastSteeringForce = steeringForce;
            return steeringForce;
        }

        public override void Render(Graphics g)
        {
            double leftCorner = ME.Pos.X - neighbourhoodRadius;
            double rightCorner = ME.Pos.Y - neighbourhoodRadius;
            double size = neighbourhoodRadius * 2;
            Pen p = new Pen(Color.FromArgb(125, 255, 0, 0), 2);
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
