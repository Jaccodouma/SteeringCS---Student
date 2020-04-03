using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.Graph
{
    class SparseGraph
    {
        public Dictionary<Vector2D, GraphNode> vertexMap;
        private GraphNode currentGraphNode;

        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------

        public SparseGraph()
        {
            vertexMap = new Dictionary<Vector2D, GraphNode>();
        }
        //----------------------------------------------------------------------
        // Getters & Setters
        //----------------------------------------------------------------------

        public Dictionary<Vector2D, GraphNode> GetVertexMap()
        {
            return this.vertexMap;
        }
        public GraphNode GetVertex(Vector2D name)
        {
            //TODO ContainsKey has to be made better
            if (vertexMap.ContainsKey(name))
            {
                return vertexMap[name];
            }
            else
            {
                vertexMap.Add(name, new GraphNode(name));
                return vertexMap[name];
            }
        }

        public void AddEdge(Vector2D source, Vector2D dest)
        {
            GraphNode v = GetVertex(source);
            GraphNode w = GetVertex(dest);
            GraphEdge edge = new GraphEdge(w, source.VectorDistance(dest));

            if (!v.adj.Contains<GraphEdge>(edge))
            {
                v.adj.Add(edge);
            }
        }

        public void ClearAll()
        {
            vertexMap.Clear();
        }


        //----------------------------------------------------------------------
        // ToString
        //----------------------------------------------------------------------

        public override string ToString()
        {
            string str = "";
            foreach (KeyValuePair<Vector2D, GraphNode> k in vertexMap)
            {
                str += k.Value.ToString() + "\n";
            }

            return str;
        }


        //----------------------------------------------------------------------
        // Algorithms
        //----------------------------------------------------------------------

        public void BFS(Vector2D name)
        {
            throw new System.NotImplementedException();
        }

        public void Dijkstra(Vector2D name)
        {
            FastPriorityQueue<GraphNode> queue = new FastPriorityQueue<GraphNode>(1000);
            this.currentGraphNode = vertexMap[name];
            foreach (KeyValuePair<Vector2D, GraphNode> k in vertexMap)
            {
                k.Value.Reset();
            }
            currentGraphNode.SetDistance(0);

            queue.Enqueue(currentGraphNode, (float)currentGraphNode.GetDistance());

            while (queue.Count > 0)
            {
                GraphNode dijkstraNode = queue.Dequeue();
                if (dijkstraNode.GetKnown())
                {
                    continue;
                }
                dijkstraNode.SetKnown(true);

                foreach (GraphEdge e in dijkstraNode.adj)
                {
                    GraphNode temp = e.dest;
                    if (!temp.GetKnown())
                    {
                        if (dijkstraNode.GetDistance() + e.cost < temp.GetDistance())
                        {
                            temp.SetDistance(dijkstraNode.GetDistance() + e.cost);
                            temp.SetPrevious(dijkstraNode);
                        }
                        queue.Enqueue(temp, (float)temp.GetDistance());
                    }
                }
            }
            string str = "";
            foreach (KeyValuePair<Vector2D, GraphNode> k in vertexMap)
            {
                str += k.Value.GetName().ToString() + "(" + k.Value.GetDistance() + ")\n";
            }
            Console.WriteLine(str);
        }
    }
}
