using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Team_Database
{
    class TeamDetailsDB
    {
        public string connectionString;
        public SqlConnection con;
        public SqlDataAdapter sda;
        public TeamDetailsDB(string ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; AttachDbFilename='C:\Users\Adarsh\source\repos\Team Database\Team Database\TeamDB.mdf';Integrated Security = True")
        {
            connectionString = ConnectionString;
            con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                con.Close();
            }
            catch (Exception e) { throw e; }
        }
        public DataTable getPlayerNames(string TeamName, int TeamID, int queryType)
        {
            string commandTxt1 = @"select playername from teamdetails where teamid = "+TeamID;
            string commandTxt0 = @"select playername from teamdetails inner join Teams on Teams.teamId = Teamdetails.teamid and teams.teamname = '"+ TeamName+"'";
            DataTable players = new DataTable();
            if(queryType == 0)
            {
                sda = new SqlDataAdapter(commandTxt0, con);
                sda.Fill(players);
            }
            else
            {
                sda = new SqlDataAdapter(commandTxt1, con);
                sda.Fill(players);
            }
            return players;
        }
        public DataTable getDB()
        {
            string command = "select * from teamdetails";
            SqlDataAdapter sda = new SqlDataAdapter(command, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        public int executeCommand(string commandString)
        {
            int affectedRows;
            try
            {
                SqlCommand command = new SqlCommand(commandString, con);
                con.Open();
                affectedRows = command.ExecuteNonQuery();
                con.Close();
                return affectedRows;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public bool add(string PlayerName, int teamID)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = "INSERT into TeamDetails (PlayerName, TeamID) VALUES (@PlayerName, @TeamID)";
            command.Parameters.AddWithValue("@PlayerName", PlayerName);
            command.Parameters.AddWithValue("@TeamID", teamID);

            try
            {
                if(con.State == ConnectionState.Closed)
                    con.Open();
                int recordsAffected = command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
    }


    class TeamsDB
    {
        public SqlConnection con;
        public TeamsDB(string ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; AttachDbFilename='C:\Users\Adarsh\source\repos\Team Database\Team Database\TeamDB.mdf';Integrated Security = True")
        {

            string connectionString = ConnectionString;
            con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                con.Close();
            }
            catch (Exception e) { throw e; }
        }
        public int executeCommand(string commandString)
        {
            int affectedRows;
            try
            {
                SqlCommand command = new SqlCommand(commandString, con);
                con.Open();
                affectedRows = command.ExecuteNonQuery();
                con.Close();
                return affectedRows;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public DataTable getDB()
        {
            string command = "select * from teams";
            SqlDataAdapter sda = new SqlDataAdapter(command, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        public bool add(string teamName, string teamCaptain)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = "INSERT into Teams (TeamName, TeamCaptain) VALUES (@teamName, @teamCaptain)";
            command.Parameters.AddWithValue("@teamName", teamName);
            command.Parameters.AddWithValue("@teamCaptain", teamCaptain);

            try
            {
                con.Open();
                int recordsAffected = command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
    }
}
