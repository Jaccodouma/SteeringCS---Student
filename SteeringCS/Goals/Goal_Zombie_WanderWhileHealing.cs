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
        Group_SeperationBehaviour seperate;
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

            // Add seperation behaviour
            List<BaseGameEntity> turrets = TurretBase.turrets.ConvertAll(x => (BaseGameEntity)x);
            seperate = new Group_SeperationBehaviour(this.zombie, turrets);
            seperate.neighbourhoodRadius = 120;
            this.movingEntity.SteeringBehaviours.Add(seperate);

            this.status = GoalProcess.active;
        }
        public override GoalProcess Process()
        {
            // if we are full health set status to completed
            if (this.zombie.GetHealth() >= this.zombie.getMaxHealth()) this.status = GoalProcess.completed;

            return this.status;
        }
        public override void Terminate()
        {
            // Remove behaviours
            this.movingEntity.SteeringBehaviours.Remove(wander);
            this.movingEntity.SteeringBehaviours.Remove(seperate);
        }
    }
}
