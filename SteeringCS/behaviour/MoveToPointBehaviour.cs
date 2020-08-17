using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class MoveToPointBehaviour : SteeringBehaviour
    {
        public Vector2D target; 

        public MoveToPointBehaviour(MovingEntity me, Vector2D target) : base(me)
        {
            this.target = target;
            this.name = "MoveToPoint";
        }

        public override Vector2D Calculate()
        {

            Vector2D desiredVelocity = (target - ME.Pos).Normalize();
            this.lastSteeringForce = desiredVelocity;
            return desiredVelocity;
        }
    }
}
