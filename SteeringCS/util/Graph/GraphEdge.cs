using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.Graph
{
    class GraphEdge
    {
        public GraphNode dest;
        public double cost;

        public GraphEdge(GraphNode d, double c)
        {
            dest = d;
            cost = c;
        }

        public override string ToString()
        {
            return this.dest.GetName() + "(" + this.cost + ")";
        }
    }
}
