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

namespace Hero_Manager
{
    public partial class Form2 : Form
    {
        
        public int listboxindex;
        public int[] heroid = new int[20];


        private MySQLDatabase database;
        public Form2()
        {
            InitializeComponent();
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listboxindex = listBox1.SelectedIndex;
            Console.WriteLine(listboxindex);
            int selected_hero = heroid[listboxindex];
            DataTable herodata = GetHeroData(selected_hero);
            DataRow herodatarow = herodata.Rows[0];
            name.Text = herodatarow["name"].ToString();
            att.Value = int.Parse(herodatarow["att"].ToString());
            def.Value = int.Parse(herodatarow["def"].ToString());
            comboBox1.SelectedText = "";
            if(herodatarow["klasse"].ToString() == "Barbarian" | herodatarow["klasse"].ToString() == "barbarian")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
        }
        private DataTable GetUserData(int userid)
        {
            database.OpenConnection();
            string query = $"SELECT id, name, klasse, hp, att, def FROM heroes WHERE user_id = '{userid}'";
            DataTable userData = database.ExecuteQuery(query);
            database.CloseConnection();
            return userData;
        }
        private DataTable GetHeroData(int id)
        {
            database.OpenConnection();
            string query = $"SELECT id, name, klasse, hp, att, def FROM heroes WHERE id = '{id}'";
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

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();

            string stringuserid = form1.get_user_id();
            int userid = int.Parse(stringuserid);


            DataTable userdata = GetUserData(userid);
            for (int i = 1; i < userdata.Rows.Count; i++)
            {
                DataRow userdatarow = userdata.Rows[i];
                string id = userdatarow["id"].ToString();
                string heroname = userdatarow["name"].ToString();
                string klasse = userdatarow["klasse"].ToString();
                string hp = userdatarow["hp"].ToString();
                string att = userdatarow["att"].ToString();
                string def = userdatarow["def"].ToString();

                listBox1.Items.Add("Klasse: " + klasse + " Name: " + heroname + " HP: " + hp + " Attack: " + att + " defence: " + def);

                heroid[i] = int.Parse(id);
                Console.WriteLine(heroid[i]);
            }
                
        }

        private void name_Click(object sender, EventArgs e)
        {

        }

        private void att_Scroll(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
