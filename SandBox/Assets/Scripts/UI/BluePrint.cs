using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class BluePrint : Item
    {
        [SerializeField] private int BluePrintID;
        [SerializeField] private GameObject turretButton, data;
        public string TBName;
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

        public void SetID(int ID)
        {
            BluePrintID = ID;
        } 

        public int getID()
        {
            return BluePrintID;
        }
    }
}
