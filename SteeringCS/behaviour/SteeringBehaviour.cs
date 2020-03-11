using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS
{
    abstract class SteeringBehaviour
    {
        public MovingEntity ME { get; set; }
        public double weight { get; set; }
        public abstract Vector2D Calculate();

        public SteeringBehaviour(MovingEntity me)
        {
            this.ME = me;
            this.weight = 1; 
        }
        public SteeringBehaviour(MovingEntity me, double weight)
        {
            this.ME = me;
            this.weight = weight; 
        }

        public virtual void Render(Graphics g)
        {
            // Used for debugging
        }
    }

    
}
