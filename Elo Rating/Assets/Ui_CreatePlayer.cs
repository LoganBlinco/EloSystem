using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Ui_CreatePlayer : MonoBehaviour {

    public GameObject MainMenu;
    public GameObject CreatePlayer;

    public InputField Input_FirstName;
    public InputField Input_SecondName;

    public void OnCreateClicked()
    {
        string firstName = Input_FirstName.text;
        string secondName = Input_SecondName.text;

        if (firstName != string.Empty)
        {
            Player create = new Player(firstName, secondName);
            Debug.Log("Player created");
            Debug.Log(Player.PlayersList[0].firstName);
            OnReturnClicked();

            ResetValues();
        }
        else
        {
            //Invalid input
            //Create message box

        }
    }

    private void ResetValues()
    {
        Input_FirstName.text = string.Empty;
        Input_SecondName.text = string.Empty;
    }

    public void OnReturnClicked()
    {
        ResetValues();
        CreatePlayer.SetActive(false);
        MainMenu.SetActive(true);
    }


}
