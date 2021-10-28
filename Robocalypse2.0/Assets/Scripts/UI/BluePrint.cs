using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /// <summary>
    /// @author Addison Shuppy
    /// The item collected that is a blueprint of a turret to be built.
    /// </summary>
    public class BluePrint : Item
    {
        [SerializeField] private int BluePrintID;
        [SerializeField] private GameObject turretButton, data;

        public string TBName;

        /// <summary>
        /// Plays the collect sound, sets the correct turret button to active, 
        /// and increments the number of turrets collected in the game.
        /// </summary>
        public override void StartingFunction()
        {
            TBName = turretButton.name;
            AudioSource.PlayClipAtPoint(collectSound, Camera.main.transform.position);
            if (turretButton != null)
            {
                turretButton.SetActive(true);
                data.GetComponent<GameData>().incrementTurretCount();
            } else {
                Debug.Log("Turret Button not found.");
            }
        }

        /// <summary>
        /// Sets the blueprint's ID.
        /// </summary>
        /// <param name="ID"> The blueprint's ID. </param>
        public void SetID(int ID)
        {
            BluePrintID = ID;
        } 

        /// <summary>
        /// Gets the blueprint's ID. 
        /// </summary>
        /// <returns> The blueprint's ID. </returns>
        public int getID()
        {
            return BluePrintID;
        }
    }
}
