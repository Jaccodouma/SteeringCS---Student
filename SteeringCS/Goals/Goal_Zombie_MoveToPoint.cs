using SteeringCS.entity;
using SteeringCS.util.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    class Goal_Zombie_MoveToPoint : CompositeGoal
    {
        private Zombie zombie;
        private Path_planning path_Planning;
        private Vector2D pos; 


        public Goal_Zombie_MoveToPoint(Zombie z, Path_planning pathPlanning, Vector2D pos)
            : base(z)
        {
            this.zombie = z;
            this.path_Planning = pathPlanning;
            this.pos = pos;
        }

        public override void Activate()
        {
            // Find path
            List<Vector2D> path = path_Planning.PathFromTo(zombie.Pos, pos);

            // Add traverse_edge subgoal for each edge
            for (int i = 0; i < path.Count; i++)
            {
                Vector2D from = i <= 0 ? zombie.Pos : path[i - 1];
                Vector2D to = i < path.Count ? path[i] : pos;

                this.AddSubgoal(new Goal_Zombie_TraverseEdge(zombie, from, to));
            }

            this.status = GoalProcess.active;
        }

        public override GoalProcess Process()
        {
            return this.ProcessSubgoals();
        }

        public override void Terminate()
        {
            this.RemoveAllSubgoals();
        }
    }
}
