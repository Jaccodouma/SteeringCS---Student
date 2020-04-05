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
        private Navigation_Graph navigation_graph;
        private double graining = 50;

        private List<BaseGameEntity> entities = new List<BaseGameEntity>();
        private Queue<BaseGameEntity> newEntities = new Queue<BaseGameEntity>();
        private Queue<BaseGameEntity> removeEntities = new Queue<BaseGameEntity>();

        private List<MovingEntity> zombies = new List<MovingEntity>();

        public Target Target { get; set; }
        public MovingEntity selectedEntity { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Random rnd = new Random();

        public float time; 

        public void removeEntity(BaseGameEntity e)
        {
            this.removeEntities.Enqueue(e);
            if (e is Zombie)
            {
                zombies.Remove((Zombie) e);
            }
        }

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            time = 0; 

            //obstacles init
            populate();
            navigation_graph = new Navigation_Graph(this, graining);
            navigation_graph.Flood_fill();
        }

        public List<MovingEntity> getZombies()
        {
            return this.zombies;
        }

        private void populate()
        {
            Target = new Target(new Vector2D(100, 60), this);
            Target.VColor = Color.DarkRed;
            Target.Pos = new Vector2D(100, 40);

            for (int i = 0; i < 20; i++)
                addZombie(rnd.Next(0,Width),rnd.Next(0,Height));
        }

        public void Update(float timeElapsed)
        {
            time += timeElapsed;

            foreach (BaseGameEntity me in entities)
            {
                me.Update(timeElapsed);
            } 
            while (newEntities.Count > 0)
            {
                entities.Add(newEntities.Dequeue());
            }
            while (removeEntities.Count > 0)
            {
                entities.Remove(removeEntities.Dequeue());
            }
        }

        internal void selectEntity(int x, int y)
        {
            this.selectedEntity = null;
            foreach (BaseGameEntity me in entities)
            {
                if (me is MovingEntity)
                {
                    if (
                        Math.Pow(Math.Abs(x - me.Pos.X), 2) +
                        Math.Pow(Math.Abs(y - me.Pos.Y), 2) < Math.Pow(10, 2)
                    )
                    {
                        this.selectedEntity = (MovingEntity) me;
                    }
                }
            }
        }

        public void Render(Graphics g)
        {
            entities.ForEach(e =>
            {
                e.Render(g);
            });
            Target.Render(g);
            if (selectedEntity != null)
            {
                selectedEntity.RenderDebug(g);
                selectedEntity.SteeringBehaviours.ForEach(sb => sb.Render(g));
            }

            if(navigation_graph != null)
            {
                navigation_graph.Render(g);
            }
        }

        public void addZombie(int x, int y)
        {
            Zombie v = new Zombie(new Vector2D(x, y), this);
            //v.VColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            v.VColor = Color.DarkGreen;

            //SteeringBehaviour arr = new ArriveBehaviour(v, Target, Deceleration.slow);
            //v.SteeringBehaviours.Add(arr);
            SteeringBehaviour seek = new SeekBehaviour(v, Target);
            seek.weight = 2;
            v.SteeringBehaviours.Add(seek);

            SteeringBehaviour coh = new Group_CohesionBehaviour(v, zombies);
            v.SteeringBehaviours.Add(coh);

            SteeringBehaviour sep = new Group_SeperationBehaviour(v, zombies);
            v.SteeringBehaviours.Add(sep);

            SteeringBehaviour wander = new WanderBehaviour(v, 70, 50, 10);
            v.SteeringBehaviours.Add(wander);

            newEntities.Enqueue(v);
            zombies.Add(v);
        }

        public void addTurret(int x, int y)
        {
            Turret t = new Turret(new Vector2D(x, y), this);
            newEntities.Enqueue(t);
        }
    }
}
