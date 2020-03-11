using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class EvadeBehaviour : SteeringBehaviour
    {
        public BaseGameEntity target { get; set; }

        public EvadeBehaviour(MovingEntity me, BaseGameEntity target) : base(me)
        {
            this.target = target;
        }

        public override Vector2D Calculate()
        {
            throw new NotImplementedException();
        }
    }
}
