using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @author Addison Shuppy
/// </summary>
public class CameraManager : MonoBehaviour
{

    [SerializeField] private float xMax, yMax, xMin, yMin;
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
        else
        {
           

        }
    }

    public void PlayTrack(int intensity)
    {
        int lvl = _GameData.level;
        if(intensity == 0)
        {
            if(lvl == 0)
            {
                _BackgroundMusic.clip = _Tracks[0];
                _BackgroundMusic.Play();
            }
        } else if (intensity == 1)
        {
            if(lvl == 0)
            {
                _BackgroundMusic.clip = _Tracks[1];
                _BackgroundMusic.Play();
            }
        } else if (intensity == 2)
        {
            if (lvl == 0)
            {
                _BackgroundMusic.clip = _Tracks[2];
                _BackgroundMusic.Play();
            }
        }
    }


    public void EndSequence()
    {
        following = false;
        //_BackgroundMusic.clip = _Tracks[1];
        _BackgroundMusic.Play();
    }
}
