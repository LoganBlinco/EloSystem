using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_CreateMatch : MonoBehaviour {

    public GameObject MainMenu;
    public GameObject CreateMatch;

    public Dropdown Player1;
    public Dropdown Player2;
    public Dropdown Result;

    public void OnReturnClicked()
    {
        CreateMatch.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void OnEnable()
    {
        UpdateDropBoxes();
    }
    private void UpdateDropBoxes()
    {
        Player1.options.Clear();
        Player2.options.Clear();

        List<Player> list = Player.PlayersList;
        for (int i = 0; i < list.Count; i++)
        {
            string optionText = list[i].firstName + "  " + list[i].lastName;
            Player1.options.Add(new Dropdown.OptionData() { text = optionText });
            Player2.options.Add(new Dropdown.OptionData() { text = optionText });

        }
        Player1.RefreshShownValue();
        Player2.RefreshShownValue();
    }

    public void OnCreateMatchClicked()
    {
        //value 0 = Player A wins
        //value 1 = Player B wins
        //value 2 = draw

        Player Winner = new Player();
        Player Loser = new Player(); ;
        //Player A wins
        if (Player1.value == Player2.value)
        {
            return;
        }
        if (Result.value == 0)
        {
            Winner = Player.PlayersList[Player1.value];
            Winner.wins += 1;
            Loser = Player.PlayersList[Player2.value];
            Loser.loses += 1;
        }
        //Player B wins
        else if (Result.value == 1)
        {
            Winner = Player.PlayersList[Player2.value];
            Loser = Player.PlayersList[Player1.value];
        }
        //Draw
        else
        {
            return;
        }
        Player.CalculateElo(Winner, Loser);
        OnReturnClicked();
    }

}
