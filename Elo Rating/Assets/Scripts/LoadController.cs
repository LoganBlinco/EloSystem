using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using UnityEditor;
using UnityEngine;

public class LoadController : MonoBehaviour {

    public GameObject PanelPlayers;
    public GameObject PanelCreatePlayer;
    public GameObject PanelViewPlayers;

    public GameObject PanelMatches;
    public Dropdown Winner;
    public Dropdown loser;


	// Use this for initialization
	void Start () {
        PanelPlayers.SetActive(false);
        PanelCreatePlayer.SetActive(false);
        PanelViewPlayers.SetActive(false);
        PanelMatches.SetActive(false);
    }
	
    #region Players

    public void OnPlayersClicked()
    {
        PanelPlayers.SetActive(true);
        PanelCreatePlayer.SetActive(false);
        PanelViewPlayers.SetActive(false);
        PanelMatches.SetActive(false);

    }
    #endregion

    #region Matches Events

    public void OnMatchesClicked()
    {
        PanelMatches.SetActive(true);
        PanelPlayers.SetActive(false);
        LoadMatches();
    }
    public void OnMatchCreateClicked()
    {
        int winnerID = Winner.value+1;
        int loserID = loser.value+1;

        if (winnerID != loserID)
        {
            EloCalculator.CalculateElo(winnerID, loserID);
            DatabaseController.addMatchPlayed(winnerID);
            DatabaseController.addMatchPlayed(loserID);
        }
        else
        {
            string title = "Invalid Input";
            string message = "How are they playing themselfs?";
            //EditorUtility.DisplayDialog(title,message,"Ok");
        }
    }



    private void LoadMatches()
    {
        UpdateDropBoxes();
    }

    private void UpdateDropBoxes()
    {
        Winner.options.Clear();
        loser.options.Clear();

        List<string> firstNames = DatabaseController.GetFirstNames();
        List<string> lastNames = DatabaseController.GetLastNames();


        for (int i = 0; i < firstNames.Count; i++)
        {
            string optionText = firstNames[i] + "  " + lastNames[i];
            Winner.options.Add(new Dropdown.OptionData() { text = optionText });
            loser.options.Add(new Dropdown.OptionData() { text = optionText });

        }
        Winner.RefreshShownValue();
        loser.RefreshShownValue();
    }

    #endregion
}
