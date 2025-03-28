using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartManager : MonoBehaviour
{

    private void Update()
    {
        
    }

    public void GameStart()
    {
        //load the game scene (SampleScene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameExit()
    {
        //Close the game
        Application.Quit(0);

        //Closes the editor if it is open rather than a build
        UnityEditor.EditorApplication.isPlaying = false;
    }
}