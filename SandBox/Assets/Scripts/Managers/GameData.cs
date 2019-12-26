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
    private UIManager _uIManager;
    public static int level;
    public static int turretsFound;
    [SerializeField] private GameObject[] turretButtons = new GameObject[2];
    public static Dictionary<int, Item> items = new Dictionary<int, Item>();
    public static Dictionary<int, Tuple<Sprite, string>> archives = new Dictionary<int, Tuple<Sprite, string>>();
    
    public void AddItem(Item item)
    {
        item.StartingFunction();
        items.Add(LowestEmptySlot(false), item);
    }

    public void incrementTurretCount()
    {
        turretsFound++;
    }

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
        Debug.Log(turretsFound);
        for(int j =0; j < turretsFound; j++)
        {
            turretButtons[j].SetActive(true);
        }
    }

    public void OnLevelWasLoaded(int level)
    {
        _uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        if(_uIManager == null)
        {
            Debug.Log("UIManager not found");
        }
        LoadItemsAndTurrets();

    }

    public void AddToArchive(Sprite _sprite, string _string)
    {
        if(archives == null)
        {
            archives = new Dictionary<int, Tuple<Sprite, string>>();
        }
        Tuple<Sprite, string> archiveEntry = new Tuple<Sprite, string>(_sprite, _string);
        archives.Add(LowestEmptySlot(true), archiveEntry);
    }

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
