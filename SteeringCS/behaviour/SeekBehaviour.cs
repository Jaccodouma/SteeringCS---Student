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
            this.name = "Seek";
        }

        public override Vector2D Calculate()
        {
            Vector2D desiredVelocity = (target.Pos - ME.Pos).Normalize();
            this.lastSteeringForce = desiredVelocity;
            return desiredVelocity;
        }

    }
}
