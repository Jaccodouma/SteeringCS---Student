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
        public double INFINITY = double.PositiveInfinity;
        public double graining;
        public Dictionary<string, Node> nodeMap = new Dictionary<string, Node>();

        //Vars for spacial partitioning
        public Dictionary<double, List<Node>> spatial_partitioning;
        public double size_of_partitioning = 100;

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
            string id = ID_generator(dest);

            if (source == null || dest == null) return;

            source.adjecent_edges.Add(id, new Edge(nodeMap[source_name], nodeMap[dest_name]));
        }

        public void Add_Edge(string source_name, Vector2D position_source, string dest_name, Vector2D position_dest)
        {
            Node source = Get_node(source_name, position_source);
            Node dest = Get_node(dest_name, position_dest);
            string id = ID_generator(dest);

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
            nodeMap.Add(node_name, new Node(position, ID_generator(position.X, position.Y)));
            return nodeMap[node_name];
        }

        public void Reset_algorithm()
        {
            foreach (KeyValuePair<string, Node> node in nodeMap)
            {
                node.Value.shortest_distance_to_dest = INFINITY;
                node.Value.previous_node_shortest_path = null;
                node.Value.handled_node = false;
                node.Value.heuristic_euclidean = INFINITY;
            }
        }
        public string ID_generator(double x, double y)
        {
            return x.ToString() + "," + y.ToString();
        }

        public string ID_generator(double x, double y, double offset_x, double offset_y)
        {
            return (x + offset_x).ToString() + "," + (y + offset_y).ToString();
        }

        public string ID_generator(Node node)
        {
            return node.position_of_node.X.ToString() + "," + node.position_of_node.Y.ToString();
        }

        public string ID_generator(Node node, double offset_x, double offset_y)
        {
            return (node.position_of_node.X + offset_x).ToString() + "," + (node.position_of_node.Y + offset_y).ToString();
        }

        public string ID_generator(Vector2D node)
        {
            return node.X.ToString() + "," + node.Y.ToString();
        }

        public string ID_generator(Vector2D node, double offset_x, double offset_y)
        {
            return (node.X + offset_x).ToString() + "," + (node.Y + offset_y).ToString();
        }
    }
}
