using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class SeekBehaviour : SteeringBehaviour
    {
        public BaseGameEntity target { get; set; }

        public SeekBehaviour(MovingEntity me, BaseGameEntity target) : base(me)
        {
            this.target = target;
        }

        public override Vector2D Calculate()
        {
            Vector2D desiredVelocity = target.Pos.Clone().Sub(ME.Pos);
            desiredVelocity.Normalize();
            desiredVelocity.Multiply(ME.MaxSpeed);
            desiredVelocity.Sub(ME.Velocity);
            return desiredVelocity;
        }

    }
}
