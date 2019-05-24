using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGuy : MonoBehaviour
{
    [SerializeField]
    private GameObject _selectedTurret;
    [SerializeField]
    private GameObject[] _allTurrets;
    [SerializeField]
    private float speed = 2.0f;
    public bool trackMode = false;
    public Vector3 trackLocation;
    [SerializeField]
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SpriteSet();
        
    }

    /// <summary>
    /// Communicates to the UIManager and sets the selected turret to the UI.
    /// </summary>
    private void SpriteSet()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _uiManager.SetSprite(0);
            _selectedTurret = _allTurrets[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _uiManager.SetSprite(1);
            _selectedTurret = _allTurrets[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _uiManager.SetSprite(2);
            _selectedTurret = _allTurrets[2];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _uiManager.SetSprite(3);
            _selectedTurret = _allTurrets[3];
        }
    }

    void Update(Vector3 turretLocation)
    {
        transform.position = Vector3.MoveTowards(transform.position, turretLocation, Time.deltaTime * speed);
    }

    private void Movement()
    {
        if (trackMode)
        {
            MoveToTurret(trackLocation);
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
        }
        //sets vertical bounds for player        
        if (transform.position.y > 3.5f)
        {
            transform.position = new Vector3(transform.position.x, 3.5f, 0);
        } else if (transform.position.y < -3.5f)
        {
            transform.position = new Vector3(transform.position.x, -3.5f, 0);
        }

        //sets the horizontal bounds for player
        if (transform.position.x > 9.0f)
        {
            transform.position = new Vector3(9.0f, transform.position.y, 0);
        } else if (transform.position.x < -9.4f)
        {
            transform.position = new Vector3(-9.4f, transform.position.y, 0);
        }
    }

    public void MoveToTurret(Vector3 turretLocation) 
    {
        float distance = Mathf.Sqrt(Mathf.Pow((turretLocation.x - transform.position.x), 2) 
            + Mathf.Pow((turretLocation.y - transform.position.y), 2));
        Vector3 unitvector = new Vector3((turretLocation.x - transform.position.x) / distance,
                (turretLocation.y - transform.position.y) / distance, 0);
        if (distance >1.0f )
        {
            
            transform.Translate(unitvector * speed * Time.deltaTime);
            distance = distance - Time.deltaTime;
            Debug.Log("Distance is: " + distance);
        }
        else
        {
            trackMode = false;
            Instantiate(_selectedTurret, turretLocation, Quaternion.identity);
        }
    }
}
