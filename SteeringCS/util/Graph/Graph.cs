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
    }
}
