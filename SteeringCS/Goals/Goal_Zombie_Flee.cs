using SteeringCS.behaviour;
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
        Group_SeperationBehaviour seperate;
        Zombie zombie; 

        public Goal_Zombie_Flee(Zombie z) 
            : base(z)
        {
            this.zombie = z;
        }

        public override void Activate()
        {
            // Fail if no turrets found 
            if (TurretBase.turrets.Count <= 0)
            {
                this.status = GoalProcess.failed;
                return;
            }

            // Create behaviour away from the nearest turret
            List<BaseGameEntity> turrets = TurretBase.turrets.ConvertAll(x => (BaseGameEntity)x);
            seperate = new Group_SeperationBehaviour(this.zombie, turrets);
            seperate.neighbourhoodRadius = 120;

            // Add behaviour to zombie
            this.movingEntity.SteeringBehaviours.Add(seperate);

            this.status = GoalProcess.active;
        }

        public override GoalProcess Process()
        {
            // Randomly complete, if we still need to flee we'll get the behaviour again
            if (new Random().Next(100) <= 5) this.status = GoalProcess.completed;
            return status;
        }

        public override void Terminate()
        {
            // Remove behaviour
            this.movingEntity.SteeringBehaviours.Remove(seperate);
        }
    }
}
