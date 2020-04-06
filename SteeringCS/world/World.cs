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
using SteeringCS.util.Data;
using SteeringCS.entity.turrets;

namespace SteeringCS
{
    class World
    {
        public Cellspace_partitioning partitioning;
        public Navigation_Graph navigation_graph;
        private double graining = 20; //distance between nodes in floodfill
        public Path_planning path_Planning;

        public MovingEntity selectedEntity { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Random rnd = new Random();

        public float time; 

        public World(int w, int h)
        {
            Width = w;
            Height = h;

            //obstacles init
            partitioning = new Cellspace_partitioning(this, 10, 10, 100);
            navigation_graph = new Navigation_Graph(this, graining, partitioning);
            path_Planning = new Path_planning(this);
            
            time = 0;

            Base bse = new Base(new Vector2D(Width / 2, Height / 2), this);

            for (int i = 0; i < 20; i++)
            {
                Zombie z = addZombie(rnd.Next(0, Width), rnd.Next(0, Height));
                SeekBehaviour b = new SeekBehaviour(z, bse);
                z.SteeringBehaviours.Add(b);
            }
        }

        public void Update(float timeElapsed)
        {
            time += timeElapsed;

            foreach (BaseGameEntity e in BaseGameEntity.baseGameEntities.ToList())
            {
                e.Update(timeElapsed);
            }
        }

        internal void selectEntity(int x, int y)
        {
            this.selectedEntity = null;
            foreach (BaseGameEntity e in BaseGameEntity.baseGameEntities.ToList())
            {
                if (e is MovingEntity)
                {
                    if (
                        Math.Pow(Math.Abs(x - e.Pos.X), 2) +
                        Math.Pow(Math.Abs(y - e.Pos.Y), 2) < Math.Pow(10, 2)
                    )
                    {
                        this.selectedEntity = (MovingEntity) e;
                    }
                }
            }
        }

        public void Render(Graphics g)
        {
            if (navigation_graph != null)
            {
                navigation_graph.Render(g);
            }

            if (partitioning != null)
            {
                partitioning.Render(g);
            }

            BaseGameEntity.baseGameEntities.ToList().ForEach(e =>
            {
                e.Render(g);
            });

            if (selectedEntity != null)
            {
                selectedEntity.RenderDebug(g);
                selectedEntity.SteeringBehaviours.ForEach(sb => sb.Render(g));

                if (path_Planning != null)
                {
                    path_Planning.owner = selectedEntity;
                    path_Planning.Create_path_to_position(new Vector2D(200, 200), new List<Vector2D>(), Path_planning.kind_of_algorithm.BF, g);
                    path_Planning.Create_path_to_position(new Vector2D(200, 200), new List<Vector2D>(), Path_planning.kind_of_algorithm.DK, g);
                    path_Planning.Create_path_to_position(new Vector2D(200, 200), new List<Vector2D>(), Path_planning.kind_of_algorithm.AS, g);
                }
            }
            if (navigation_graph != null)
            {
                navigation_graph.Render(g);
            }

        }

        public Zombie addZombie(int x, int y)
        {
            return new Zombie(new Vector2D(x, y), this);
        }

        public void addTurret(int x, int y)
        {
            new BasicTurret(new Vector2D(x, y), this);
        }
    }
}
