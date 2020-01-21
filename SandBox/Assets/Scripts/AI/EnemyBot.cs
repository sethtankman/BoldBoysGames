using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// @author Addison Shuppy
/// An enemy robot.
/// </summary>
public class EnemyBot : MonoBehaviour
{
    /// <summary>
    /// A reference to the player and the Spawn Manager
    /// </summary>
    [SerializeField] private GameObject Player, SpawnManager;

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

    /// <summary>
    /// The indicator of the enemy bot on the minimap.
    /// </summary>
    [SerializeField] private RawImage indicator, indic;

    /// <summary>
    /// the change in position of the enemy bot while moving.
    /// </summary>
    private Vector3 change;

    /// <summary>
    /// The animator for the robot.
    /// </summary>
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        SpawnManager = GameObject.Find("SpawnManager");
        UIManager = GameObject.Find("UIManager");
        Player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
        maxHealth = health;
        indic = Instantiate(indicator, transform.position - 800 * Vector3.left, Quaternion.identity);
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
            DecrementEnemyCount();
            Destroy(indic);
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
        float prevScale = hBar.localScale.x;
        hBar.localScale = new Vector3(newHealth, 1f);
        //this is where the green health bar is shifted left as it scales down, making it fit with the red background
        hBar.localPosition -= new Vector3((prevScale - newHealth)/2.5f, 0, 0);
        if(health <= 0)
        {
            Player.GetComponent<MainGuy>().Transaction(goldValue);
            DecrementEnemyCount();
            Destroy(indic);
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Decrements the number of enemies known by the spawn manager, 
    /// and allows the player to start the next wave.
    /// </summary>
    public void DecrementEnemyCount()
    {
        if (SpawnManager.GetComponent<SpawnManager0>())
        {
            int foesRemaining = --SpawnManager.GetComponent<SpawnManager0>().numEnemies;
            if (foesRemaining == 0)
                UIManager.GetComponent<UIManager>().EnableStartButton();
        } else if (SpawnManager.GetComponent<SpawnManager1>())
        {
            int foesRemaining = --SpawnManager.GetComponent<SpawnManager1>().numEnemies;
            if (foesRemaining == 0)
                UIManager.GetComponent<UIManager>().EnableStartButton();
        }
    }

    /// <summary>
    /// Starts the stun couroutine.
    /// </summary>
    /// <param name="seconds"> the number of seconds to stun the enemy. </param>
    public void Stun(float seconds) { 
        StartCoroutine(StunCoroutine(seconds));
    }

    /// <summary>
    /// Stuns this enemy in place for a given amount of time.
    /// </summary>
    /// <param name="seconds"> the number of seconds to stun this enemy. </param>
    /// <returns></returns>
    public IEnumerator StunCoroutine(float seconds)
    {
        float prevSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(seconds);
        speed = prevSpeed;
    }
}
