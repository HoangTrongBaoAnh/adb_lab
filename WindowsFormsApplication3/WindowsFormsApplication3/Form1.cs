using Db4objects.Db4o;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            IObjectContainer db = Db4oEmbedded.OpenFile("pilotdb.yap");
            Pilot p1 = new Pilot(txtname.Text, int.Parse(txtpoint.Text));
            //Lưu đối tượng p1
            db.Store(p1);

            db.Close();
            getpilot();
        }

        private void getpilot()
        {
            var pilotTemp = new Pilot();
            IObjectContainer db = Db4oEmbedded.OpenFile("pilotdb.yap");
            List<Pilot> data = new List<Pilot>();
            IObjectSet result = db.QueryByExample(pilotTemp);

            for(int i = 0; i < result.Count; i++)
            {
                var plobj = (Pilot)result[i];
                data.Add(plobj);
            }


            db.Close();

            dataGridView1.DataSource = data;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            getpilot();


        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            IObjectContainer db = Db4oEmbedded.OpenFile("pilotdb.yap");
            IObjectSet result = db.QueryByExample(new Pilot(txtname.Text, 0));
            Pilot p = (Pilot)result[0];
            p.addpoint(int.Parse(txtpoint.Text));
            db.Store(p);
            db.Close();
            getpilot();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            txtname.Text = row.Cells[0].Value.ToString();
            txtpoint.Text = row.Cells[1].Value.ToString();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            IObjectContainer db = Db4oEmbedded.OpenFile("pilotdb.yap");
            IObjectSet result = db.QueryByExample(new Pilot(txtname.Text, 0));
            Pilot p = (Pilot)result[0];
            db.Delete(p);
            db.Close();
            getpilot();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getpilot();
        }
    }
}
