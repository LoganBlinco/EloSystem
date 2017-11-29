using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_MainMenu : MonoBehaviour {

    public GameObject MainMenu;
    public GameObject CreatePlayer;
    public GameObject CreateMatch;
    public GameObject ShowScoreBoard;

    public void Start()
    {
        MainMenu.SetActive(true);
        CreatePlayer.SetActive(false);
        CreateMatch.SetActive(false);
    }

    public void OnCreatePlayerClicked()
    {
        MainMenu.SetActive(false);
        CreatePlayer.SetActive(true);
    }

    public void OnCreateMatchClicked()
    {
        MainMenu.SetActive(false);
        CreateMatch.SetActive(true);
    }
    public void OnShowScoareboardClicked()
    {
        MainMenu.SetActive(false);
        ShowScoreBoard.SetActive(true);
    }
}
