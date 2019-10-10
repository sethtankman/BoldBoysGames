using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour
{
    [SerializeField] public GameObject symbolizedObject;
    private Vector3 position;
    private int spacing;

    // Start is called before the first frame update
    void Start()
    {
        spacing = 11;
        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        spacing++;
        if(spacing > 10)
        {
            UpdatePosition();
            spacing = 0;
        }
        
    }

    void UpdatePosition()
    {
        if (symbolizedObject != null)
        {
            position = symbolizedObject.transform.position;
            position.x += 26.7f;
            position.y += 11.7f;
            position.x = 4.72f * position.x;
            position.y = 5.2f * position.y;
            transform.position = position;
        } else
        {
            symbolizedObject = gameObject.transform.parent.gameObject;
        }
    }
}
