using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// @author Addison Shuppy
/// The main character that is able to move and build turrets.
/// </summary>
public class MainGuy : MonoBehaviour
{
    [SerializeField] public int gBytes;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject[] _allTurrets;
    [SerializeField] private float speed = 2.0f;

    public GameObject _selectedTurret;
    public Vector3 trackLocation;
    public Animator _animator;
    public BuildableSquare selectedSquare;
    public bool trackMode = false;
    
    private GameData _data;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private bool isMoving;

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

    /// <summary>
    /// Controls all animations involving the player and which direction he is facing.
    /// </summary>
    private void UpdateAnimationAndMove()
    {
        if (trackMode)
        {
            MoveToTurret(trackLocation);
            _animator.SetBool("moving", true);
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
    /// Communicates to the UIManager and sets the selected turret to the UI using numbers.
    /// </summary>
    private void SpriteSet()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (GameObject.Find("Turret1Button"))
            {
                _uiManager.SetTurret(0);
                _uiManager.UISound(1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (GameObject.Find("Turret2Button"))
            {
                _uiManager.SetTurret(1);
                _uiManager.UISound(1);
            }
        }
    }

    /// <summary>
    /// Controls player movement
    /// </summary>
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

            _animator.SetFloat("moveX", unitvector.x);
            _animator.SetFloat("moveY", unitvector.y);
            transform.Translate(unitvector * speed * Time.deltaTime);
            distance = distance - Time.deltaTime;
        }
        else
        {
            trackMode = false;
            this.selectedSquare.BuildTurret(_selectedTurret.GetComponent<Turret>());
        }
    }

    /// <summary>
    /// Handles all transactions involving player purchasing.
    /// </summary>
    /// <param name="amount"> The amount of GigaBytes to be added or subtracted. </param>
    public void Transaction(int amount)
    {
        gBytes += amount;
        _uiManager.modifyGBytesText(gBytes.ToString());
    }

    /// <summary>
    /// Collects items collided with
    /// </summary>
    /// <param name="collision"> the collider belonging to the trigger effect. </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            _data.AddItem(collision.gameObject.GetComponent<Item>());
            _data.AddToArchive(collision.gameObject.GetComponent<Item>().ArchiveSprite, collision.gameObject.GetComponent<Item>().ArchiveDescription);
            Destroy(collision.gameObject);
            _uiManager.EnableStartButton();
        }
    }

    /// <summary>
    /// Sets the turret that will be built on left-click
    /// </summary>
    /// <param name="gameObject"> the turret object </param>
    public void SetTurret(GameObject gameObject)
    {
        _selectedTurret = gameObject;
    }
}
