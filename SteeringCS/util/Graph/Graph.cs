using System;
using System.Collections.Generic;
using Priority_Queue;

namespace SteeringCS.util.Graph
{
    class Graph
    {
        /*------------------------------------------------------------------------------------------*/
        /*Variables---------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------*/
        public int INFINITY = -1;
        public Dictionary<string, Node> nodeMap = new Dictionary<string, Node>();


        /*------------------------------------------------------------------------------------------*/
        /*Constructors------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------*/

        /*------------------------------------------------------------------------------------------*/
        /*Methods-----------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------*/

        /*----------------------------------------------------------------*/
        /*Adding-Edges----------------------------------------------------*/
        /*----------------------------------------------------------------*/
        public void Add_Edge(string source_name, string dest_name)
        {
            Node source = Get_node(source_name);
            Node dest = Get_node(dest_name);
            string id = dest.position_of_node.X.ToString() + "," + dest.position_of_node.Y.ToString();

            if (source == null || dest == null) return;

            source.adjecent_edges.Add(id, new Edge(nodeMap[source_name],nodeMap[dest_name]));
        }

        public void Add_Edge(string source_name, Vector2D position_source, string dest_name, Vector2D position_dest)
        {
            Node source = Get_node(source_name, position_source);
            Node dest = Get_node(dest_name, position_dest);
            string id = dest.position_of_node.X.ToString() + "," + dest.position_of_node.Y.ToString();

            if (source == null || dest == null) return;

            source.adjecent_edges.Add(id, new Edge(nodeMap[source_name], nodeMap[dest_name]));
        }

        /*----------------------------------------------------------------*/
        /*Node-getting-&-generation---------------------------------------*/
        /*----------------------------------------------------------------*/

        public Node Get_node(string node_name)
        {
            if (nodeMap.ContainsKey(node_name)) return nodeMap[node_name];
            return null;  
        }

        public Node Get_node(string node_name, Vector2D position)
        {
            if (nodeMap.ContainsKey(node_name)) return nodeMap[node_name];
            nodeMap.Add(node_name, new Node(node_name, position));
            return nodeMap[node_name];
        }

        private void _Flood_fill(int x, int y)
        {

        }
        /*----------------------------------------------------------------*/
        /*Path-finding-algorithms-----------------------------------------*/
        /*----------------------------------------------------------------*/

        public void Breath_first_search(Node current_position)
        {
            FastPriorityQueue<Node> queue = new FastPriorityQueue<Node>(nodeMap.Count);

            foreach(KeyValuePair<string, Node> node in nodeMap)
            {
                node.Value.shortest_distance_to_dest = INFINITY;
            }

            current_position.shortest_distance_to_dest = 0;
            queue.Enqueue(current_position, 1);

            while(queue.Count > 0)
            {
                Node node = queue.Dequeue();
                foreach(KeyValuePair<string,Edge> n in node.adjecent_edges)
                {
                    if(n.Value.destination.shortest_distance_to_dest == INFINITY)
                    {
                        n.Value.destination.shortest_distance_to_dest = node.shortest_distance_to_dest + 1;
                        n.Value.destination.previous_node_shortest_path = node;
                        queue.Enqueue(n.Value.destination, 1);
                    }
                }
            }

            foreach(KeyValuePair<string, Node> node in nodeMap)
            {
                Console.WriteLine(node.Key + "(" + node.Value.shortest_distance_to_dest + ")");
            }
        }

    }
}
