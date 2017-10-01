using System;
using System.Windows.Forms;

namespace Team_Database
{
    public partial class SettingsPage : Form
    {
        private Form1 f = new Form1();
        private TeamDetailsDB teamdetails = new TeamDetailsDB();
        private TeamsDB teams = new TeamsDB();
        public SettingsPage()
        {
            InitializeComponent();
            column1Txt.Visible = false;
            column2Txt.Visible = false;
            column3Txt.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loadAdd(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                Column1Lbl.Text = "Team Name :";
                Column2Lbl.Text = "Team Captain :";
                Column3Lbl.Visible = false;
                column3Txt.Visible = false;
                column1Txt.Visible = true;
                column2Txt.Visible = true;
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                Column1Lbl.Text = "Player ID :";
                Column2Lbl.Text = "Player Name :";
                Column3Lbl.Text = "Team ID :";
                column3Txt.Visible = true;
                column1Txt.Visible = true;
                column2Txt.Visible = true;
                Column3Lbl.Visible = true;

            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            AddBtn.Enabled = false;
            if(comboBox1.SelectedIndex == 0)
            {
                bool status = teams.add(column1Txt.Text.Trim(),column2Txt.Text.Trim());
                if (status == false)
                {
                    MessageBox.Show("Failed!");
                }
                else
                {
                    MessageBox.Show("Successful!");
                }
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                bool status = teamdetails.add(column2Txt.Text,Convert.ToInt32(column3Txt.Text.Trim()));
                if(status == false)
                {
                    MessageBox.Show("Failed!");
                }
                else
                {
                    MessageBox.Show("Successful!");
                }
            }
            AddBtn.Enabled = true;
        }

        private void loadDatabase(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex == 0)
            {
                dataGridView.DataSource = teams.getDB();
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                dataGridView.DataSource = teamdetails.getDB();
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                string command1 = @"Delete from teams where " + textBox1.Text.Trim() + " = '" + textBox2.Text.Trim() + "' ";
                string command2 = @"Delete from teamdetails where " + textBox1.Text.Trim() + " = '" + textBox2.Text.Trim() + "' ";
                teamdetails.executeCommand(command2).ToString();
                int affectedRows  = teams.executeCommand(command1);
                MessageBox.Show("No. of rows affected: " + affectedRows.ToString());

            }
            else if(comboBox3.SelectedIndex == 1)
            {
                string command = @"Delete from teamdetails where " + textBox1.Text.Trim() + " = '" + textBox2.Text.Trim() + "' ";
                MessageBox.Show("Affected Rows: " + teamdetails.executeCommand(command).ToString());
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void modifyBtn_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex == 0)
            {
                string command = "update teams set " + modifyColumn.Text.Trim() + " = '" + newValue.Text.Trim() + "' where " + knownColumn.Text.Trim() + " = '" + knownValue.Text.Trim() + "' ";
                int affectedRows = teams.executeCommand(command);
                if (affectedRows == -1)
                {
                    MessageBox.Show("Modification Failed!");
                    return;
                }
                else
                {
                    MessageBox.Show("No. of rows affected: " + affectedRows);
                }
            }
            else if (comboBox4.SelectedIndex == 1)
            {
                string command = "update teamdetails set " + modifyColumn.Text.Trim() + " = '" + newValue.Text.Trim() + "' where " + knownColumn.Text.Trim() + " = '" + knownValue.Text.Trim() + "' ";
                int affectedRows = teamdetails.executeCommand(command);
                if (affectedRows == -1)
                {
                    MessageBox.Show("Modification Failed!");
                    return;
                }
                else
                {
                    MessageBox.Show("No. of rows affected: " + affectedRows);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            f.Visible = true;
            this.Visible = false;
        }
    }
}
