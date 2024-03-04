using HeroManagerOnline;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.X509;
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
        public int[] heroid = new int[100];


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
        // Schreiben von daten auf der Seite wenn man auf ein Heroe clickt
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listboxindex = listBox1.SelectedIndex;
            int selected_hero = heroid[listboxindex];
            DataTable herodata = GetHeroData(selected_hero);
            DataRow herodatarow = herodata.Rows[0];
            att.Value = int.Parse(herodatarow["att"].ToString());
            def.Value = int.Parse(herodatarow["def"].ToString());
            textBox1.Text = herodatarow["name"].ToString();
            comboBox1.SelectedText = "";
            if (herodatarow["klasse"].ToString() == "Barbarian" | herodatarow["klasse"].ToString() == "barbarian")
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
            string query = $"SELECT id, name, klasse, deleted, hp, att, def FROM heroes WHERE user_id = '{userid}' and deleted = 0";
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
        private DataTable UpdateHeroData(int heroid, string klasse, int attackpoints, int defencepoints, string name, int deleted)
        {
            database.OpenConnection();
            string query = $"UPDATE `k215510_b7i-211`.`heroes` SET `klasse` = '{klasse}', `att` = '{attackpoints}', `def` = '{defencepoints}', `name` = '{name}', `deleted` = '{deleted}' WHERE (`id` = '{heroid}');";
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

            Array.Clear(heroid, 0, heroid.Length);
            listBox1.Items.Clear();
            DataTable userdata = GetUserData(userid);
            for (int i = 0; i < userdata.Rows.Count; i++)
            {
                DataRow userdatarow = userdata.Rows[i];
                string id = userdatarow["id"].ToString();
                string heroname = userdatarow["name"].ToString();
                string klasse = userdatarow["klasse"].ToString();
                string hp = userdatarow["hp"].ToString();
                string att = userdatarow["att"].ToString();
                string def = userdatarow["def"].ToString();
                listBox1.Items.Add("Klasse: " + klasse + " Name: " + heroname);
                heroid[i] = int.Parse(id);
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (att.Value > -1 && def.Value > -1 && comboBox1.SelectedIndex > -1 && listBox1.SelectedIndex > 0 && textBox1 != null)
            {
                string klasse = "NULL";
                if (comboBox1.SelectedIndex == 0)
                {
                    klasse = "Barbarian";
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    klasse = "Wizard";
                }

                UpdateHeroData(heroid[listBox1.SelectedIndex], klasse, att.Value, def.Value, textBox1.Text, 0);

                Form1 form1 = new Form1();
                string stringuserid = form1.get_user_id();
                int userid = int.Parse(stringuserid);
                Array.Clear(heroid, 0, heroid.Length);
                listBox1.Items.Clear();
                DataTable userdata = GetUserData(userid);
                for (int i = 0; i < userdata.Rows.Count; i++)
                {
                    DataRow userdatarow = userdata.Rows[i];
                    string id = userdatarow["id"].ToString();
                    string heroname = userdatarow["name"].ToString();
                    string klasse1 = userdatarow["klasse"].ToString();
                    string hp = userdatarow["hp"].ToString();
                    string att = userdatarow["att"].ToString();
                    string def = userdatarow["def"].ToString();

                    listBox1.Items.Add("Klasse: " + klasse1 + " Name: " + heroname);
                    heroid[i] = int.Parse(id);
                }
            }
        }

        private void def_Scroll(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private DataTable AddHeroData(string klasse, int att, int def, string name)
        {
            database.OpenConnection();
            string query = $"INSERT INTO `k215510_b7i-211`.`heroes` (`name`, `klasse`, `hp`, `att`, `def`, `deleted`, `custom`, `user_id`) VALUES ('{name}', '{klasse}', '100', '{att}', '{def}', '0', '0', '4')";
            DataTable userData = database.ExecuteQuery(query);
            database.CloseConnection();
            return userData;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (att.Value > -1 && def.Value > -1 && listBox1.SelectedIndex > -1 && textBox1 != null)
            {
                AddHeroData("Barbarian", att.Value, def.Value, textBox1.Text);
                MessageBox.Show("Hero Added");
            }
            else
            {
                MessageBox.Show("ERROR");
            }


            Form1 form1 = new Form1();
            string stringuserid = form1.get_user_id();
            int userid = int.Parse(stringuserid);
            Array.Clear(heroid, 0, heroid.Length);
            listBox1.Items.Clear();
            DataTable userdata = GetUserData(userid);
            for (int i = 0; i < userdata.Rows.Count; i++)
            {
                DataRow userdatarow = userdata.Rows[i];
                string id = userdatarow["id"].ToString();
                string heroname = userdatarow["name"].ToString();
                string klasse1 = userdatarow["klasse"].ToString();
                string hp = userdatarow["hp"].ToString();
                string att = userdatarow["att"].ToString();
                string def = userdatarow["def"].ToString();

                listBox1.Items.Add("Klasse: " + klasse1 + " Name: " + heroname);
                heroid[i] = int.Parse(id);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (att.Value > -1 && def.Value > -1 && comboBox1.SelectedIndex > -1 && listBox1.SelectedIndex > 0 && textBox1 != null)
            {
                string klasse = "NULL";
                if (comboBox1.SelectedIndex == 0)
                {
                    klasse = "Barbarian";
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    klasse = "Wizard";
                }

                UpdateHeroData(heroid[listBox1.SelectedIndex], klasse, att.Value, def.Value, textBox1.Text, 1);
                MessageBox.Show("Hero Deleted");
            }

            
            Form1 form1 = new Form1();
            string stringuserid = form1.get_user_id();
            int userid = int.Parse(stringuserid);
            Array.Clear(heroid, 0, heroid.Length);
            listBox1.Items.Clear();
            DataTable userdata = GetUserData(userid);
            for (int i = 0; i < userdata.Rows.Count; i++)
            {
                DataRow userdatarow = userdata.Rows[i];
                string id = userdatarow["id"].ToString();
                string heroname = userdatarow["name"].ToString();
                string klasse1 = userdatarow["klasse"].ToString();
                string hp = userdatarow["hp"].ToString();
                string att = userdatarow["att"].ToString();
                string def = userdatarow["def"].ToString();

                listBox1.Items.Add("Klasse: " + klasse1 + " Name: " + heroname);
                heroid[i] = int.Parse(id);
            }
        }
    }
}
