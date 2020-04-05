using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.Graph
{
    class Navigation_Graph : Graph
    {
        /*------------------------------------------------------------------------------------------*/
        /*Variables---------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------*/
        World currentWorld;
        double waypoint_size = 10;
        bool already_rendered;

        /*------------------------------------------------------------------------------------------*/
        /*Constructors------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------*/
        public Navigation_Graph(World world, double grain)
        {
            if (world == null) throw new Exception("World that is passed equals NULL");
            this.currentWorld = world;
            if (grain < 8) throw new Exception("Graining should be atleast 4");
            this.graining = grain;

            spatial_partitioning = new Dictionary<double, List<Node>>();
            already_rendered = false;
        }


        /*------------------------------------------------------------------------------------------*/
        /*Methods-----------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------*/
        /*Flood-fill-algorithms-------------------------------------------*/
        /*----------------------------------------------------------------*/
        public void Flood_fill()
        {
            Random random = new Random();
            double x = random.Next((int)(graining / 2), (int)(currentWorld.Width - graining / 2));
            double y = random.Next((int)(graining / 2), (int)(currentWorld.Height - graining / 2));
            string old = x.ToString() + "," + y.ToString();
            _Flood_fill(x, y, old);
            Console.WriteLine("floodfill finished");
        }

        private void _Flood_fill(double x, double y, string old)
        {
            string position = x.ToString() + "," + y.ToString();
            if (x < 0 + graining / 2 || y < 0 + graining / 2 || x > currentWorld.Width - graining / 2 || y > currentWorld.Height - graining / 2
                || nodeMap.ContainsKey(position))
            {
                return;
            }

            //check if node already exists
            if (!nodeMap.ContainsKey(position))
            {
                Get_node(position, new Vector2D(x, y));
            }
            _Flood_fill(x + graining, y, position); // right
            _Flood_fill(x - graining, y, position); // left
            _Flood_fill(x, y + graining, position); // down
            _Flood_fill(x, y - graining, position); // up

            //check if edge already exists
            //edge x+graining, y - right
            string adjecent = (x + graining).ToString() + "," + y.ToString();
            if (!nodeMap[position].adjecent_edges.ContainsKey(adjecent) && nodeMap.ContainsKey(adjecent))
            {
                nodeMap[position].adjecent_edges.Add(adjecent, new Edge(nodeMap[position], nodeMap[adjecent]));
            }
            //edge x-graining, y - left
            adjecent = (x - graining).ToString() + "," + y.ToString();
            if (!nodeMap[position].adjecent_edges.ContainsKey(adjecent) && nodeMap.ContainsKey(adjecent))
            {
                nodeMap[position].adjecent_edges.Add(adjecent, new Edge(nodeMap[position], nodeMap[adjecent]));
            }
            //edge x, y+graining - down 
            adjecent = x.ToString() + "," + (y + graining).ToString();
            if (!nodeMap[position].adjecent_edges.ContainsKey(adjecent) && nodeMap.ContainsKey(adjecent))
            {
                nodeMap[position].adjecent_edges.Add(adjecent, new Edge(nodeMap[position], nodeMap[adjecent]));
            }
            //edge x, y-graining - up
            adjecent = x.ToString() + "," + (y - graining).ToString();
            if (!nodeMap[position].adjecent_edges.ContainsKey(adjecent) && nodeMap.ContainsKey(adjecent))
            {
                nodeMap[position].adjecent_edges.Add(adjecent, new Edge(nodeMap[position], nodeMap[adjecent]));
            }
            //edge x-graining, y-graining - upper left
            adjecent = (x - graining).ToString() + "," + (y - graining).ToString();
            if (!nodeMap[position].adjecent_edges.ContainsKey(adjecent) && nodeMap.ContainsKey(adjecent))
            {
                nodeMap[position].adjecent_edges.Add(adjecent, new Edge(nodeMap[position], nodeMap[adjecent]));
            }
            //edge x+graining, y-graining - upper right
            adjecent = (x + graining).ToString() + "," + (y - graining).ToString();
            if (!nodeMap[position].adjecent_edges.ContainsKey(adjecent) && nodeMap.ContainsKey(adjecent))
            {
                nodeMap[position].adjecent_edges.Add(adjecent, new Edge(nodeMap[position], nodeMap[adjecent]));
            }
            //edge x-graining, y+graining - bottom left
            adjecent = (x - graining).ToString() + "," + (y + graining).ToString();
            if (!nodeMap[position].adjecent_edges.ContainsKey(adjecent) && nodeMap.ContainsKey(adjecent))
            {
                nodeMap[position].adjecent_edges.Add(adjecent, new Edge(nodeMap[position], nodeMap[adjecent]));
            }
            //edge x-graining, y-graining - bottom right
            adjecent = (x + graining).ToString() + "," + (y + graining).ToString();
            if (!nodeMap[position].adjecent_edges.ContainsKey(adjecent) && nodeMap.ContainsKey(adjecent))
            {
                nodeMap[position].adjecent_edges.Add(adjecent, new Edge(nodeMap[position], nodeMap[adjecent]));
            }
        }
        /*----------------------------------------------------------------*/
        /*Rendering-of-flood-filled-mesh----------------------------------*/
        /*----------------------------------------------------------------*/
        public void Render(Graphics g)
        {
            if (already_rendered) return;

            Brush brush = new SolidBrush(Color.Gray);
            Pen pen = new Pen(Color.Gray, 2);
            foreach (KeyValuePair<string, Node> node in nodeMap)
            {
                g.FillEllipse(
                    brush,
                    (float)(node.Value.position_of_node.X - (waypoint_size / 2)),
                    (float)(node.Value.position_of_node.Y - (waypoint_size / 2)),
                    (float)waypoint_size,
                    (float)waypoint_size
                    );

                foreach (KeyValuePair<string, Edge> edge in node.Value.adjecent_edges)
                {
                    g.DrawLine(
                        pen,
                        (float)node.Value.position_of_node.X,
                        (float)node.Value.position_of_node.Y,
                        (float)edge.Value.destination.position_of_node.X,
                        (float)edge.Value.destination.position_of_node.Y
                        );
                }
            }
            pen.Color = Color.BlueViolet;
            pen.Width = 1;
            for (double i = 0; i < currentWorld.Width; i += size_of_partitioning)
            {
                g.DrawLine(pen, (float)i, 0, (float)i, currentWorld.Height);
            }

            for (double i = 0; i < currentWorld.Height; i += size_of_partitioning)
            {
                g.DrawLine(pen, 0, (float)i, currentWorld.Width, (float)i);
            }
            //make sure it does not get rendered again.
            //BE FRIENDLY TO YOUR CPU YEAH!!
            //this.already_rendered = true;
        }

        /*----------------------------------------------------------------*/
        /*Spacial-partitioning-of-mesh-nodes------------------------------*/
        /*----------------------------------------------------------------*/
        public void Spacial_partitioning_subscribe()
        {
            double amount_of_cells = Math.Ceiling((currentWorld.Width / size_of_partitioning) * (currentWorld.Height / size_of_partitioning));
            for (double i = 0; i < amount_of_cells; i++)
            {
                spatial_partitioning.Add(i, new List<Node>());
            }

            if (nodeMap.Count < 0) throw new Exception("Graph is empty");
            foreach (KeyValuePair<string, Node> key in nodeMap)
            {
                if (spatial_partitioning.ContainsKey(Math.Floor((key.Value.position_of_node.X / size_of_partitioning)
                    * (key.Value.position_of_node.Y / size_of_partitioning))))
                {
                    spatial_partitioning[Math.Floor((key.Value.position_of_node.X / size_of_partitioning) * (key.Value.position_of_node.Y / size_of_partitioning))].Add(
                            key.Value
                        );
                }
                else throw new Exception("key not found: " + (key.Value.position_of_node.X / size_of_partitioning)
                    * (key.Value.position_of_node.Y / size_of_partitioning));
            }
        }
    }
}
