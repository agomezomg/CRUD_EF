using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkCRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CatBaseEntities context = new CatBaseEntities();
            catgrid.DataSource = context.cats.ToList();
        }

        private void loadgrid()
        {
            CatBaseEntities context = new CatBaseEntities();
            catgrid.DataSource = context.cats.ToList();
        }

        private void filltxtfields()
        {
            try
            {
                this.txt_id.Text = catgrid.SelectedRows[0].Cells[0].Value.ToString();
                this.txt_name.Text = catgrid.SelectedRows[0].Cells[1].Value.ToString();
                this.txt_age.Text = catgrid.SelectedRows[0].Cells[2].Value.ToString();
            } catch (Exception e)
            {
                Console.WriteLine("could not load text");
            }
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            using (CatBaseEntities context = new CatBaseEntities())
            {
                int nextID = context.cats.Last().catID;
                cat mew_cat = new cat
                {
                    catID = nextID,
                    catName = this.txt_name.Text,
                    catAge = int.Parse(this.txt_age.Text),
                    ownerID = 1
                };
                context.cats.Add(mew_cat);
                context.SaveChanges();
                loadgrid();
            }

        }

        private void catgrid_MouseClick(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("Location is:\nx = " + e.Location.X + "\ny = " + e.Location.Y);
            filltxtfields();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            int key = int.Parse(this.txt_id.Text);
            using (CatBaseEntities context = new CatBaseEntities())
            {
                cat mew_cat = context.cats.FirstOrDefault(input => input.catID==key);

                mew_cat.catName = this.txt_name.Text;
                mew_cat.catAge = int.Parse(this.txt_id.Text);
                mew_cat.ownerID = 1;
                
                context.SaveChanges();
                loadgrid();
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            int key = int.Parse(this.txt_id.Text);
            using (CatBaseEntities context = new CatBaseEntities())
            {
                cat mew_cat = context.cats.FirstOrDefault(input => input.catID == key);

                context.cats.Remove(mew_cat);
                context.SaveChanges();
                loadgrid();
            }
        }
    }
}
