using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{

    public InputField nameInput;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SingleStartGame() //if the button clicked 
    {
        SceneManager.LoadScene("Level_1_Son"); //load the first level
        PlayerPrefs.DeleteAll(); //deleting all datas saved before in game like total shoots number , name etc. 
        PlayerPrefs.SetString("PlayerName",nameInput.text); //save the player name which wrote in input field

    }

    public void MultiStartGame()
    {

    }

    public void QuitGame()
    {
        Application.Quit(); //shut down the game
    }

       
}

