using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @author Addison Shuppy
/// Health bars for enemies.
/// </summary>
public class HealthBar : MonoBehaviour
{
    [SerializeField] private float healthBarHeight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = this.transform.parent.position + new Vector3(0, healthBarHeight, 0);
    }
}
