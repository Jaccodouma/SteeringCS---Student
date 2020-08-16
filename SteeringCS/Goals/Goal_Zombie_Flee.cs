using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    class Goal_Zombie_Flee : Goal
    {
        public Goal_Zombie_Flee(MovingEntity me) 
            : base(me) { }
        public override GoalProcess Process()
        {
            return status;
        }
    }
}
