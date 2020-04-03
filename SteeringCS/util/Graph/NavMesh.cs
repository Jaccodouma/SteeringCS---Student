using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.Graph
{
    class NavMesh
    {
        private SparseGraph graph;
        private World world;
        private int boarderX, boarderY;
        private Random random = new Random();
        public NavMesh(World world)
        {
            this.world = world;
            this.boarderX = world.Width - 50;
            this.boarderY = world.Height - 50;
            graph = new SparseGraph();
            InitNavMesh();
        }

        public void InitNavMesh()
        {
            Floodfill();
        }

        private void Floodfill()
        {
            int inc = 50;
            /*
             * Step 1: Take a random point on screen and make it a vector
             * step 2: Put the vector in a queue
             * Step 3: go in a while loop till queue is empty
             * step 4: dequeue first vector in queue
             * step 5: check if vector is already in vertexmap if yes, do nothing. If no step 6
             * step 6: make the vector a new node in the map
             * step 7: Check for other nodes within range and lay edge
             * step 8: if spot is empty, make new vector and put it in the vector queue;
            */
            Queue<Vector2D> queue = new Queue<Vector2D>();
            //Step 1
            //Step 2
            queue.Enqueue(new Vector2D(world.Width/2, world.Height/2));

            //Step 3
            while (queue.Count > 0)
            {
                //Step 4
                Vector2D iterator = new Vector2D(queue.Dequeue());
                //Step 5
                if (!graph.vertexMap.ContainsKey(iterator))
                {
                    //Step 6
                    graph.GetVertex(iterator);
                }

                //Step 7
                //To the right
                if (iterator.X > 0 && iterator.X + inc < world.Width && iterator.Y > 0 && iterator.Y < world.Height)
                {
                    if (!queue.Any(x =>
                     x.X == iterator.X + inc))
                    {
                        queue.Enqueue(new Vector2D(iterator.X + inc, iterator.Y));
                    }
                }

                //To the left
                if (iterator.X - inc > 0 && iterator.X < world.Width && iterator.Y > 0 && iterator.Y < world.Height)
                {
                    if (!queue.Any(x =>
                     x.X == iterator.X - inc))
                    {
                        queue.Enqueue(new Vector2D(iterator.X - inc, iterator.Y));
                    }
                }
            }
        }
        public void Render(Graphics g)
        {
            Pen pen = new Pen(Brushes.Black, 0.1f);
            Brush brush = new SolidBrush(Color.Red);
            foreach (KeyValuePair<Vector2D, GraphNode> k in graph.GetVertexMap())
            {
                foreach (GraphEdge edge in k.Value.adj)
                {
                    g.DrawLine(pen,
                        (float)k.Value.GetName().X,
                        (float)k.Value.GetName().Y,
                        (float)edge.dest.GetName().X,
                        (float)edge.dest.GetName().Y);
                }
                g.FillEllipse(
                        brush,
                        (float)k.Value.GetName().X - 2.5f,
                        (float)k.Value.GetName().Y - 2.5f,
                        4,
                        4);
            }
        }
    }
}

