using HeroManagerOnline;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hero_Manager
{
    public partial class Form3 : Form
    {
        private MySQLDatabase database;
        public Form3()
        {
            InitializeComponent();
            database = new MySQLDatabase("91.204.46.137", "k215510_b7i-211", "k215510_b7i-211", "Er$1234Er$");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string salz = GetCurrentDateTime();
            Console.WriteLine(salz);
            string passwordhashd = HashPassword(password, salz);

            if(username != null && passwordhashd != null)
            {
                SetUserData(username, passwordhashd, salz);
                MessageBox.Show("User Added");
                Close();
            }


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
        private string HashPassword(string password, string salt)
        {
            string hash1 = sha256_hash(password);
            string hash2 = sha256_hash(hash1 + salt);
            Debug.WriteLine(hash2);
            return hash2;
        }




        private void SetUserData(string username, string password, string salz)
        {
            database.OpenConnection();
            string query = $"INSERT INTO `k215510_b7i-211`.`user` (`username`, `passwordhash`, `salt`) VALUES ('{username}', '{password}', '{salz}')";
            DataTable userData = database.ExecuteQuery(query);
            database.CloseConnection();
        }
        public static string GetCurrentDateTime()
        {
                DateTime now = DateTime.Now;
                return now.ToString();
        }


}
}
