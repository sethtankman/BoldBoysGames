using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGuy : MonoBehaviour
{
    [SerializeField]
    public int gBytes;
    public GameObject _selectedTurret;
    [SerializeField]
    private GameObject[] _allTurrets;
    [SerializeField]
    private float speed = 2.0f;
    public bool trackMode = false;
    public Vector3 trackLocation;
    [SerializeField]
    private UIManager _uiManager;
    private GameData _data;
    public Animator _animator;
    private bool isMoving;
    private Vector3 change;
    private Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        myRigidBody = GetComponent<Rigidbody2D>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _data = GameObject.Find("GameData").GetComponent<GameData>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
        SpriteSet();

    }

    private void UpdateAnimationAndMove()
    {
        if (trackMode)
        {
            MoveToTurret(trackLocation);
        } else if (change != Vector3.zero)
        {
            Movement();
            _animator.SetFloat("moveX", change.x);
            _animator.SetFloat("moveY", change.y);
            _animator.SetBool("moving", true);
        } else {
            _animator.SetBool("moving", false);
        }
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
            myRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);

    }

    /// <summary>
    /// Automatically moves the player to the location of the turret they are about to build 
    /// and builds the turret. 
    /// </summary>
    /// <param name="turretLocation"> The location of the turret to build. </param>
    public void MoveToTurret(Vector3 turretLocation)
    {
        float distance = Mathf.Sqrt(Mathf.Pow((turretLocation.x - transform.position.x), 2)
            + Mathf.Pow((turretLocation.y - transform.position.y), 2));
        Vector3 unitvector = new Vector3((turretLocation.x - transform.position.x) / distance,
                (turretLocation.y - transform.position.y) / distance, 0);
        if (distance > 1.0f)
        {

            transform.Translate(unitvector * speed * Time.deltaTime);
            distance = distance - Time.deltaTime;
        }
        else
        {
            trackMode = false;
            Instantiate(_selectedTurret, new Vector3(turretLocation.x, turretLocation.y, 0), Quaternion.identity);
        }
    }

    /// <summary>
    /// Handles all transactions involving player purchasing.
    /// </summary>
    /// <param name="amount"> The amount of GigaBytes to be added or subtracted</param>
    public void Transaction(int amount)
    {
        gBytes += amount;
        _uiManager.modifyGBytes(gBytes.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            _data.AddItem(collision.gameObject.GetComponent<Item>());
            _data.AddToArchive(collision.gameObject.GetComponent<Item>().ArchiveSprite, collision.gameObject.GetComponent<Item>().ArchiveDescription);
            Destroy(collision.gameObject);

        }
    }

    public void SetTurret(GameObject gameObject)
    {
        _selectedTurret = gameObject;
    }
}
