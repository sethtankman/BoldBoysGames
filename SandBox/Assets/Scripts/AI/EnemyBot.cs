using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// @author Addison Shuppy
/// </summary>
public class EnemyBot : MonoBehaviour
{
    /// <summary>
    /// A reference to the player
    /// </summary>
    [SerializeField] private GameObject Player;

    /// <summary>
    /// the number of enemies in the entire game
    /// </summary>
    private static int numEnemies;

    /// <summary>
    /// the list of waypoints along the path the enemy is to follow
    /// </summary>
    [SerializeField] private GameObject[] _waypoints;

    /// <summary>
    /// the index of the waypoint
    /// </summary>
    [SerializeField] private int waypointIndex;

    /// <summary>
    /// The speed of this enemy
    /// </summary>
    [SerializeField] private float speed;

    /// <summary>
    /// the current health of the enemy
    /// </summary>
    [SerializeField] private float health;

    /// <summary>
    /// the max health of the enemy
    /// </summary>
    private float maxHealth;

    /// <summary>
    /// A reference to the UIManager
    /// </summary>
    private GameObject UIManager;

    /// <summary>
    /// the damage an enemy deals when it gets to the end.
    /// </summary>
    [SerializeField] private int damage;

    /// <summary>
    /// The value of this bot dead in gold.
    /// </summary>
    [SerializeField] private int goldValue;

    [SerializeField] private RawImage indicator;
    private Vector3 change;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        numEnemies++;
        int place = 0;
        UIManager = GameObject.Find("UIManager");
        GameObject signal = GameObject.Find("TurnSignal");
        while (true)
        {
            if (signal == null)
            {
                break;
            }
            _waypoints[place] = signal;
            place++;
            signal = GameObject.Find("TurnSignal (" + place + ")");
        }

        Player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
        waypointIndex = 0;
        transform.position = _waypoints[0].transform.position;
        maxHealth = health;
        RawImage indic = Instantiate(indicator, transform.position, Quaternion.identity);
        indic.transform.SetParent(GameObject.Find("MiniMap").transform, false);
        indic.gameObject.GetComponent<MinimapIcon>().symbolizedObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypointIndex <= _waypoints.Length-1)
        {
            var move = Vector3.MoveTowards(transform.position, _waypoints[waypointIndex].transform.position,
                speed * Time.deltaTime);
            change = transform.position - move;
            //animate depending on direction
            if (_animator != null)
            {
                _animator.SetFloat("MoveX", -change.x);
                _animator.SetFloat("MoveY", -change.y);
            }

            transform.position = move;

            if (transform.position == _waypoints[waypointIndex].transform.position)
            {
                waypointIndex++;
            }

        } else if (waypointIndex == _waypoints.Length)
        {
            UIManager.GetComponent<UIManager>().loseLives(damage);
            if(--numEnemies == 0)
            {
                UIManager.GetComponent<UIManager>().EnableStartButton();
            }
            Destroy(this.gameObject);
        }

    }

    /// <summary>
    /// used to designate the path for a bot to spawn to and follow
    /// </summary>
    /// <param name="waypoints"></param>
    public void SetPath(GameObject[] waypoints)
    {
        _waypoints = waypoints;
        transform.position = new Vector3(_waypoints[0].transform.position.x, _waypoints[0].transform.position.y, 0);
    }

    /// <summary>
    /// subtracts damage from health, updates health bar, destroys gameobject if dead.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        health -= damage;
        Transform hBar = this.transform.GetChild(0).transform.Find("Bar");
        float newHealth = health / maxHealth;
        hBar.localScale = new Vector3(newHealth, 1f);
        if(health <= 0)
        {
            Player.GetComponent<MainGuy>().Transaction(goldValue);
            Destroy(this.gameObject);
            Debug.Log(indicator.name);
            Destroy(GameObject.Find(indicator.name + "(Clone)"));
            if(--numEnemies == 0)
            {
                UIManager.GetComponent<UIManager>().EnableStartButton();
            }
        }
    }
    
}
