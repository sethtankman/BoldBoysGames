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
    /// The turret in the selected turret box.
    /// </summary>
    private GameObject selectedTurret;

    /// <summary>
    /// The button at the top of the screen that starts the wave of enemies.
    /// </summary>
    public GameObject startWaveButton;

    /// <summary>
    /// The text that displays how many GigaBytes (currency) the player has.
    /// </summary>
    private Text gBText;

    /// <summary>
    /// The list of all audioClips used by the UIManager.
    /// </summary>
    public AudioClip[] audioClips;

    /// <summary>
    /// The audioSource of the UIManager.
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// The text that displays the number of lives you have.
    /// </summary>
    private Text livesText;

    /// <summary>
    /// The number of lives the player has.
    /// </summary>
    [SerializeField] private int lives;
    private Text costText;

    public static bool gamePaused;
    public GameObject pauseMenuUI, InGameUI, InstructionsUI, LosingScreen;
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
        InventoryButtons = new Button[16];
        _audioSource = GetComponent<AudioSource>();
        gamePaused = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (InGameUI.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
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

    public void SetSprite(int num)
    {
        selectedTurretImage.sprite = allTurrets[num].GetComponent<SpriteRenderer>().sprite;
    }

    public void loseLives(int livesLost)
    {
        _audioSource.volume = 1;
        _audioSource.PlayOneShot(audioClips[3]);
        lives -= livesLost;
        livesText.text = lives.ToString();
        if(lives <= 0)
        {
            CameraManager.GetComponent<CameraManager>().PlayTrack(3);
            LosingScreen.SetActive(true);
        }
    }

    public void DisableStartButton()
    {
        startWaveButton.SetActive(false);
    }

    public void EnableStartButton()
    {
        startWaveButton.SetActive(true);
    }

    public void modifyGBytes(string newDisplay)
    {
        gBText.text = newDisplay;
    }

    public void SetTurret(int index)
    {
        GameObject turret = allTurrets[index];
        Player._selectedTurret = turret;
        selectedTurretImage.sprite = turret.GetComponent<SpriteRenderer>().sprite;
        int cost = 0;
        if(index == 0)
        {
            cost = 30;
        } else if(index ==1)
        {
            
        }
        costText.text = "Cost: " + cost;
    }

    public void UISound(int i)
    {
        if(i == 1)
        {
            _audioSource.volume = 1;
            _audioSource.PlayOneShot(audioClips[1]);
        } else if(i == 2)
        {
            _audioSource.volume = 1;
            _audioSource.PlayOneShot(audioClips[2]);
        }
    }

}
