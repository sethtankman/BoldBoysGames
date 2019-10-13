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
    private float RangeIndicatorSize;
    public SpriteRenderer rend;
    private int turretPrice;
    private MainGuy _player;


    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        _player = (MainGuy)FindObjectOfType(typeof(MainGuy));
    }

    private void Update()
    {
    }

    // The mesh shows a green highlight when the mouse enters
    void OnMouseEnter()
    {
        rend.enabled = true;
    }

    // maybe use this to add an effect while the mouse hovers over it.
    void OnMouseOver()
    {
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
        if(_player._selectedTurret == null) { return; }
        turretPrice = _player._selectedTurret.GetComponent<Turret>().getPrice();
        RangeIndicatorSize = _player._selectedTurret.GetComponent<CircleCollider2D>().radius * 0.4f;
        if (turretPrice != 0 && _player.gBytes >= turretPrice)
        {
            _player.Transaction(turretPrice - (2*turretPrice));
            _player.trackMode = true;
            _player.trackLocation = transform.position;
            rend.sprite = RangeIndicator;
            transform.localScale = new Vector3(RangeIndicatorSize, RangeIndicatorSize, 1);
        }
    }
}
