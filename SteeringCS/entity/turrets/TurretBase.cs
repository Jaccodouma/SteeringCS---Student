﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    class TurretBase : BaseGameEntity
    {
        protected int range = 100;

        #region getters / setters
        public int getRange() { return this.range; }
        #endregion

        public static List<TurretBase> turrets = new List<TurretBase>();

        public TurretBase(Vector2D pos, World w) : base(pos, w)
        {
            turrets.Add(this);
        }

        public override void Update(float delta) { }

        public override void Render(Graphics g)
        {
            Pen p; 

            // Draw barrel
            p = new Pen(Color.RoyalBlue, 5);
            g.DrawLine(
                p,
                (int)Pos.X,
                (int)Pos.Y,
                (int)(Pos.X + Dir.X * 20),
                (int)(Pos.Y + Dir.Y * 20));
        }
    }
}
