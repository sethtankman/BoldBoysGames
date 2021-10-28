using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @author Addison Shuppy
/// Closes the game from the pause menu.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Quits the game if the player hits the quit button.
    /// </summary>
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
