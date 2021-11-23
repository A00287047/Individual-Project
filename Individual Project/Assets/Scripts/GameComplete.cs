using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameComplete : MonoBehaviour
{
    // Loads main menu again after Game complete
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
