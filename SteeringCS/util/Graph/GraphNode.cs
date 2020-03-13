using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.Graph
{
    class GraphNode : FastPriorityQueueNode
    {
        public static readonly double INFINITY = System.Double.MaxValue;
        private Vector2D name;
        public List<GraphEdge> adj;

        //smallest distance from start vertex to this vertex
        private double distance;
        //previous vertex on shortest path
        private GraphNode previous;
        //indicator whether node is already known in dijkstra
        private bool known;
        /*------------------------------------------------------------*/
        //Constructor
        public GraphNode(Vector2D name)
        {
            this.name = name;
            this.known = false;
            this.previous = null;
            this.distance = INFINITY;
            this.adj = new List<GraphEdge>();
        }
        /*------------------------------------------------------------*/
        //Getters
        public Vector2D GetName()
        {
            return this.name;
        }

        public GraphNode GetPrevious()
        {
            return previous;
        }

        public double GetDistance()
        {
            return this.distance;
        }

        public Boolean GetKnown()
        {
            return this.known;
        }
        //Setters
        public void SetName(Vector2D name)
        {
            this.name = name;
        }

        public void SetPrevious(GraphNode prev)
        {
            this.previous = prev;
        }

        public void SetDistance(double dist)
        {
            this.distance = dist;
        }

        public void SetKnown(Boolean known)
        {
            this.known = known;
        }
        /*------------------------------------------------------------*/
        //methods
        public void Reset()
        {
            this.known = false;
            this.previous = null;
            this.distance = INFINITY;
        }

        public override string ToString()
        {
            string str = this.GetName() + "\t[";
            foreach (GraphEdge e in adj)
            {
                str += e.dest.GetName() + "{" + Math.Round(e.cost,2) + "}\t\t";
            }

            return str + "]";
        }
    }
}
