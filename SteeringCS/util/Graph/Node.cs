using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.Graph
{
    class Node : FastPriorityQueueNode
    {
        /*----------------------------------------------------------------*/
        /*Variables-------------------------------------------------------*/
        /*----------------------------------------------------------------*/
        //constructor vars
        public string id;
        public Dictionary<string, Edge> adjecent_edges;
        public Vector2D position_of_node;

        //methods vars
        public double shortest_distance_to_dest;
        public Node previous_node_shortest_path;
        public bool handled_node;

        /*----------------------------------------------------------------*/
        /*Constructors----------------------------------------------------*/
        /*----------------------------------------------------------------*/
        public Node(string id, Vector2D position)
        {
            this.id = id;
            this.adjecent_edges = new Dictionary<string, Edge>();
            this.position_of_node = position;

            this.shortest_distance_to_dest = -1;
            this.previous_node_shortest_path = null;
            this.handled_node = false;
        }

        /*----------------------------------------------------------------*/
        /*Methods---------------------------------------------------------*/
        /*----------------------------------------------------------------*/
    }
}
