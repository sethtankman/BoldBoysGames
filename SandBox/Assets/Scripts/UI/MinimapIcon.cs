using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour
{
    [SerializeField] public GameObject symbolizedObject, mainCamera;
    private Vector3 position;
    private int spacing;

    // Start is called before the first frame update
    void Start()
    {
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

    void UpdatePosition()
    {
        if (symbolizedObject != null)
        {
            float mapSizeX = transform.parent.GetComponent<RectTransform>().rect.width;
            float mapSizeY = transform.parent.GetComponent<RectTransform>().rect.height;
            float MainGuyPositionX = symbolizedObject.transform.position.x - (1.6f * mainCamera.GetComponent<CameraManager>().xMin);// - 1.6f tutorial //- (1.42f * mainCamera.GetComponent<CameraManager>().xMin); //35.5f lv1;
            float MainGuyPositionY = symbolizedObject.transform.position.y - (1.8f * mainCamera.GetComponent<CameraManager>().yMin);// - 1.8f tutorial //- (1.54f * mainCamera.GetComponent<CameraManager>().yMin); //17.5f lv1;
            float CameraRangeX = mainCamera.GetComponent<CameraManager>().xMax - mainCamera.GetComponent<CameraManager>().xMin;
            float CameraRangeY = mainCamera.GetComponent<CameraManager>().yMax - mainCamera.GetComponent<CameraManager>().yMin;
            position.x = mapSizeX 
                * MainGuyPositionX 
                / (CameraRangeX); // + 0 lv1, tutorial same
            position.y = mapSizeY
                * MainGuyPositionY
                / (CameraRangeY +1); // -3 lv1, +1 tutorial
            //float horizConstant = mainCamera.GetComponent<CameraManager>().x
            //need to find some value that changes when the screen is resized.
            //position.x += 26.5f;
            //position.y += 12;
            //position.x = 5.7f * position.x;
            //position.y = 6.2f * position.y;
            transform.position = position;
        } else
        {
            symbolizedObject = gameObject.transform.parent.gameObject;
        }
    }
}
