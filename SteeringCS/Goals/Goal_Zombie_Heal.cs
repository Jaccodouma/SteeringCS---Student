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
        Zombie zombie; 
        public Goal_Zombie_Heal(Zombie z)
            : base(z)
        {
            this.zombie = z; 
        }

        public override void Activate()
        {
            this.status = GoalProcess.active;

            // Add multiple flee goals and one wanderheal 
            this.AddSubgoal(new Goal_Zombie_Flee(zombie));
            this.AddSubgoal(new Goal_Zombie_Flee(zombie));
            this.AddSubgoal(new Goal_Zombie_Flee(zombie));
            this.AddSubgoal(new Goal_Zombie_Flee(zombie));
            this.AddSubgoal(new Goal_Zombie_WanderWhileHealing(zombie));
        }

        public override GoalProcess Process()
        {
            return ProcessSubgoals();
        }
        public override void Terminate()
        {
            this.RemoveAllSubgoals();
        }
    }
}
