using System;
using UnityEngine;

/// <summary>
/// @author Addison Shuppy
/// The abstract class to lay the basis of all collectable items.
/// </summary>
public abstract class Item : MonoBehaviour
{
    [SerializeField] public Sprite InventorySprite;
    [SerializeField] public AudioClip collectSound;
    [SerializeField] GameObject equipment;
    public Sprite ArchiveSprite;
    public string InventoryDescription, ArchiveDescription;
    public abstract void StartingFunction();
   
}