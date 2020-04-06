using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity.turrets
{
    class Base : TurretBase
    {
        private int width = 50, height = 35;

        public Base(Vector2D pos, World w) : base(pos, w)
        {
            // Create turrets
            new BasicTurret(new Vector2D(Pos.X-0.5*width, Pos.Y-0.5*height), this.MyWorld);
            new BasicTurret(new Vector2D(Pos.X-0.5*width, Pos.Y+0.5*height), this.MyWorld);
            new BasicTurret(new Vector2D(Pos.X+0.5*width, Pos.Y-0.5*height), this.MyWorld);
            new BasicTurret(new Vector2D(Pos.X+0.5*width, Pos.Y+0.5*height), this.MyWorld);
        }

        public override void Update (float delta) { }

        public override void Render(Graphics g)
        {
            Pen p = new Pen(Color.DarkBlue, 3);
            g.DrawRectangle(
                p, 
                new Rectangle(
                    (int) (Pos.X - 0.5 * width),
                    (int) (Pos.Y - 0.5 * height),
                    width, 
                    height));
        }

    }
}
