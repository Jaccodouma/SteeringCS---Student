using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteeringCS
{
    class GameDebug
    {
        private World world; 

        public GameDebug(World w)
        {
            this.world = w;
        }

        public DBPanel behavioursPanel; 
        public void behaviorsPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int x = 0, y = 0;

            if (this.world.selectedEntity != null)
            {
                this.world.selectedEntity.SteeringBehaviours.ForEach(behavior =>
                {
                    behavior.RenderInfoPanel(g, x, y);
                    y += 100;
                });
                behavioursPanel.Size = new Size(behavioursPanel.Size.Width, y);
            }
        }
    }
}
