using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    class Goal_Zombie_Attack : Goal
    {
        SeekBehaviour seek;
        Zombie zombie;

        public Goal_Zombie_Attack(Zombie z)
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

            // Find nearest turret
            TurretBase nearestTurret = TurretBase.turrets[0];
            double currentShortestDistance = Double.MaxValue;
            foreach (TurretBase turret in TurretBase.turrets)
            {
                double distSqrd = (this.zombie.Pos - turret.Pos).LengthSquared();
                if (distSqrd > currentShortestDistance)
                {
                    nearestTurret = turret;
                }
            }

            // Create seek behaviour towards the nearest turret
            seek = new SeekBehaviour(zombie, nearestTurret);

            // Add behaviour to zombie
            this.movingEntity.SteeringBehaviours.Add(seek);

            this.status = GoalProcess.active;
        }

        public override GoalProcess Process()
        {
            return status;
        }
        public override void Terminate()
        {
            // Remove behaviour
            this.movingEntity.SteeringBehaviours.Remove(seek);
        }
    }
}
