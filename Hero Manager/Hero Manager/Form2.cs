using HeroManagerOnline;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hero_Manager
{
    public partial class Form2 : Form
    {
        private MySQLDatabase database;
        public Form2()
        {
            InitializeComponent();
            // Initialize MySQLDatabase with your MySQL server details 

            database = new MySQLDatabase("91.204.46.137", "k215510_b7i-211", "k215510_b7i-211", "Er$1234Er$");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            string userid = form1.UserID;
            DataTable userData = GetUserData();
            if (userData.Rows.Count > 0)
            {
                for (int i = 0; i < userData.Rows.Count; i++)
                {
                    DataRow userRow = userData.Rows[i];
                    string storedid = userRow["id"].ToString();
                    string storedname = userRow["name"].ToString();
                    string storedklasse = userRow["klasse"].ToString();

                    comboBox1.Items.Add("ID:" + storedid + "Username:" + storedname + "Hero:" + storedklasse);

                    string[] hero0 = {storedid, storedname, storedklasse};

                    

                }
                
            }
            else
            {
                MessageBox.Show("Keine Verbindung zur Datenbank");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private DataTable GetUserData(int userid = 4)
        {
            database.OpenConnection();
            string query = $"SELECT id, name, klasse FROM heroes WHERE user_id = '{userid}'";
            DataTable userData = database.ExecuteQuery(query);
            database.CloseConnection();
            return userData;
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
