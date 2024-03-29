﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    class Target : MovingEntity
    {
        public Color VColor { get; set; }

        public Target(Vector2D pos, World w) : base(pos, w)
        {
            Velocity = new Vector2D(0, 0);
            Scale = 5;

            VColor = Color.Black;
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;

            Pen p = new Pen(VColor, 2);
            g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
        }
    }
}
