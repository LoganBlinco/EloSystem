using System.Collections;
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[Serializable]
public class Player {
    
    public static List<Player> PlayersList = new List<Player>();
    public List<Player> personal;
    public static int startingElo = 1000;
    public static int nextID = 0;
    //public static Dictionary<string, Player> Players = new Dictionary<string, Player>();


    public int id;

    //Always lower case
    private string _firsName;
    public string firstName
    {
        get { return _firsName; }
        set { _firsName = value.ToLower(); }
    }
    //Always lower case
    private string _lastName;
    public string lastName
    {
        get { return _lastName; }
        set { _lastName = value.ToLower(); }
    }

    public int wins;
    public int draws;
    public int loses;

    private int _winRate;
    public int winRate
    {
        get
        {
            CalculateWinRate();
            return _winRate;
        }
        set { _winRate = value; }
    }


    public int elo;

    public Player(string _firstName, string _lastName)
    {
        firstName = _firstName;
        lastName = _lastName;
        id = nextID;
        nextID += 1;
        elo = startingElo;

        string fullName = firstName + "  " + lastName;
        //Players.Add(fullName, this);
        PlayersList.Add(this);
    }

    public Player()
    {

    }

    public static void CalculateElo(Player winner,Player loser)
    {
        float mod = 400;
        //Higher score means less volatile
        float K = 32;
        float pointsPerWin = 1.0f;
        float pointsPerDraw = 0.0f;
        float pointsPerLoss = 0.0f;

        float expectedScoreA = 1 / (1 + Mathf.Pow(10, (loser.elo - winner.elo) / mod));
        float expectedScoreB = 1 / (1 + Mathf.Pow(10, (winner.elo - loser.elo) / mod));

        CalculationOfElo(winner,K,pointsPerWin,expectedScoreA);
        CalculationOfElo(loser, K, pointsPerLoss, expectedScoreB);
    }

    public static void CalculationOfElo(Player player,float K , float points, float expectedScore)
    {
        Debug.Log("Previous Elo " + player.elo);
        player.elo = Convert.ToInt16(player.elo + K * (points - expectedScore));
        Debug.Log("Current Elo " + player.elo);
    }

    private void CalculateWinRate()
    {
        int total = wins + loses + draws;
        if (total != 0)
        {
            winRate = 100 * wins / total;
        }
    }

    public static void SaveData(object obj,string filename)
    {
        XmlSerializer sr = new XmlSerializer(obj.GetType());
        TextWriter writer = new StreamWriter(filename);
        sr.Serialize(writer, obj);
        writer.Close();
    }

    public static void LoadData(string filename)
    {
        if (File.Exists("data.xml"))
        {
            XmlSerializer xs = new XmlSerializer(typeof(Player));
            FileStream read = new FileStream("data.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
            Particle info = (Particle)xs.Deserialize(read);
        }
    }


}
