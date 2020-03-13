using SteeringCS.util.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteeringCS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    Application.Run(new Form1());

            /*Vector2D vec0 = new Vector2D(0, 0);
            Vector2D vec1 = new Vector2D(500, 500);
            Vector2D vec2 = new Vector2D(1000, 1000);
            Vector2D vec3 = new Vector2D(750, -750);
            SparseGraph graph = new SparseGraph();
            graph.GetVertex(vec0);
            graph.GetVertex(vec1);
            graph.GetVertex(vec2);
            graph.GetVertex(vec3);

            graph.AddEdge(vec0, vec1);
            graph.AddEdge(vec0, vec2);
            graph.AddEdge(vec0, vec3);

            graph.AddEdge(vec1, vec0);
            graph.AddEdge(vec1, vec2);
            graph.AddEdge(vec1, vec3);

            graph.AddEdge(vec3, vec0);
            graph.AddEdge(vec3, vec1);
            graph.AddEdge(vec3, vec2);

            graph.AddEdge(vec2, vec0);
            graph.AddEdge(vec2, vec1);
            graph.AddEdge(vec2, vec3);

            Console.WriteLine(graph);

            graph.Dijkstra(vec0);*/

            SparseGraph graph;
        }
    }
}
