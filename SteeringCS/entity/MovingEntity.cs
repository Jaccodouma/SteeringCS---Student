using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    abstract class MovingEntity : BaseGameEntity
    {
        public static List<MovingEntity> movingEntities = new List<MovingEntity>();
        public Vector2D Velocity { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }

        public List<SteeringBehaviour> SteeringBehaviours { get; set; }

        public MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 100;
            MaxSpeed = 50;
            Velocity = new Vector2D();
            SteeringBehaviours = new List<SteeringBehaviour>();
            movingEntities.Add(this);
        }

        public override void Update(float timeElapsed)
        {
            if (SteeringBehaviours.Count != 0)
            {
                // Update velocity
                Vector2D SteeringForce = this.Calculate();
                Vector2D acceleration = SteeringForce / Mass;
                Velocity += acceleration * timeElapsed;

                // Make sure velocity doesn't exceed max velocity
                Velocity.Truncate(MaxSpeed);

                // Update position
                if (Velocity.Length() > 0)
                {
                    this.Pos.Add(Velocity.Multiply(timeElapsed));
                    if (this.Pos.X < 0) this.Pos.X = 0;
                    if (this.Pos.Y < 0) this.Pos.Y = 0;
                    if (this.Pos.X > MyWorld.Width) this.Pos.X = MyWorld.Width;
                    if (this.Pos.Y > MyWorld.Height) this.Pos.Y = MyWorld.Height;

                    // Update heading (it normalises in the setter)
                    Dir = Velocity;
                }
            }
        }

        private Vector2D Calculate()
        {
            Vector2D SteeringForce = new Vector2D();

            // loop through SB list and add behavior * weight
            foreach(SteeringBehaviour SB in SteeringBehaviours)
            {
                SteeringForce += (SB.Calculate() * SB.weight);
            }

            return SteeringForce;
        }

        public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Blue, new Rectangle((int)Pos.X, (int)Pos.Y, 10, 10));
        }

        public override void RenderDebug(Graphics g) { }

        public new void Delete()
        {
            movingEntities.Remove(this);
            base.Delete();
        }
    }
}
