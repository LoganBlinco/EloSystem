using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class PlayersController : MonoBehaviour {

    public GameObject PanelCreatePlayers;
    public GameObject PanelViewPlayers;


    public InputField Input_Forename;
    public InputField Input_Surname;

    public GameObject ScorePrefab;


	// Use this for initialization
	void Start () {
        DisablePanels();
    }

    public void OnEnable()
    {
        DisablePanels();
    }

    public void OnCreatePlayersClicked()
    {
        Debug.Log("Ran");
        PanelCreatePlayers.SetActive(true);
        PanelViewPlayers.SetActive(false);
    }
    public void OnViewPlayersClicked()
    {
        PanelCreatePlayers.SetActive(false);
        PanelViewPlayers.SetActive(true);
        ShowScoreBoard();
    }


    #region Create Player methods

    public void OnCreateClicked()
    {
        string forename = Input_Forename.text;
        string surname = Input_Surname.text;

        if (forename.Length > 1 && surname.Length > 1)
        {
            DatabaseController.AddPlayer(forename, surname,1000);
            Input_Forename.text = "";
            Input_Surname.text = "";
        }
        else
        {
            string title = "Invalid input";
            string message = "You must enter a forename and a surname";
            string okButton = "Ok";
            EditorUtility.DisplayDialog(title, message, okButton);
        }
    }



    #endregion

    #region Scoreboard

    public void ShowScoreBoard()
    {
        string _tag = "Score";
        DestroyObejctsWithTag(_tag);
        PlayerInfo data = DatabaseController.GetHighScores();
        int size = data.firstNames.Count;

        for (int i =0;i<size;i++)
        {
            GameObject tempObject = Instantiate(ScorePrefab);
            tempObject.GetComponent<HighScoresController>().Rank.text = "#"+(i+1).ToString();
            tempObject.GetComponent<HighScoresController>().FirstName.text = data.firstNames[i];
            tempObject.GetComponent<HighScoresController>().LastName.text = data.lastNames[i];
            tempObject.GetComponent<HighScoresController>().Elo.text = data.elo[i].ToString();

            tempObject.transform.parent = GameObject.Find("Scores").transform;
            tempObject.transform.localScale = Vector3.one;
        }
        Debug.Log("Scoreboard showed");
    
    }
    #endregion


    private void DisablePanels()
    {
        PanelCreatePlayers.SetActive(false);
        PanelViewPlayers.SetActive(false);
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
