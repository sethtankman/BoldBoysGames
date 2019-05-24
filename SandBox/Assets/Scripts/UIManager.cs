using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // This will contain however many turrets we make.
    [SerializeField]
    private Sprite[] allTurrets = new Sprite[4];
    [SerializeField]
    private Image selectedTurret;

    // Start is called before the first frame update
    void Start()
    {
        selectedTurret.sprite = allTurrets[0];
    }
    
    public void SetSprite(int num)
    {
        selectedTurret.sprite = allTurrets[num];
    }
}
