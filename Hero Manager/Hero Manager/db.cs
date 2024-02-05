using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using MySql.Data.MySqlClient;

using System.Data;



namespace HeroManagerOnline

{



    public class MySQLDatabase

    {

        private MySqlConnection connection;

        private string server;

        private string database;

        private string username;

        private string password;



        // Constructor 

        public MySQLDatabase(string server, string database, string username, string password)

        {

            this.server = server;

            this.database = database;

            this.username = username;

            this.password = password;

        }



        // Method to open a connection to the MySQL database 

        public void OpenConnection()

        {

            string connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";

            connection = new MySqlConnection(connectionString);



            try

            {

                connection.Open();

                Console.WriteLine("MySQL connection opened successfully!");

            }

            catch (MySqlException ex)

            {

                Console.WriteLine("Error: " + ex.Message);

            }

        }



        // Method to close the connection to the MySQL database 

        public void CloseConnection()

        {

            try

            {

                connection.Close();

                Console.WriteLine("MySQL connection closed successfully!");

            }

            catch (MySqlException ex)

            {

                Console.WriteLine("Error: " + ex.Message);

            }

        }



        // Method to execute a query that returns a DataTable 

        public DataTable ExecuteQuery(string query)

        {

            DataTable dataTable = new DataTable();



            try

            {

                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(dataTable);

            }

            catch (MySqlException ex)

            {

                Console.WriteLine("Error: " + ex.Message);

            }



            return dataTable;

        }



    }



}