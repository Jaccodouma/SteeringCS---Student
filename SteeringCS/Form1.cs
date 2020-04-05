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

        private List<Panel> panels = new List<Panel>();

        private bool paused = true;

        private DBPanel debugPanel; 

        public Form1()
        {
            InitializeComponent();

            world = new World(w: dbPanel1.Width, h: dbPanel1.Height);

            initTabs();

            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 20;
            timer.Enabled = true;

            panels.Add(this.dbPanel1);
        }

        private void initTabs()
        {
            TabControl tabControl = this.tabControl1;

            //initTab_testTab(tabControl);
            initTab_behaviours(tabControl);
        }

        private void initTab_testTab(TabControl tabControl)
        {
            TabPage page = new TabPage();
            page.Text = "Test";
            page.Size = new Size(tabControl.Size.Width, tabControl.Size.Height);

            tabControl.Controls.Add(page);
        }

        DBPanel tab_behaviors_panel;
        private void initTab_behaviours(TabControl tabControl)
        {
            // Create page
            TabPage page = new TabPage();
            page.Text = "Behaviours";
            page.Size = new Size(tabControl.Size.Width, tabControl.Size.Height);

            // Add drawing field
            tab_behaviors_panel = new DBPanel();
            tab_behaviors_panel.Size = new Size(page.Size.Width, page.Size.Height);

            tab_behaviors_panel.BorderStyle = BorderStyle.Fixed3D;

            debugPanel = tab_behaviors_panel;
            tab_behaviors_panel.Paint += behaviorsPanel_Paint;

            panels.Add(tab_behaviors_panel);
            
            page.Controls.Add(tab_behaviors_panel);

            // Add control to page 
            tabControl.Controls.Add(page);
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!paused)
            {
                world.Update(timeDelta);
                panels.ForEach(panel => panel.Invalidate());
            }
        }

        private void dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            world.Render(e.Graphics);
        }
        public void behaviorsPanel_Paint(object sender, PaintEventArgs e)
        {
            if (this.world.selectedEntity != null)
                this.world.selectedEntity.RenderDebugPanel(e.Graphics, this.debugPanel);
        }

        private void dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                world.Target.Pos = new Vector2D(e.X, e.Y);
                world.addTurret(e.X, e.Y);
            } else if (e.Button == MouseButtons.Right)
            {
                world.selectEntity(e.X, e.Y);
            }
        }

        private void btn_pause_click(object sender, EventArgs e)
        {
            this.paused = true; 
        }

        private void btn_step_click(object sender, EventArgs e)
        {
            world.Update(timeDelta);
            panels.ForEach(panel => panel.Invalidate());
        }

        private void btn_play_click(object sender, EventArgs e)
        {
            this.paused = false; 
        }
    }
}
