using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// @author Addison Shuppy
/// A buildable square is placed on all turret plates and handles the building of turrets.
/// </summary>
public class BuildableSquare : MonoBehaviour
{
    [SerializeField] private GameObject[] turrets;
    [SerializeField] private Sprite RangeIndicator;

    public AudioClip[] audioClips;
    public SpriteRenderer rend;

    private UIManager _UIManager;
    private MainGuy _player;
    private Turret myTurret;
    private AudioSource _audioSource;
    private float RangeIndicatorSize;
    private int turretPrice, buildLevel;

    /// <summary>
    /// Called when the game starts.
    /// </summary>
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        rend = GetComponent<SpriteRenderer>();
        _player = (MainGuy)FindObjectOfType(typeof(MainGuy));
        rend.enabled = false;
    }
   

    // The mesh shows a green highlight when the mouse enters
    void OnMouseEnter()
    {
        rend.enabled = true;
        DetermineCostText();
    }

    /// <summary>
    /// While the mouse is over the square, checks to see if the right-click
    /// button has been pressed and calls DismantleTurret if it has.
    /// </summary>
    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1))
        {
            DismantleTurret();
        }
    }

    // the mesh loses the highlight when the mouse moves away.
    void OnMouseExit()
    {
        rend.enabled = false;
    }

    // Activates when mouse button comes up.
    private void OnMouseUp()
    {
        if (_player._selectedTurret != null)
        {
            RangeIndicatorSize = _player._selectedTurret.GetComponent<CircleCollider2D>().radius * 0.4f;
            if(myTurret == null)
            {
                turretPrice = _player._selectedTurret.GetComponent<Turret>().getUpgradePrice(buildLevel);
            }
            else { turretPrice = myTurret.getUpgradePrice(buildLevel); }
            if (turretPrice != 0 && _player.gBytes >= turretPrice)
            {
                _player.Transaction(turretPrice - (2 * turretPrice));
                _player.trackMode = true;
                _player.trackLocation = transform.position;
                _player.selectedSquare = this;
                rend.sprite = RangeIndicator;
                transform.localScale = new Vector3(RangeIndicatorSize, RangeIndicatorSize, 1);
                buildLevel++;
            }
        }
    }

    /// <summary>
    /// Builds the selected turret on this square and changes the buildable square to display the range of the turret.
    /// </summary>
    /// <param name="_selectedTurret"> The turret selected by the player to be built. </param>
    public void BuildTurret(Turret _selectedTurret)
    {
        _audioSource.volume = 0.3f;
        _audioSource.PlayOneShot(audioClips[0]);
        if (myTurret == null)
        {
            myTurret = Instantiate(_selectedTurret, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
        myTurret.setLevel(buildLevel);
    }

    /// <summary>
    /// Determines the cost text to display based on what turret has already been built on this buildable square.
    /// </summary>
    public void DetermineCostText()
    {
        if(buildLevel == 0)
        {
            string costText = _player._selectedTurret.GetComponent<Turret>().getPrice().ToString();
            _UIManager.modifyCostText("Cost: " + costText);
        } else if (myTurret!= null && (buildLevel == 1 || buildLevel == 2))
        {
            string costText = myTurret.getUpgradePrice(buildLevel).ToString();
            _UIManager.modifyCostText("Cost: " + costText);
        } else if (myTurret != null)
        {
            _UIManager.modifyCostText("MAXED");
        }
    }

    /// <summary>
    /// Dismantles turrets and sells for gigabytes.
    /// </summary>
    public void DismantleTurret()
    {
        _audioSource.volume = 0.4f;
        _audioSource.PlayOneShot(audioClips[1]);
        _player.Transaction(myTurret.getSellPrice());
        Destroy(myTurret.gameObject);
        myTurret = null;
        buildLevel = 0;
    }
}
