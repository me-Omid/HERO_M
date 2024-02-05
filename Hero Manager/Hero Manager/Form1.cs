using HeroManagerOnline;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Hero_Manager
{
    public partial class Form1 : Form
    {
        private MySQLDatabase database;

        public Form1()
        {
            InitializeComponent();
            // Initialize MySQLDatabase with your MySQL server details 

            database = new MySQLDatabase("91.204.46.137", "k215510_b7i-211", "k215510_b7i-211", "Er$1234Er$");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "123" && textBox2.Text == "123")
            {
               
                Form2 form2 = new Form2();
                form2.Show();
            }
            else
            {
                MessageBox.Show("Wrong Password! Pleace try again");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void open_connection()
        {
            string myConnectionString = "server=91.204.46.137;database=k215510_b7i-211;uid=k215510_b7i-211;pwd=Er$1234Er$;";
            MySqlConnection cnn = new MySqlConnection(myConnectionString);
            try
            {
                cnn.Open();
                MessageBox.Show("Connection Open!");
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot open connection!");
            }
        }




    }
}
