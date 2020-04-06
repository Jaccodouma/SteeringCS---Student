using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.util.Graph;

namespace SteeringCS.util.Data
{
    class Cellspace_partitioning
    {
        //A cell partition -- inner class
        private class Cell
        {
            public Rectangle cell;
            public List<BaseGameEntity> entities = new List<BaseGameEntity>();                       //list of entities in cell
            public Dictionary<string,Node> navigation_map_nodes = new Dictionary<string, Node>();    //list of navmesh nodes in cell

            public Cell(int x, int y, int width, int height)
            {
                this.cell = new Rectangle(x, y, width, height);
            }
        }
        /*------------------------------------------------------------------------------------------*/
        /*Variables---------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------*/
        private Dictionary<int, Cell> cells = new Dictionary<int, Cell>();  //list of all cells
        public int space_width, space_height;                               //Size of a space
        public int x_amount_cells, y_amount_cells, amount_cells;            //Amount of cells in a row and colom
        public int max_entity_count;                                        //Max amount of enities in one cell
        /*------------------------------------------------------------------------------------------*/
        /*Constructors------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------*/
        public Cellspace_partitioning(World world, int cells_X, int cells_Y, int max_entities)
        {
            this.space_height = world.Height / cells_X;
            this.space_width = world.Width / cells_Y;
            this.x_amount_cells = cells_X;
            this.y_amount_cells = cells_Y;
            this.amount_cells = x_amount_cells * y_amount_cells;
            this.max_entity_count = max_entities;

            for (int i = 0; i < (x_amount_cells * y_amount_cells); i++)
            {
                Vector2D xy = Cell_xy_generation(i);
                cells.Add(i, new Cell((int)xy.X, (int)xy.Y, space_width, space_height));
            }
        }
        /*------------------------------------------------------------------------------------------*/
        /*Methods-----------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------*/
        /*Information-generation------------------------------------------*/
        /*----------------------------------------------------------------*/
        public int Cell_position_generation(Vector2D vector)
        {
            int x_value = (int)Math.Floor(vector.X / this.space_width);
            int y_value = (int)Math.Floor(vector.Y / this.space_height);
            return x_value * y_value;
        }

        public int Cell_position_generation(Node node)
        {
            int x_value = (int)Math.Floor(node.position_of_node.X / this.space_width);
            int y_value = (int)Math.Floor(node.position_of_node.Y / this.space_height);
            return x_value * y_value;
        }

        public int Cell_position_generation(BaseGameEntity entity)
        {
            int x_value = (int)Math.Floor(entity.Pos.X / this.space_width);
            int y_value = (int)Math.Floor(entity.Pos.Y / this.space_height);
            return x_value * y_value;
        }

        public Vector2D Cell_xy_generation(int x)
        {
            int y_multiplier = (int)Math.Floor((double)x / x_amount_cells);
            int x_multiplier = (int)Math.Floor((double)(x % x_amount_cells));
            return new Vector2D(space_width * x_multiplier, space_height *y_multiplier);
        }

        public bool ContainsNode(Node node)
        {
            return cells[Cell_position_generation(node)]
                .navigation_map_nodes
                .ContainsKey(Graph.Graph.ID_generator(node));
        }

        public bool Contains_key_node(Node node) {
            return cells.ContainsKey(Cell_position_generation(node));
        }

        public Dictionary<string, Node> GetCell(int i)
        {
            return cells[i].navigation_map_nodes;
        }
        /*----------------------------------------------------------------*/
        /*Adding-information-to-cell--------------------------------------*/
        /*----------------------------------------------------------------*/
        public void Add_entity(BaseGameEntity entity)
        {
            int cell_number = Cell_position_generation(entity);
            cells[cell_number].entities.Add(entity);
        }

        public void Add_node(Node node)
        {
            int cell_number = Cell_position_generation(node);
            cells[cell_number].navigation_map_nodes.Add(Graph.Graph.ID_generator(node),node);
        }

        public Cellspace_partitioning Clone()
        {
            return this;
        }

        /*----------------------------------------------------------------*/
        /*Rendering-of-map------------------------------------------------*/
        /*----------------------------------------------------------------*/
        public void Render(Graphics g) 
        {
            Pen pen = new Pen(Color.Blue);
            pen.Width = 2;
            foreach (KeyValuePair<int, Cell> cell in cells)
            {
                g.DrawRectangle(pen, cell.Value.cell);
            }
        }
    }
}
