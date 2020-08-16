using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    class Goal_Zombie_Wander : Goal
    {
        WanderBehaviour wander; 

        public Goal_Zombie_Wander(MovingEntity me)
            : base(me) { }

        public override void Activate()
        {
            // Add wander behaviour
            wander = new WanderBehaviour(this.movingEntity, 70, 30, 20);
            this.movingEntity.SteeringBehaviours.Add(wander);
            this.status = GoalProcess.active;
        }
        public override GoalProcess Process()
        {
            // 1 in 10 chance to complete, just so it doesn't keep wandering 
            if (new Random().Next(10) <= 0) this.status = GoalProcess.completed;

            return this.status;
        }
        public override void Terminate()
        {
            // Remove wander behaviour
            this.movingEntity.SteeringBehaviours.Remove(wander);
        }
    }
}
