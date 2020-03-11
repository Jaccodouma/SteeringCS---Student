using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class FleeBehaviour : SteeringBehaviour
    {
        public BaseGameEntity target { get; set; }

        public FleeBehaviour(MovingEntity me, BaseGameEntity target) : base(me)
        {
            this.target = target;
        }

        public override Vector2D Calculate()
        {
            Vector2D desiredVelocity = ME.Pos.Clone();
            desiredVelocity.Sub(target.Pos).Normalize().Multiply(ME.MaxSpeed).Sub(ME.Velocity);
            return desiredVelocity;
        }
    }
}
