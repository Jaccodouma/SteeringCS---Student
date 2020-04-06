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
        public Path_planning(World world)
        {
            this.owner = null;
            this.currentGraph = world.navigation_graph;
        }
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
            int cell = currentGraph.spatial_partitioning.Cell_position_generation(position);

            if (cell > currentGraph.spatial_partitioning.amount_cells)
                return closest_node_found.no_closest_node_found.ToString();
            if (currentGraph.spatial_partitioning.GetCell(cell).Count < 0)
                return closest_node_found.no_closest_node_found.ToString();


            foreach (Node node in currentGraph.spatial_partitioning.GetCell(cell).Values)
            {
                if (position.VectorDistance(node.position_of_node.Clone()) < closest_distace)
                {
                    closest_distace = position.VectorDistance(node.position_of_node.Clone());
                    closest_node = node.position_of_node.Clone();
                }
            }
            if (closest_node.X != currentGraph.INFINITY && closest_node.Y != currentGraph.INFINITY)
            {
                return Graph.ID_generator(closest_node.X, closest_node.Y);
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
            if (algorithm == kind_of_algorithm.BF)
            {
                path.Add(owner.Pos.Clone());
                Breath_first_search_fast(path, closest_node_to_owner, closest_node_to_target);
                path.Add(destination_position.Clone());

            }
            if (algorithm == kind_of_algorithm.DK)
            {
                path.Add(destination_position.Clone());
                Dijkstra_fast(path, closest_node_to_owner, closest_node_to_target);
                path.Add(owner.Pos.Clone());
                path.Reverse();
            }
            if (algorithm == kind_of_algorithm.AS)
            {
                path.Add(destination_position.Clone());
                A_star(path, closest_node_to_owner, closest_node_to_target);
                path.Add(owner.Pos.Clone());
                path.Reverse();
            }

            Render_path(g, path, algorithm);
            return true;
        }

        public bool Create_path_to_item(Vector2D target_item)
        {
            throw new NotImplementedException();
        }
        /*------------------------------------------------------------------------------------------*/
        /*PATH-PLANNING-ALGORITHMES-----------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------*/
        /*Breath-first-search-with-heuristic------------------------------*/
        /*----------------------------------------------------------------*/
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
        /*----------------------------------------------------------------*/
        /*dijkstra--------------------------------------------------------*/
        /*----------------------------------------------------------------*/
        public List<Vector2D> Dijkstra_fast(List<Vector2D> path, string start_node, string destination)
        {
            FastPriorityQueue<Node> queue_fast = new FastPriorityQueue<Node>(currentGraph.nodeMap.Count + 1);
            Node iterator = currentGraph.nodeMap[start_node];

            iterator.shortest_distance_to_dest = 0;
            queue_fast.Enqueue(iterator, (float)iterator.heuristic_euclidean);

            while (queue_fast.Count > 0)
            {
                iterator = queue_fast.Dequeue();

                if (iterator.handled_node)
                {
                    continue;
                }
                iterator.handled_node = true;

                foreach (KeyValuePair<string, Edge> node in iterator.adjecent_edges)
                {
                    node.Value.destination.heuristic_euclidean = currentGraph.nodeMap[destination].position_of_node.Clone()
                        .VectorDistance(currentGraph.nodeMap[node.Value.destination.id].position_of_node.Clone());

                    if (!node.Value.destination.handled_node)
                    {
                        if (iterator.shortest_distance_to_dest + iterator.position_of_node.Clone().VectorDistance(node.Value.destination.position_of_node.Clone())
                            < node.Value.destination.shortest_distance_to_dest)
                        {
                            node.Value.destination.shortest_distance_to_dest =
                                 iterator.shortest_distance_to_dest + iterator.position_of_node.Clone().VectorDistance(node.Value.destination.position_of_node.Clone());
                            node.Value.destination.previous_node_shortest_path = iterator;
                        }
                        if (queue_fast.Contains(node.Value.destination))
                        {
                            IEnumerator<Node> enumerator = queue_fast.GetEnumerator();
                            enumerator.Reset();
                            enumerator.MoveNext();
                            while (enumerator.Current != node.Value.destination)
                            {
                                enumerator.MoveNext();
                            }
                            if (enumerator.Current.Priority > node.Value.destination.shortest_distance_to_dest)
                            {
                                queue_fast.UpdatePriority(node.Value.destination, (float)node.Value.destination.shortest_distance_to_dest);
                            }
                        }
                        else
                        {
                            queue_fast.Enqueue(node.Value.destination, (float)node.Value.destination.heuristic_euclidean);
                        }
                    }
                }
            }

            iterator = currentGraph.nodeMap[destination];
            while (iterator != null)
            {
                path.Add(iterator.position_of_node.Clone());
                iterator = iterator.previous_node_shortest_path;
            }
            return path;
        }
        /*----------------------------------------------------------------*/
        /*A*--------------------------------------------------------------*/
        /*----------------------------------------------------------------*/
        public List<Vector2D> A_star(List<Vector2D> path, string start_node, string destination)
        {
            FastPriorityQueue<Node> queue_fast = new FastPriorityQueue<Node>(currentGraph.nodeMap.Count + 1);
            Node iterator = currentGraph.nodeMap[start_node];

            iterator.shortest_distance_to_dest = 0;
            queue_fast.Enqueue(iterator, (float)iterator.heuristic_euclidean);

            while (queue_fast.Count > 0)
            {
                iterator = queue_fast.Dequeue();

                if (iterator.handled_node)
                {
                    continue;
                }
                iterator.handled_node = true;

                foreach (KeyValuePair<string, Edge> node in iterator.adjecent_edges)
                {
                    node.Value.destination.heuristic_euclidean = currentGraph.nodeMap[destination].position_of_node.Clone()
                        .VectorDistance(currentGraph.nodeMap[node.Value.destination.id].position_of_node.Clone());

                    if (!node.Value.destination.handled_node)
                    {
                        if (iterator.shortest_distance_to_dest + iterator.position_of_node.Clone().VectorDistance(node.Value.destination.position_of_node.Clone())
                            < node.Value.destination.shortest_distance_to_dest)
                        {
                            node.Value.destination.shortest_distance_to_dest =
                                 iterator.shortest_distance_to_dest
                                 + iterator.position_of_node.Clone().VectorDistance(node.Value.destination.position_of_node.Clone())
                                 + node.Value.destination.heuristic_euclidean;
                            node.Value.destination.previous_node_shortest_path = iterator;
                        }
                        if (queue_fast.Contains(node.Value.destination))
                        {
                            IEnumerator<Node> enumerator = queue_fast.GetEnumerator();
                            enumerator.Reset();
                            enumerator.MoveNext();
                            while (enumerator.Current != node.Value.destination)
                            {
                                enumerator.MoveNext();
                            }
                            if (enumerator.Current.Priority > node.Value.destination.shortest_distance_to_dest)
                            {
                                queue_fast.UpdatePriority(node.Value.destination, (float)node.Value.destination.shortest_distance_to_dest);
                            }
                        }
                        else
                        {
                            queue_fast.Enqueue(node.Value.destination, (float)node.Value.destination.heuristic_euclidean);
                        }
                    }
                }
            }

            iterator = currentGraph.nodeMap[destination];
            while (iterator != null)
            {
                path.Add(iterator.position_of_node.Clone());
                iterator = iterator.previous_node_shortest_path;
            }
            return path;
        }

        public void Render_path(Graphics g, List<Vector2D> path, kind_of_algorithm algorithm)
        {
            Pen pen = new Pen(Color.Aqua);
            Brush brush = new SolidBrush(Color.Aqua);
            if (algorithm == kind_of_algorithm.BF)
            {
                pen = new Pen(Color.Red);
                pen.Width = 3;
                brush = new SolidBrush(Color.Black);
            }
            if (algorithm == kind_of_algorithm.DK)
            {
                pen = new Pen(Color.Blue);
                pen.Width = 3;
                brush = new SolidBrush(Color.Yellow);
            }
            if (algorithm == kind_of_algorithm.AS)
            {
                pen = new Pen(Color.ForestGreen);
                pen.Width = 3;
                brush = new SolidBrush(Color.Purple);
            }

            for (int i = 0; i < path.Count - 1; i++)
            {
                g.DrawLine(pen, (float)path[i].X, (float)path[i].Y, (float)path[i + 1].X, (float)path[i + 1].Y);


            }
            for (int i = 0; i < path.Count - 1; i++)
            {
                g.FillEllipse(brush, (float)path[i + 1].X-4, (float)path[i + 1].Y-4, 8, 8);
            }
        }
    }
}
