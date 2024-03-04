using HeroManagerOnline;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Hero_Manager
{
    public partial class LOGIN : Form
    {
        private MySQLDatabase database;
        string userid = "4";

        public LOGIN()
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
            MessageBox.Show("Show data");
            DataTable userData = GetUserData(textBox1.Text);
            if (userData.Rows.Count > 0)
            {
                DataRow userRow = userData.Rows[0];
                string storedHash = userRow["passwordhash"].ToString();
                string storedSalt = userRow["salt"].ToString();
                string storedId = userRow["id"].ToString();

                if (HashPassword(textBox2.Text, storedSalt) == storedHash)
                {
                    Form2 form2 = new Form2();
                    form2.Show();
                }
                else
                {
                    MessageBox.Show("Wrong Password! Pleace try again");
                }
            }
            else
            {
                Console.WriteLine("Null userdata");
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
        private DataTable GetUserData(string username)
        {
            database.OpenConnection();
            string query = $"SELECT passwordhash, salt, id FROM user WHERE username = '{username}'";
            DataTable userData = database.ExecuteQuery(query);
            database.CloseConnection();
            return userData;
        }
        public void open_connection()
        {
            string myConnectionString = "server=91.204.46.137;database=k215510_b7i-211;uid=k215510_b7i-211;pwd=Er$1234Er$;";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            try
            {
                connection.Open();
                MessageBox.Show("Connection Open!");
                connection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot open connection!");
            }

            
        }
        private string HashPassword(string password, string salt)
        {
            string hash1 = sha256_hash(password);
            string hash2 = sha256_hash(hash1 + salt);
            Debug.WriteLine(hash2);
            return hash2;
        }

        public static String sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();
            using (var hash = System.Security.Cryptography.SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }
        public string get_user_id()
        {
            Console.WriteLine(userid);
            return userid;
        }




    }
}
