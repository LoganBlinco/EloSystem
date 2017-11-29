using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_ScoreBoard : MonoBehaviour {

    public GameObject MainMenu;
    public GameObject ShowScoreBoard;

    public void OnReturnClicked()
    {
        ShowScoreBoard.SetActive(false);
        MainMenu.SetActive(true);
    }

}
