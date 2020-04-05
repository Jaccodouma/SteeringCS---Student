using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS
{
    abstract class SteeringBehaviour
    {
        public MovingEntity ME { get; set; }
        public double weight { get; set; }
        public abstract Vector2D Calculate();
        public Vector2D lastSteeringForce = new Vector2D(); // Used to draw in debug
        protected String name; 

        public SteeringBehaviour(MovingEntity me) : this(me, 1) { }
        public SteeringBehaviour(MovingEntity me, double weight)
        {
            this.ME = me;
            this.weight = weight;
            this.name = "";
        }

        public override string ToString()
        {
            return name;
        }

        public virtual void Render(Graphics g)
        {
            // Used for debugging
        }

        public virtual void RenderInfoPanel(Graphics g, int x, int y)
        {
            // Write name
            Font f = new Font("Arial", 10);
            SolidBrush b = new SolidBrush(Color.Black);
            g.DrawString(this.ToString(), f, b, x, y, new StringFormat());

            // Draw circle
            RenderSteeringForce(g, x+50, y+50, 40);
        }

        // Renders a circle for steering force
        public virtual void RenderSteeringForce(Graphics g, int x, int y, int r)
        {
            Vector2D sf = this.lastSteeringForce.Clone();
            sf.Normalize();

            // Draw point in the middle
            Pen p_blk = new Pen(Color.Black, 1);
            Pen p_red = new Pen(Color.Red, 1);
            Pen p_grn = new Pen(Color.Green, 1);
            SolidBrush b = new SolidBrush(Color.FromArgb(150,Color.Purple));
            Pen p_prp = new Pen(b);

            g.FillEllipse(new SolidBrush(Color.Black), new Rectangle(x - 4, y - 4, 8, 8));

            // Draw circle for 1
            g.DrawEllipse(p_blk, new Rectangle(x - r, y - r, 2*r, 2*r));

            // Draw forces
            g.DrawLine(p_red,
                x,
                y,
                x + (float)sf.X * r,
                y);
            g.DrawLine(p_grn,
                x,
                y,
                x,
                y + (float)sf.Y * r);
            g.DrawLine(p_blk,
                x,
                y,
                x + (float)sf.X * r,
                y + (float)sf.Y * r);

            // Draw current direction
            Vector2D pos = ME.Dir.Clone() * r;
            pos += new Vector2D(x,y);
            g.FillEllipse(b, (float)pos.X-4, (float)pos.Y-4, 8, 8);
            g.DrawLine(p_prp,
                x,
                y,
                (float)pos.X,
                (float)pos.Y);
        }
    }
}
