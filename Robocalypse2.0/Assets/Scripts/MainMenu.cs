using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// @author Addison Shuppy
/// contains functions from the Main Menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    private GameObject GameDataObj;

    public void Start()
    {
        GameDataObj = GameObject.Find("GameData");
        Wipe();
    }
    /// <summary>
    /// Progresses to the next scene.
    /// </summary>
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void Quit()
    {
        #if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void Wipe()
    {
        GameDataObj.GetComponent<GameData>().Wipe();
    }
}
