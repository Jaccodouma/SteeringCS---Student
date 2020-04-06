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
using SteeringCS.entity.turrets;

namespace SteeringCS
{
    class World
    {
        private Navigation_Graph navigation_graph;
        private double graining = 50;

        public MovingEntity selectedEntity { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Random rnd = new Random();

        public float time; 

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            time = 0;

            Base bse = new Base(new Vector2D(Width / 2, Height / 2), this);

            for (int i = 0; i < 20; i++)
            {
                Zombie z = addZombie(rnd.Next(0, Width), rnd.Next(0, Height));
                SeekBehaviour b = new SeekBehaviour(z, bse);
                z.SteeringBehaviours.Add(b);
            }

            navigation_graph = new Navigation_Graph(this, graining);
            navigation_graph.Flood_fill();
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

            BaseGameEntity.baseGameEntities.ToList().ForEach(e =>
            {
                e.Render(g);
            });

            if (selectedEntity != null)
            {
                selectedEntity.RenderDebug(g);
                selectedEntity.SteeringBehaviours.ForEach(sb => sb.Render(g));
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
