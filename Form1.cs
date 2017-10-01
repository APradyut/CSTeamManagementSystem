using System;
using System.Data;
using System.Windows.Forms;

namespace Team_Database
{
    public partial class Form1 : Form
    {
        delegate void UpdateGridHandler(string query);
        delegate void UpdateGridThreadHandler(DataTable table);
        private SupportLayer support = new SupportLayer();
        private TeamDetailsDB td = new TeamDetailsDB();
        public Form1()
        {
            InitializeComponent();
            dataGridView.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int queryType = support.inputType(QueryTxtbx.Text);
            if (queryType == -1)
            {
                MessageBox.Show("Check for the Input");
            }
            else if (queryType == 0)
            {
                UpdateGridHandler ug = getPlayerNamesString;
                dataGridView.Visible = true;
                ug.BeginInvoke(QueryTxtbx.Text, cb, null);
            }
            else if (queryType == 1)
            {
                UpdateGridHandler ug = getPlayerNamesNumber;
                dataGridView.Visible = true;
                ug.BeginInvoke(QueryTxtbx.Text, cb, null);
            }
        } 
        private void cb(IAsyncResult res)
        {

        }
        private void UpdateGrid11(DataTable table)
        {
            dataGridView.DataSource = table;
        }
        private void getPlayerNamesString(object query)
        {
            string Query = query.ToString();
            DataTable dt = new DataTable();
            dt = td.getPlayerNames(Query, 0, 0);
            if (dataGridView.InvokeRequired)
            {
                UpdateGridThreadHandler handler = UpdateGrid11;
                dataGridView.BeginInvoke(handler, dt);
            }
            else
            {
                dataGridView.DataSource = dt;
            }                    
        }
        private void getPlayerNamesNumber(object query)
        {
            int Query = Convert.ToInt32(query);
            DataTable dt = new DataTable();
            dt = td.getPlayerNames(null, Query, 1);
            if (dataGridView.InvokeRequired)
            {
                UpdateGridThreadHandler handler = UpdateGrid11;
                dataGridView.BeginInvoke(handler, dt);
            }
            else
            {
                dataGridView.DataSource = dt;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SettingsPage sp = new SettingsPage();
            sp.Visible = true;
            this.Visible = false;
        }
    }
}
