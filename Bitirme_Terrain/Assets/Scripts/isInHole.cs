using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class isInHole : MonoBehaviour
{
    public int currentSceneIndex;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoundObject")) //check for is the object's tag is BoundObject or not
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //get the active level's index
            PlayerPrefs.SetInt("CurrentSceneIndex", currentSceneIndex); //save the active level index
            SceneManager.LoadScene("ScoreBoardUI"); //load the score board scene
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
