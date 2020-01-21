using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @author Addison Shuppy
/// The icon representing players and enemies on the minimap.
/// </summary>
public class MinimapIcon : MonoBehaviour
{
    [SerializeField] public GameObject symbolizedObject, mainCamera, BLCorner;
    private Vector3 position;
    private int spacing;

    // Start is called before the first frame update
    void Start()
    {
        BLCorner = GameObject.Find("BLCorner");
        mainCamera = GameObject.Find("MainCamera");
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

    /// <summary>
    /// Changes the player's position on the minimap.
    /// </summary>
    void UpdatePosition()
    {
        if (symbolizedObject != null)
        {
            float lv1ScaleFactor = BLCorner.transform.position.z;
            float mapSizeX = transform.parent.GetComponent<RectTransform>().rect.width;
            float mapSizeY = transform.parent.GetComponent<RectTransform>().rect.height;
            float MainGuyPositionX = symbolizedObject.transform.position.x - BLCorner.transform.position.x;
            float MainGuyPositionY = symbolizedObject.transform.position.y - BLCorner.transform.position.y;
            float CameraRangeX = mainCamera.GetComponent<CameraManager>().xMax - mainCamera.GetComponent<CameraManager>().xMin;
            float CameraRangeY = mainCamera.GetComponent<CameraManager>().yMax - mainCamera.GetComponent<CameraManager>().yMin;
            position.x = 0.000828f * Screen.width * mapSizeX 
                * MainGuyPositionX 
                / (CameraRangeX); 
            position.y =  lv1ScaleFactor * 0.00131f * Screen.height * mapSizeY
                * MainGuyPositionY
                / (CameraRangeY); 
            transform.position = position;
        } else
        {
            Destroy(this.gameObject);
        }
    }
}
