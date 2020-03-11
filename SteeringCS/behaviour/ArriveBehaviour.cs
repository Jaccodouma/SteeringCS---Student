using SteeringCS.entity;
using SteeringCS.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class ArriveBehaviour : SteeringBehaviour
    {
        public BaseGameEntity target { get; set; }
        public Deceleration deceleration { get; set; }

        public ArriveBehaviour(MovingEntity me, BaseGameEntity target, Deceleration deceleration) : base(me)
        {
            this.target = target;
            this.deceleration = deceleration;
        }

        public override Vector2D Calculate()
        {
            Vector2D ToTarget = target.Pos.Clone().Sub(ME.Pos);
            double distance = ToTarget.Length();

            if (distance > 0)
            {
                const double DecelerationTweaker = 0.4;

                double speed = distance / ((double)deceleration * DecelerationTweaker);

                if (speed > ME.MaxSpeed)
                {
                    speed = ME.MaxSpeed;
                }

                Vector2D DesiredVelocity = ToTarget.Multiply(speed/distance);
                return (DesiredVelocity.Sub(ME.Velocity));
            }
            return new Vector2D();
        }
    }
}
