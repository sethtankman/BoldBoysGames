using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    [SerializeField] private GameObject[] turretButtons = new GameObject[2];

    public static Dictionary<int, Item> items = new Dictionary<int, Item>();
    public static Dictionary<int, Tuple<Sprite, string>> archives = new Dictionary<int, Tuple<Sprite, string>>();
    public static int level, turretsFound;

    private UIManager _uIManager;
    
    /// <summary>
    /// Performs the item's starting function and adds it to the lowest empty slot in the inventory.
    /// </summary>
    /// <param name="item"> The item to be added. </param>
    public void AddItem(Item item)
    {
        item.StartingFunction();
        items.Add(LowestEmptySlot(false), item);
    }

    /// <summary>
    /// Increments the number of turrets found in the game.
    /// </summary>
    public void incrementTurretCount()
    {
        turretsFound++;
    }

    /// <summary>
    /// Retrieves items and data from the previously saved game and loads them to the different UI elements.
    /// </summary>
    public void LoadItemsAndTurrets()
    {
        if (items == null)
        {
            items = new Dictionary<int, Item>();
        }
        for (int i = 0; i < items.Count; i++)
        {
            if (items.ContainsKey(i) && items[i] is BluePrint)
            {
                BluePrint bp = (BluePrint)items[i];
            }
        }
        for(int j =0; j < turretsFound; j++)
        {
            turretButtons[j].SetActive(true);
        }
    }

    // Called when a level is loaded.  Retrieves information from the UIManager and loads data from the previous scene.
    public void OnLevelWasLoaded(int level)
    {
        _uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        if(_uIManager == null)
        {
            Debug.Log("UIManager not found");
        }
        LoadItemsAndTurrets();

    }

    /// <summary>
    /// Adds the given sprite and text to the GameData Archives.
    /// </summary>
    /// <param name="_sprite"> The sprite to be added. </param>
    /// <param name="_string"> The description to be added. </param>
    public void AddToArchive(Sprite _sprite, string _string)
    {
        if(archives == null)
        {
            archives = new Dictionary<int, Tuple<Sprite, string>>();
        }
        Tuple<Sprite, string> archiveEntry = new Tuple<Sprite, string>(_sprite, _string);
        archives.Add(LowestEmptySlot(true), archiveEntry);
    }

    /// <summary>
    /// Finds the lowest empty slot either in the items inventory or the archives menu.
    /// </summary>
    /// <param name="isArchive"> Whether or not the lowest empty slot is to be found from the archive or 
    /// the items inventory. 
    /// </param>
    /// <returns> the lowest empty slot integer. </returns>
    private int LowestEmptySlot(bool isArchive)
    {
        int i = 0;
        while (!isArchive && items.ContainsKey(i))
        {
            i++;
        }
        while (isArchive && archives.ContainsKey(i))
        {
            i++;
        }
        return i;
    }


    /// <summary>
    /// Initializes all buttons in the Inventory and Archive scrollview.
    /// </summary>
    /// <param name="isArchive"> determines whether to initialize the Inventory or the Archive </param>
    public void ButtonSetup(bool isArchive)
    {
        
        for (int i = 0; !isArchive && i<items.Count; i++)
        {
            GameObject thisButton = GameObject.Find("InventoryButton" + i);
            thisButton.GetComponent<Image>().sprite = items[i].InventorySprite;
        }
        for (int i = 0; isArchive && i<archives.Count; i++)
        {
            GameObject thisButton = GameObject.Find("ArchiveButton" + i);
            thisButton.GetComponent<Image>().sprite = archives[i].Item1;
        }
    }

    /// <summary>
    /// Reads descriptions of items in the inventory menu.
    /// </summary>
    /// <param name="buttonIndex"> The index of the button pressed. </param>
    public void InventoryReadDescription(int buttonIndex)
    {
        Text descriptionText = GameObject.Find("InventoryDescriptionText").GetComponent<Text>();
        if (items.ContainsKey(buttonIndex))
        {
            descriptionText.text = items[buttonIndex].InventoryDescription;
        } else {
            descriptionText.text = "No item in slot.";
        }
    }

    /// <summary>
    /// Reads the descriptions of items in the archives menu.
    /// </summary>
    /// <param name="buttonIndex"> The index of the button pressed. </param>
    public void ArchiveReadDescription(int buttonIndex)
    {
        Text descriptionText = GameObject.Find("ArchiveDescriptionText").GetComponent<Text>();
        if (archives.ContainsKey(buttonIndex))
        {
            descriptionText.text = archives[buttonIndex].Item2;
        } else
        {
            descriptionText.text = "Empty archive slot.";
        }
    }

}
