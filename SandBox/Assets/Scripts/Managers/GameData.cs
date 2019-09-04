using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    public int level;
    public Dictionary<int, Item> items = new Dictionary<int, Item>();
    public Dictionary<int, Tuple<Sprite, string>> archives = new Dictionary<int, Tuple<Sprite, string>>();
    
    public void AddItem(Item item)
    {
        if(items == null)
        {
            items = new Dictionary<int, Item>();
        }
        items.Add(LowestEmptySlot(false), item);
        item.StartingFunction();
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

    /// <summary>
    /// Needs to actually be implemented.
    /// </summary>
    /// <param name="path"></param>
    public void SaveGame(string path)
    {
        StringBuilder saveString = new StringBuilder();
        saveString.Append("level: " + level);
        
        Debug.Log(saveString.ToString());
        StreamWriter sw = File.CreateText(path);
        sw.Close();

        File.WriteAllText(path, saveString.ToString());
    }
}
