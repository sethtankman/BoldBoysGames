using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildableSquare : MonoBehaviour
{

    
    // 0 = basic turret
    [SerializeField]
    private GameObject[] turrets;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
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
        MainGuy player = (MainGuy)FindObjectOfType(typeof(MainGuy));
        player.trackMode = true;
        player.trackLocation = transform.position;
    }
}
