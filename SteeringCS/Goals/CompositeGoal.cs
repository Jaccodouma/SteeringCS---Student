﻿using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    class CompositeGoal : Goal
    {
        List<Goal> goals = new List<Goal>();

        public CompositeGoal(MovingEntity me) 
            : base(me) { }

        public new void Activate() { }
        public new GoalProcess Process()
        {
            return this.ProcessSubgoals();
        }
        public new void Terminate() { }
        public void AddSubgoal(Goal g)
        {
            // Insert new goal at the start of the list
            this.goals.Insert(0, g);
        }
        public GoalProcess ProcessSubgoals()
        {
            // Terminate and remove completed/failed goals from the front of the list
            while (this.goals.Count() > 0 &&
                (this.goals.First().IsCompleted() || this.goals.First().IsFailed()))
            {
                this.goals.First().Terminate();
                this.goals.RemoveAt(0);
            }

            // If there's any goals left, process the first 
            if (goals.Count > 0)
            {
                // Process the first goal 
                GoalProcess goalStatus = this.goals.First().Process();

                // We need to return "active" if there's more goals but the first one is completed
                if (goalStatus == GoalProcess.completed && this.goals.Count > 1) return GoalProcess.active;

                return goalStatus;

            }
            else
            {
                // No more subgoals, return completed
                return GoalProcess.completed;
            }
        }
        public void RemoveAllSubgoals()
        {
            // Terminate all goals
            foreach (Goal goal in goals)
            {
                goal.Terminate();
            }
            // Empty list
            this.goals.Clear();
        }
    }
}