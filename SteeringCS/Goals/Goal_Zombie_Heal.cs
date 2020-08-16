using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    class Goal_Zombie_Heal : CompositeGoal
    {
        public Goal_Zombie_Heal(MovingEntity me)
            : base(me) { }
        public new GoalProcess Process()
        {
            return status;
        }
    }
}
