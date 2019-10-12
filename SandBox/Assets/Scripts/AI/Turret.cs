using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    //[SerializeField] public Sprite InventorySprite;
    //[SerializeField] public AudioClip collectSound;
    //[SerializeField] GameObject equipment;
    //public Sprite ArchiveSprite;
    //public string InventoryDescription, ArchiveDescription;
    //public abstract void StartingFunction();
    public int id;
    public AudioClip ShotSound;
    public bool firing;

    // Update is called once per frame
    public abstract void FireShot();
    public abstract int getPrice();
}
