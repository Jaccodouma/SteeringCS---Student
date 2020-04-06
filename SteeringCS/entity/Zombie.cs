using SteeringCS.behaviour;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    class Zombie : MovingEntity
    {
        public static List<BaseGameEntity> zombies = new List<BaseGameEntity>();

        public Color VColor { get; set; }
        private double health; 
        private double maxHealth = 100;

        public Zombie(Vector2D pos, World w) : base(pos, w)
        {
            Velocity = new Vector2D(0, 0);
            Dir = new Vector2D(w.rnd.Next(-1, 1), w.rnd.Next(-1, 1)).Normalize();
            Scale = 5;
            health = 100;

            VColor = Color.DarkGreen;

            Group_CohesionBehaviour coh = new Group_CohesionBehaviour(this, Zombie.zombies);
            SteeringBehaviours.Add(coh);

            Group_SeperationBehaviour sep = new Group_SeperationBehaviour(this, Zombie.zombies);
            SteeringBehaviours.Add(sep);

            WanderBehaviour wander = new WanderBehaviour(this, 70, 50, 10);
            SteeringBehaviours.Add(wander);

            Group_CohesionBehaviour findTurret = new Group_CohesionBehaviour(this, TurretBase.turrets);
            findTurret.neighbourhoodRadius = 150;
            SteeringBehaviours.Add(findTurret);

            zombies.Add(this);
        }
        
        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;

            Vector2D v_front        = this.Pos + (this.Dir.Clone() * size);
            Vector2D v_backLeft     = this.Pos + (this.Dir.Clone().Rotate(140) * size);
            Vector2D v_back         = this.Pos + (this.Dir.Clone() * -1 * size/2);
            Vector2D v_backRight    = this.Pos + (this.Dir.Clone().Rotate(220) * size);

            Point front = new Point((int)v_front.X, (int)v_front.Y);
            Point backLeft = new Point((int)v_backLeft.X, (int)v_backLeft.Y);
            Point back = new Point((int)v_back.X, (int)v_back.Y);
            Point backRight = new Point((int)v_backRight.X, (int)v_backRight.Y);

            Pen p = new Pen(VColor, 2);
            g.DrawPolygon(
                p,
                new Point[] {
                    front, backLeft, back, backRight
                });

            // Draw health
            p = new Pen(Color.Red, 3);
            g.DrawLine(
                p,
                (float)this.Pos.X - 10,
                (float)(this.Pos.Y - size * 1.5),
                (float)this.Pos.X + 10,
                (float)(this.Pos.Y - size * 1.5)
                );
            p = new Pen(Color.Green, 3);
            g.DrawLine(
                p,
                (float)this.Pos.X - 10,
                (float)(this.Pos.Y - size * 1.5),
                (float)(this.Pos.X - 10 + health/maxHealth*20),
                (float)(this.Pos.Y - size * 1.5)
                );
        }

        public override void RenderDebug(Graphics g)
        {
            Pen p_bl = new Pen(Color.Black, 1);
            Pen p_gr = new Pen(Color.Green, 1);
            Pen p_rd = new Pen(Color.Red, 1);
            // Velocity lines
            //g.DrawLine(p_bl, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 20), (int)Pos.Y + (int)(Velocity.Y * 20));
            try
            {
                g.DrawLine(p_rd, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 20), (int)Pos.Y);
                g.DrawLine(p_gr, (int)Pos.X, (int)Pos.Y, (int)Pos.X, (int)Pos.Y + (int)(Velocity.Y * 20));
            } catch (OverflowException)
            {
                // Happens when drawing line to outside of the screen
            }
        }

        public override void RenderDebugPanel(Graphics g, DBPanel p)
        {
            int x = 0, y = 0;
            this.SteeringBehaviours.ForEach(b =>
            {
                b.RenderInfoPanel(g, x, y);
                y += 100;
            });
        }

        public void doDamage(double damage)
        {
            health -= damage;

            if (health < 0)
            {
                // Delete
                //this.MyWorld.removeEntity(this);
                Delete();
            }
        }

        public new void Delete()
        {
            zombies.Remove(this);
            base.Delete();
        }
    }
}
