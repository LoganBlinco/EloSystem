using Mono.Data.SqliteClient;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;
using System;

public class DatabaseController : MonoBehaviour {

    static string filename = "PlayerDatabase";
    static IDbConnection databaseConnection;


    // Use this for initialization
    void Start () {
        CreateConnection();
        CreatePlayerTable();
    }

    private static void CreatePlayerTable()
    {
        databaseConnection.Open();

        string sqlCommand = @"CREATE TABLE IF NOT EXISTS Players(
ID INTEGER NOT NULL PRIMARY KEY,
FirstName VARCHAR(20) NOT NULL,
LastName VARCHAR(20) NOT NULL,
ELO int NOT NULL)";

        IDbCommand command = databaseConnection.CreateCommand();
        command.CommandText = sqlCommand;
        command.ExecuteReader();
        databaseConnection.Close();
    }

    private static void CreateConnection()
    {
        string connectionString = "URI=file:" + Application.dataPath + "/"+filename+".db";
        databaseConnection = (IDbConnection)new SqliteConnection(connectionString);
        Debug.Log("Connection created");
    }

    public static void addMatchPlayed(int id)
    {
        databaseConnection.Open();

        //get number of games played
        string sqlCommand = String.Format("SELECT gamesPlayed FROM Players WHERE ID = {0}",id);
        IDbCommand command = databaseConnection.CreateCommand();
        command.CommandText = sqlCommand;
        IDataReader reader = command.ExecuteReader();

        int gamesPlayed =0;
        while (reader.Read())
        {
            gamesPlayed = (int)reader["gamesPlayed"];
        }

        //add to the games played
        gamesPlayed += 1;
        sqlCommand = string.Format("UPDATE Players SET gamesPlayed = {0} WHERE ID = {1}", gamesPlayed, id);
        command = databaseConnection.CreateCommand();
        command.CommandText = sqlCommand;
        reader = command.ExecuteReader();

        databaseConnection.Close();


    }


    public static void AddPlayer(string FirstName, string LastName,int elo)
    {
        databaseConnection.Open();

        string sqlCommand = String.Format("INSERT INTO Players(FirstName,LastName,ELO,gamesPlayed) values('{0}','{1}','{2}', '0')", FirstName, LastName, elo);
        IDbCommand command = databaseConnection.CreateCommand();
        command.CommandText = sqlCommand;
        IDataReader reader = command.ExecuteReader();

        databaseConnection.Close();
    }
    public static PlayerInfo GetHighScores()
    {
        databaseConnection.Open();

        string sqlCommand = "SELECT FirstName,LastName,ELO FROM Players ORDER BY ELO DESC";
        IDbCommand command = databaseConnection.CreateCommand();
        command.CommandText = sqlCommand;
        IDataReader reader = command.ExecuteReader();


        PlayerInfo data = new PlayerInfo();
        while (reader.Read())
        {
            data.firstNames.Add(reader["FirstName"].ToString());
            data.lastNames.Add(reader["LastName"].ToString());
            data.elo.Add(Convert.ToInt16(reader["ELO"]));
        }

        databaseConnection.Close();
        return data;
    }

    public static List<string> GetFirstNames()
    {
        databaseConnection.Open();
        string sqlCommand = "SELECT FirstName FROM Players";
        IDbCommand command = databaseConnection.CreateCommand();
        command.CommandText = sqlCommand;
        IDataReader reader = command.ExecuteReader();

        List<string> names = new List<string>();
        while (reader.Read())
        {
            names.Add(reader["FirstName"].ToString());
        }
        databaseConnection.Close();
        return names;
    }


    public static List<string> GetLastNames()
    {
        databaseConnection.Open();
        string sqlCommand = "SELECT LastName FROM Players";
        IDbCommand command = databaseConnection.CreateCommand();
        command.CommandText = sqlCommand;
        IDataReader reader = command.ExecuteReader();

        List<string> names = new List<string>();
        while (reader.Read())
        {
            names.Add(reader["LastName"].ToString());
        }
        databaseConnection.Close();
        return names;
    }

    public static int GetElo(int id)
    {
        databaseConnection.Open();
        string sqlCommand = string.Format("SELECT ELO FROM Players WHERE ID = {0}", id);
        IDbCommand command = databaseConnection.CreateCommand();
        command.CommandText = sqlCommand;
        IDataReader reader = command.ExecuteReader();
        databaseConnection.Close();
        int value =0;
        while (reader.Read())
        {
             value = (int)reader["ELO"];
        }
        return value;
    }

    public static void SetElo(int id , int newELo)
    {
        databaseConnection.Open();
        string sqlCommand = string.Format("UPDATE Players SET ELO = {0} WHERE ID = {1}", newELo, id);
        IDbCommand command = databaseConnection.CreateCommand();
        command.CommandText = sqlCommand;
        IDataReader reader = command.ExecuteReader();
    }


}
