using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    class Goal_Zombie_WanderWhileHealing: Goal
    {
        WanderBehaviour wander;
        Zombie zombie; 

        public Goal_Zombie_WanderWhileHealing(Zombie z)
            : base(z)
        {
            this.zombie = z; 
        }

        public override void Activate()
        {
            // Add wander behaviour
            wander = new WanderBehaviour(this.movingEntity, 70, 30, 20);
            this.movingEntity.SteeringBehaviours.Add(wander);
            this.status = GoalProcess.active;
        }
        public override GoalProcess Process()
        {
            // if we get close to a turret set status to failed
            foreach (TurretBase turret in TurretBase.turrets)
            {
                double distSqrd = (this.zombie.Pos - turret.Pos).LengthSquared();
                if (distSqrd > Math.Pow(turret.getRange(), 2)) { this.status = GoalProcess.failed; }
            }

            // if we are full health set status to completed
            if (this.zombie.GetHealth() >= this.zombie.getMaxHealth()) this.status = GoalProcess.completed;

            return this.status;
        }
        public override void Terminate()
        {
            // Remove wander behaviour
            this.status = GoalProcess.completed;
            this.movingEntity.SteeringBehaviours.Remove(wander);
        }
    }
}
