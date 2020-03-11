using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteeringCS
{
    public partial class Form1 : Form
    {
        World world;
        System.Timers.Timer timer;
        public const float timeDelta = 0.8f;

        private bool paused = true;

        public Form1()
        {
            InitializeComponent();

            world = new World(w: dbPanel1.Width, h: dbPanel1.Height);

            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 20;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!paused)
            {
                world.Update(timeDelta);
            }
            dbPanel1.Invalidate();
        }

        private void dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            world.Render(e.Graphics);
        }
        
        private void dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                world.Target.Pos = new Vector2D(e.X, e.Y);
            } else if (e.Button == MouseButtons.Right)
            {
                world.selectEntity(e.X, e.Y);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_pause_click(object sender, EventArgs e)
        {
            this.paused = true; 
        }

        private void btn_step_click(object sender, EventArgs e)
        {
            world.Update(timeDelta);
            dbPanel1.Invalidate();
        }

        private void btn_play_click(object sender, EventArgs e)
        {
            this.paused = false; 
        }
    }
}
