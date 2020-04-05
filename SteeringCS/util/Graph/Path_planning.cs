using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SteeringCS.util.Graph
{
    class Path_planning
    {
        /*------------------------------------------------------------------------------------------*/
        /*Variables---------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------*/
        enum closest_node_found
        {
            closest_node_found = 1,
            no_closest_node_found = -1
        }

        public enum kind_of_algorithm
        {
            BF = 1,
            DK = 2,
            AS = 3
        }

        public BaseGameEntity owner;       //owner of the pathplanning class
        private Graph currentGraph;         //local reference to the navigation mesh
        private Vector2D destination_position;   //position where the "owner" would love to go to



        /*------------------------------------------------------------------------------------------*/
        /*Constructors------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------*/
        public Path_planning(BaseGameEntity owner)
        {
            this.owner = owner;
            this.currentGraph = owner.MyWorld.navigation_graph;
        }

        /*------------------------------------------------------------------------------------------*/
        /*Methods-----------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------*/
        /*Preperation-methods-for-path-planning---------------------------*/
        /*----------------------------------------------------------------*/
        private string Get_closest_node_to_position(Vector2D position)
        {
            double closest_distace = currentGraph.INFINITY;
            Vector2D closest_node = new Vector2D(currentGraph.INFINITY, currentGraph.INFINITY);
            int partition_cell = (int)Math.Floor((position.X / currentGraph.size_of_partitioning)
                * (position.Y / currentGraph.size_of_partitioning));

            if (!currentGraph.spatial_partitioning.ContainsKey(partition_cell))
                return closest_node_found.no_closest_node_found.ToString();
            if (currentGraph.spatial_partitioning[partition_cell].Count < 0)
                return closest_node_found.no_closest_node_found.ToString();


            foreach (Node node in currentGraph.spatial_partitioning[partition_cell])
            {
                if (position.VectorDistance(node.position_of_node.Clone()) < closest_distace)
                {
                    closest_distace = position.VectorDistance(node.position_of_node.Clone());
                    closest_node = node.position_of_node.Clone();
                }
            }
            if (closest_node.X != currentGraph.INFINITY && closest_node.Y != currentGraph.INFINITY)
            {
                return closest_node.X.ToString() + "," + closest_node.Y.ToString();
            }

            return closest_node_found.no_closest_node_found.ToString();
        }

        public bool Create_path_to_position(Vector2D target_position, List<Vector2D> path, kind_of_algorithm algorithm, Graphics g)
        {
            //set the requested target position
            this.destination_position = target_position.Clone();

            //Check if path is unobstructed
            //True => directly to target pos ====> TODO <====
            //false => find closest unobstructed target node

            string closest_node_to_owner = Get_closest_node_to_position(owner.Pos.Clone());
            string closest_node_to_target = Get_closest_node_to_position(destination_position);
            //Check if a node is found
            if (closest_node_to_owner == closest_node_found.no_closest_node_found.ToString()
                || closest_node_to_target == closest_node_found.no_closest_node_found.ToString())
            {
                return false;
            }
            //Reset node values for algorithm
            currentGraph.Reset_algorithm();
            path.Add(owner.Pos.Clone());

            if(algorithm == kind_of_algorithm.BF) Breath_first_search_fast(path, closest_node_to_owner, closest_node_to_target);
            if (algorithm == kind_of_algorithm.DK) Dijkstra_fast(path, closest_node_to_owner, closest_node_to_target);
            if (algorithm == kind_of_algorithm.AS) A_star(path, closest_node_to_owner, closest_node_to_target);
            path.Add(destination_position.Clone());

            Render_path(g, path);

            return true;
        }

        public bool Create_path_to_item(Vector2D target_item)
        {
            throw new NotImplementedException();
        }

        public List<Vector2D> Breath_first_search_fast(List<Vector2D> path, string start_node, string destination)
        {
            FastPriorityQueue<Node> queue_fast = new FastPriorityQueue<Node>(currentGraph.nodeMap.Count + 1);
            Node iterator = currentGraph.nodeMap[start_node];

            iterator.shortest_distance_to_dest = 0;
            queue_fast.Enqueue(iterator, (float)iterator.heuristic_euclidean);

            while (queue_fast.Count > 0)
            {
                iterator = queue_fast.Dequeue();
                path.Add(iterator.position_of_node);
                if (iterator.id == destination) break;

                foreach (KeyValuePair<string, Edge> node in iterator.adjecent_edges)
                {
                    node.Value.destination.heuristic_euclidean = currentGraph.nodeMap[destination].position_of_node
                        .VectorDistance(currentGraph.nodeMap[node.Value.destination.id].position_of_node);

                    if (node.Value.destination.shortest_distance_to_dest == currentGraph.INFINITY)
                    {
                        node.Value.destination.shortest_distance_to_dest = iterator.shortest_distance_to_dest + 1;
                        node.Value.destination.previous_node_shortest_path = iterator;
                        queue_fast.Enqueue(node.Value.destination, (float)node.Value.destination.heuristic_euclidean);
                    }
                }
            }
            return path;
        }

        public List<Vector2D> Dijkstra_fast(List<Vector2D> path, string start_node, string destination)
        {
            FastPriorityQueue<Node> queue_fast = new FastPriorityQueue<Node>(currentGraph.nodeMap.Count + 1);
            Node iterator = currentGraph.nodeMap[start_node];

            iterator.shortest_distance_to_dest = 0;
            queue_fast.Enqueue(iterator, (float)iterator.heuristic_euclidean);

            while (queue_fast.Count > 0)
            {
                iterator = queue_fast.Dequeue();
                path.Add(iterator.position_of_node);
            }
            return path;
        }

        public List<Vector2D> A_star(List<Vector2D> path, string start_node, string destination)
        {
            throw new NotImplementedException();
        }

        public void Render_path(Graphics g, List<Vector2D> path)
        {
            Pen pen = new Pen(Color.Red);
            pen.Width = 4;
            for (int i = 0; i < path.Count - 1; i++)
            {
                g.DrawLine(pen, (float)path[i].X, (float)path[i].Y, (float)path[i + 1].X, (float)path[i + 1].Y);
            }
            pen.Color = Color.Green;
            g.DrawEllipse(pen, (float)path[path.Count - 1].X, (float)path[path.Count - 1].Y, 10, 10);
        }
    }
}
