using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    class Goal_Zombie_TraverseEdge : Goal
    {
        MoveToPointBehaviour behaviour;
        Zombie zombie;

        Vector2D from, to; 

        public Goal_Zombie_TraverseEdge(Zombie z, Vector2D from, Vector2D to)
            : base(z)
        {
            this.zombie = z;
            this.from = from;
            this.to = to; 
        }

        public override void Activate()
        {
            // Create seek behaviour towards the goal
            behaviour = new MoveToPointBehaviour(zombie, to);
            behaviour.weight = 10;
            this.movingEntity.SteeringBehaviours.Add(behaviour);

            this.status = GoalProcess.active;
        }

        public override GoalProcess Process()
        {
            // Set status to complete if near the goal 
            double distSqr = (zombie.Pos - to).LengthSquared();
            if (distSqr > Math.Pow(5,2))
            {
                this.status = GoalProcess.completed;
            }

            // TODO: Set status to failed if strayed too far from the path 

            return this.status;
        }

        public override void Terminate()
        {
            this.movingEntity.SteeringBehaviours.Remove(behaviour);
        }
    }
}
