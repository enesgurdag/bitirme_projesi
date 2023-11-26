using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScoreBoardManagerScript : MonoBehaviour
{
    private int currentSceneIndex;
    public Button button;
    public Button quitButton;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ExampleCoroutine()
    {
        quitButton.gameObject.SetActive(false); //if the game has not finished , do not show the button
        button.gameObject.SetActive(false); //if the game has not finished , do not show the button
        text.gameObject.SetActive(false);//if the game has not finished , do not show the text
        currentSceneIndex = PlayerPrefs.GetInt("CurrentSceneIndex"); //for get the last active level scene index from datastore.
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 4 seconds.
        yield return new WaitForSeconds(4); //give the delay to the game for showing score board

        //After we have waited 4 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);


        if (currentSceneIndex < 10)
        {

            SceneManager.LoadScene(currentSceneIndex + 1); //for load the next level scene
        }
        else
        {
            text.gameObject.SetActive(true); //if the game has finished, show the text
            button.gameObject.SetActive(true); //if the game has finished, show the button
            quitButton.gameObject.SetActive(true); //if the game has finished, show the button
        }


    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MenuScene"); //if button clicked , load the main menu scene
    }
    public void QuitGame()
    {
        Application.Quit(); //if the button clicked , shut down the game
    }
}
