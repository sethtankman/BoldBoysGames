using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildableSquare : MonoBehaviour
{


    // 0 = basic turret
    [SerializeField] private GameObject[] turrets;
    [SerializeField] private Sprite RangeIndicator;
    private UIManager _UIManager;
    private float RangeIndicatorSize;
    public SpriteRenderer rend;
    private int turretPrice, buildLevel;
    private MainGuy _player;
    private Turret myTurret;
    public AudioClip[] audioClips;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        rend = GetComponent<SpriteRenderer>();
        _player = (MainGuy)FindObjectOfType(typeof(MainGuy));
        rend.enabled = false;
    }

    private void Update()
    {
        
    }

    // The mesh shows a green highlight when the mouse enters
    void OnMouseEnter()
    {
        rend.enabled = true;
        DetermineCostText();
    }

    // maybe use this to add an effect while the mouse hovers over it.
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

    //if clicked
    //add sprite on top of current sprite, set script to that building's script? 
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

    public void DetermineCostText()
    {
        if(buildLevel == 0)
        {
            string costText = _player._selectedTurret.GetComponent<Turret>().getPrice().ToString();
            _UIManager.modifyCostText("Cost: " + costText);
        } else if (buildLevel == 1 || buildLevel == 2 && myTurret != null)
        {
            string costText = myTurret.getUpgradePrice(buildLevel).ToString();
            _UIManager.modifyCostText("Cost: " + costText);
        } else { _UIManager.modifyCostText("Cannot Upgrade"); }
    }

    public void DismantleTurret()
    {
        _audioSource.volume = 0.4f;
        _audioSource.PlayOneShot(audioClips[1]);
        _player.Transaction(myTurret.getSellPrice());
        //Destroy turret
        Destroy(myTurret.gameObject);
        myTurret = null;
        //Reset buildLevel
        buildLevel = 0;
    }
}
