using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreListController : MonoBehaviour {

    public GameObject playerScoreEntryPrefab;

	// Use this for initialization
	void Start () {
        /*
		for (int i =0;i<5;i++)
        {
            GameObject go = Instantiate(playerScoreEntryPrefab) as GameObject;
            go.transform.SetParent(this.transform);

        }
        */
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnEnable()
    {
        DestroyObejctsWithTag("PlayerScore");
        CreateScoreboard();
    }
    public void CreateScoreboard()
    {
        List<Player> ListOfPlayers = Player.PlayersList;
        for (int i =0;i<ListOfPlayers.Count;i++)
        {
            GameObject go = Instantiate(playerScoreEntryPrefab) as GameObject;
            go.transform.SetParent(this.transform);
            string name = ListOfPlayers[i].firstName + " " + ListOfPlayers[i].lastName;
            go.transform.Find("Header :Name").GetComponent<Text>().text = name;
            go.transform.Find("Header :Elo").GetComponent<Text>().text = ListOfPlayers[i].elo.ToString();
            go.transform.Find("Header :Wins").GetComponent<Text>().text = ListOfPlayers[i].wins.ToString();
            go.transform.Find("Header : Loss").GetComponent<Text>().text = ListOfPlayers[i].loses.ToString();
            go.transform.Find("Header : Ties").GetComponent<Text>().text = ListOfPlayers[i].draws.ToString();
            go.transform.Find("Header :Ratio").GetComponent<Text>().text = ListOfPlayers[i].winRate.ToString();


        }
    }

    public List<string> GetPlayerNames()
    {
        List<Player> ListOfPlayers = Player.PlayersList;
        List<string> ListToReturn = new List<string>();
        for (int i =0;i< ListOfPlayers.Count;i++)
        {
            string name = ListOfPlayers[i].firstName + " " + ListOfPlayers[i].lastName;
            ListToReturn.Add(name);
        }
        return ListToReturn;
    }

    //Destroys anyobject with the input tag parameter in the scene
    public static void DestroyObejctsWithTag(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
