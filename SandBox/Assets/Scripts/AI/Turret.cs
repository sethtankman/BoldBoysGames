using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @author Addison Shuppy
/// An abstract class for Turrets.
/// </summary>
public abstract class Turret : MonoBehaviour
{
    //[SerializeField] public Sprite InventorySprite;
    //[SerializeField] public AudioClip collectSound;
    //[SerializeField] GameObject equipment;
    //public Sprite ArchiveSprite;
    //public string InventoryDescription, ArchiveDescription;
    //public abstract void StartingFunction();

    /// <summary>
    /// The type of turret this one is.
    /// </summary>
    protected int id;
    /// <summary>
    /// The audioclip to play when a shot is fired.
    /// </summary>
    public AudioClip ShotSound;
    /// <summary>
    /// True if a target is in range.
    /// </summary>
    public bool firing;

    // Update is called once per frame
    public abstract void FireShot();

    /// <summary>
    /// Gets the price of the initial turret.
    /// </summary>
    /// <returns>
    /// </returns>
    public abstract int getPrice();

    /// <summary>
    /// Sets the level.
    /// </summary>
    /// <param name="lvl"> the given parameter to set the level of this turret to. </param>
    public abstract void setLevel(int lvl);

    /// <summary>
    /// Gets the price to upgrade the turret depending on the level
    /// </summary>
    /// <param name="_lvl"> the level of turret to upgrade to. </param>
    /// <returns> the price required to upgrade the turret. </returns>
    public abstract int getUpgradePrice(int _lvl);

    /// <summary>
    /// Calculates the sell price of the turret.
    /// </summary>
    /// <returns> the sell price of the turret. </returns>
    public abstract int getSellPrice();
}
