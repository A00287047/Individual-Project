using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //Restarts main scene
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //Closes Game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
}

