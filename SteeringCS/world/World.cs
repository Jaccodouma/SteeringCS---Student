using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.util.Graph;

namespace SteeringCS
{
    class World
    {
        public Navigation_Graph navigation_graph;
        private double graining = 20; //distance between nodes in floodfill
        public Path_planning path_Planning;

        private List<MovingEntity> entities = new List<MovingEntity>();
        private Queue<MovingEntity> newEntities = new Queue<MovingEntity>();
        public Target Target { get; set; }
        public MovingEntity selectedEntity { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Random rnd = new Random();

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            //obstacles init
            populate();
            navigation_graph = new Navigation_Graph(this, graining);
            navigation_graph.Flood_fill();
            navigation_graph.Spacial_partitioning_subscribe();
            path_Planning = new Path_planning(new Vehicle(new Vector2D(100,100), this));
            
        }

        private void populate()
        {
            Target = new Target(new Vector2D(100, 60), this);
            Target.VColor = Color.DarkRed;
            Target.Pos = new Vector2D(100, 40);

            for (int i = 0; i < 10; i++)
                addEntity(rnd.Next(0,Width),rnd.Next(0,Height));
        }

        public void Update(float timeElapsed)
        {
            foreach (MovingEntity me in entities)
            {
                me.Update(timeElapsed);
            } 
            while (newEntities.Count > 0)
            {
                entities.Add(newEntities.Dequeue());
            }
        }

        internal void selectEntity(int x, int y)
        {
            this.selectedEntity = null;
            foreach (MovingEntity me in entities)
            {
                if (
                    Math.Pow(Math.Abs(x - me.Pos.X), 2) +
                    Math.Pow(Math.Abs(y - me.Pos.Y), 2) < Math.Pow(10, 2)
                )
                {
                    this.selectedEntity = me; 
                }
            }
        }

        public void Render(Graphics g)
        {
            if (navigation_graph != null)
            {
                navigation_graph.Render(g);
            }

            entities.ForEach(e =>
            {
                e.Render(g);
            });
            Target.Render(g);
            if (selectedEntity != null)
            {
                selectedEntity.RenderDebug(g);
                selectedEntity.SteeringBehaviours.ForEach(sb => sb.Render(g));

                if (path_Planning != null)
                {
                    path_Planning.owner = selectedEntity;
                    path_Planning.Create_path_to_position(new Vector2D(200, 200), new List<Vector2D>(), Path_planning.kind_of_algorithm.BF, g);
                }
            }

        }

        public void addEntity(int x, int y)
        {
            Vehicle v = new Vehicle(new Vector2D(x, y), this);
            //v.VColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            v.VColor = Color.Blue;

            //SteeringBehaviour arr = new ArriveBehaviour(v, Target, Deceleration.slow);
            //v.SteeringBehaviours.Add(arr);
            //SteeringBehaviour seek = new SeekBehaviour(v, Target);
            //v.SteeringBehaviours.Add(seek);

            SteeringBehaviour coh = new Group_CohesionBehaviour(v, entities);
            v.SteeringBehaviours.Add(coh);

            SteeringBehaviour sep = new Group_SeperationBehaviour(v, entities);
            v.SteeringBehaviours.Add(sep);

            SteeringBehaviour wander = new WanderBehaviour(v, 20, 20, 10);
            v.SteeringBehaviours.Add(wander);

            newEntities.Enqueue(v);
        }
    }
}
