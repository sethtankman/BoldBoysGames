using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Made By Addison Shuppy June 2019
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// The list of all turrets in the game.
    /// </summary>
    [SerializeField] private GameObject[] allTurrets = new GameObject[2];

    /// <summary>
    /// The image of the turret ready to place on the UI.
    /// </summary>
    [SerializeField] private Image selectedTurretImage;

    /// <summary>
    /// The number of lives the player has.
    /// </summary>
    [SerializeField] private int lives;

    public static bool gamePaused;
    public GameObject[] TurretButtons;
    public GameObject pauseMenuUI, InGameUI, InstructionsUI, LosingScreen, startWaveButton;

    /// <summary>
    /// The list of all audioClips used by the UIManager.
    /// </summary>
    public AudioClip[] audioClips;

    /// <summary>
    /// The turret in the selected turret box.
    /// </summary>
    private GameObject selectedTurret;

    /// <summary>
    /// The text that displays how many GigaBytes (currency) the player has.
    /// </summary>
    private Text gBText;


    /// <summary>
    /// The audioSource of the UIManager.
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// The text that displays the number of lives you have.
    /// </summary>
    private Text livesText;

    private Text costText;

    private GameObject BuildableSquares, CameraManager;
    private MainGuy Player;
    private Button[] InventoryButtons;

    // Start is called before the first frame update
    void Start()
    {
        BuildableSquares = GameObject.Find("BuildableSquares");
        CameraManager = GameObject.Find("MainCamera");
        gBText = GameObject.Find("GigaBytes").GetComponent<Text>();
        livesText = GameObject.Find("LivesText").GetComponent<Text>();
        costText = GameObject.Find("CostText").GetComponent<Text>();
        Player = GameObject.Find("MainGuy").GetComponent<MainGuy>();
        gBText.text = "" + Player.gBytes;
        InventoryButtons = new Button[16];
        //Unpause the game when it is loaded.
        LosingScreen.SetActive(false);
        BuildableSquares.SetActive(true);
        InGameUI.SetActive(true);
        Time.timeScale = 1;
        gamePaused = false;

        _audioSource = GetComponent<AudioSource>();
        gamePaused = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (InGameUI.activeInHierarchy && 
            (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            InstructionsUI.SetActive(false);
        }
    }

    /// <summary>
    /// Plays the pause sound, deactivates in-game UI, stops time, enables pause menu UI.
    /// </summary>
    public void Pause()
    {
        _audioSource.volume = 0.5f;
        _audioSource.PlayOneShot(audioClips[0]);
        BuildableSquares.SetActive(false);
        InGameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true;
    }

    /// <summary>
    /// Activates in-game UI, resumes time, closes pause menu.
    /// </summary>
    public void Resume()
    {
        BuildableSquares.SetActive(true);
        InGameUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gamePaused = false;
    }

    /// <summary>
    /// Sets the selected turret image.
    /// </summary>
    /// <param name="num"> The index of the turret sprite to be displayed. </param>
    public void SetSprite(int num)
    {
        selectedTurretImage.sprite = allTurrets[num].GetComponent<SpriteRenderer>().sprite;
    }

    /// <summary>
    /// Loses lives and deals with consequences of running out of lives, including dying.
    /// </summary>
    /// <param name="livesLost"> The number of lives to lose. </param>
    public void loseLives(int livesLost)
    {
        _audioSource.volume = 1;
        _audioSource.PlayOneShot(audioClips[3]);
        lives -= livesLost;
        livesText.text = lives.ToString();
        if (lives <= 0)
        {
            CameraManager.GetComponent<CameraManager>().PlayTrack(3);
            LosingScreen.SetActive(true);
            BuildableSquares.SetActive(false);
            InGameUI.SetActive(false);
            Time.timeScale = 0;
            gamePaused = true;
        }
    }


    /// <summary>
    /// Disables the start button until the level is completed.
    /// </summary>
    public void DisableStartButton()
    {
        startWaveButton.SetActive(false);
    }

    /// <summary>
    /// Enables the start button to move on to the next wave. 
    /// </summary>
    public void EnableStartButton()
    {
        startWaveButton.SetActive(true);
    }

    /// <summary>
    /// Enables the button of the turret just collected. 
    /// </summary>
    /// <param name="buttonNum"> the index of the button on the list of all turret buttons. </param>
    public void EnableTurretButton(int buttonNum)
    {
        TurretButtons[buttonNum].SetActive(true);
    }

    /// <summary>
    /// Changes the text that displays the number of gigabytes the player has.
    /// </summary>
    /// <param name="newDisplay"> the new value to display </param>
    public void modifyGBytesText(string newDisplay)
    {
        gBText.text = newDisplay;
    }

    /// <summary>
    /// Changes the cost text on the UI.
    /// </summary>
    /// <param name="newDisplay"> the text to display. </param>
    public void modifyCostText(string newDisplay)
    {
        costText.text = newDisplay;
    }

    /// <summary>
    /// Sets the turret to the given index and reports the cost in the UI.
    /// </summary>
    /// <param name="index"> The index of the turret in the list of all turrets. </param>
    public void SetTurret(int index)
    {
        GameObject turret = allTurrets[index];
        Player._selectedTurret = turret;
        selectedTurretImage.sprite = turret.GetComponent<SpriteRenderer>().sprite;
        int cost = turret.GetComponent<Turret>().getPrice();
        costText.text = "Cost: " + cost;
    }

    /// <summary>
    /// Plays UI sounds
    /// </summary>
    /// <param name="i"> the index of the UI sound in the array of sounds to be played.</param>
    public void UISound(int i)
    {
        _audioSource.volume = 1;
        _audioSource.PlayOneShot(audioClips[i]);
    }

}
