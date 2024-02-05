using HeroManagerOnline;
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

namespace Hero_Manager
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
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
            string salz = GetCurrentDateTime().ToString();

            Debug.WriteLine("Salz:" + salz);

            string password_in_hash = CalculateMD5Hash(password);
            Debug.WriteLine("Password in hash:" + password_in_hash);
            string password_mit_salz_in_hash = CalculateMD5Hash(password_in_hash + salz);

            Debug.WriteLine("Password plus salz in hash:" + password_mit_salz_in_hash);

        }

        public static string CalculateMD5Hash(string input)
        {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("x2"));
                    }

                    return sb.ToString();
                }
        }
        public static string GetCurrentDateTime()
        {
                DateTime now = DateTime.Now;
                return now.ToString();
        }


}
}
