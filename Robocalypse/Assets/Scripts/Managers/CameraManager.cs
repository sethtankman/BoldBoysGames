using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @author Addison Shuppy
/// Controls the camera, and also the background music.
/// </summary>
public class CameraManager : MonoBehaviour
{

    [SerializeField] public float xMax, yMax, xMin, yMin;
    [SerializeField] private AudioClip[] _Tracks;
    [SerializeField] GameData _GameData;

    private bool following = true;
    private Transform target;
    private Camera _camera;
    private AudioSource _BackgroundMusic;
    
    // Start is called before the first frame update
    void Start()
    {
        _GameData = GameObject.Find("GameData").GetComponent<GameData>();
        target = GameObject.Find("MainGuy").transform;
        _BackgroundMusic = GetComponent<AudioSource>();
        _camera = GetComponent<Camera>();
        _BackgroundMusic.clip = _Tracks[0];
        _BackgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (following)
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), 
                Mathf.Clamp(target.position.y, yMin, yMax), -10);
        }
    }


    /// <summary>
    /// Plays the track requested from the list made in the Unity Editor
    /// </summary>
    /// <param name="trackNum"> the index of teh track in the array of tracks for that level. </param>
    public void PlayTrack(int trackNum)
    {
        _BackgroundMusic.clip = _Tracks[trackNum];
        _BackgroundMusic.Play();
    }
}
