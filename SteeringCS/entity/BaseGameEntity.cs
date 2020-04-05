﻿using System.Drawing;

namespace SteeringCS
{
    abstract class BaseGameEntity
    {
        public Vector2D Pos { get; set; }
        private Vector2D _Dir;
        public Vector2D Dir
        {
            get
            {
                return this._Dir;
            }
            set
            {
                this._Dir = value.Normalize();
            }
        }

        public float Scale { get; set; }
        public World MyWorld { get; set; }

        public BaseGameEntity(Vector2D pos, World w)
        {
            Pos = pos;
            _Dir = new Vector2D(0,0);
            MyWorld = w;
        }

        public abstract void Update(float delta);

        public virtual void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Blue, new Rectangle((int) Pos.X,(int) Pos.Y, 10, 10));
        }

        public virtual void RenderDebug(Graphics g) { }

        public virtual void RenderDebugPanel(Graphics g, DBPanel p) { }
    }
}
