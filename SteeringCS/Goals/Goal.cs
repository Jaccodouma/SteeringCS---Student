using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    enum GoalProcess
    {
        inactive,
        active,
        completed,
        failed
    }

    class Goal
    {
        protected MovingEntity movingEntity;
        protected GoalProcess status = GoalProcess.inactive;

        #region status check functions
        public bool IsInactive()    { return this.status == GoalProcess.inactive; }
        public bool IsActive()      { return this.status == GoalProcess.active; }
        public bool IsCompleted()   { return this.status == GoalProcess.completed; }
        public bool IsFailed()      { return this.status == GoalProcess.failed; }
        #endregion

        public Goal(MovingEntity me)
        {
            this.movingEntity = me; 
        }

        public virtual void Activate() { }

        public virtual GoalProcess Process()
        {
            // Throw exception if Process() is not overridden 
            Console.WriteLine(this.GetType().Name + " Process() NOT IMPLEMENTED!");
            throw new NotImplementedException();
        }

        public virtual void Terminate() { }
    }
}
