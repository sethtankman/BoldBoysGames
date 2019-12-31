using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A turret that freezes enemies for a short duration
/// </summary>
public class EMPTurret : Turret
{
    /// <summary>
    /// The queue of enemies in range
    /// </summary>
    private List<GameObject> queue;
    /// <summary>
    /// the animator of the turret
    /// </summary>
    [SerializeField] private Animator _animator;
    /// <summary>
    /// the cooldown and the level of the turret
    /// </summary>
    private int cooldown = 0, lvl = 0;

    // Start is called before the first frame update
    void Start()
    {
        queue = new List<GameObject>();
        firing = false;
    }

    // Called every frame.  Counts down on the cooldown.
    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown--;
        }
    }

    /// <summary>
    /// Called when a target is in range and the animator reaches a certain frame.
    /// Plays audio and freezes enemies in place for 3 seconds with the Stun method.
    /// </summary>
    public override void FireShot()
    {
        if (firing && cooldown < 1)
        {
            AudioSource.PlayClipAtPoint(ShotSound, transform.position);
            foreach (GameObject bot in queue)
            {
                EnemyBot enemy = bot.GetComponent<EnemyBot>();
                enemy.Stun(3);
            }
            firing = false;
            _animator.SetBool("Firing", false);
            cooldown = 300;
        }
    }

    /// <summary>
    /// Called when a trigger enters. Begins firing sequence.
    /// </summary>
    /// <param name="collision">
    /// The collidere causeing the trigger event.
    /// </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            queue.Add(collision.gameObject);
            if (cooldown < 1 && firing == false)
            {
                firing = true;
                _animator.SetBool("Firing", true);
            }
        }
    }

    /// <summary>
    /// Handles the queue when an enemy leaves the trigger area.
    /// </summary>
    /// <param name="collision"> 
    /// The collider causing the trigger event.
    /// </param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy" && queue.Count != 0)
        {
            queue.Remove(collision.gameObject);
            if (queue.Count == 0)
            {
                firing = false;
                _animator.SetBool("Firing", false);
            }

        }
        else if (collision.transform.tag == "Enemy" && queue.Count == 0)
        {
            _animator.SetBool("Firing", false);
            firing = false;
        }
    }


    public override int getPrice()
    {
        return 40;
    }

 
    public override int getUpgradePrice(int _lvl)
    {
        return 40;
    }

   
    public override void setLevel(int _lvl)
    {
        lvl = _lvl;
    }
    
    public override int getSellPrice()
    {
        int sellPrice = 0;
        for (int i = 0; i < lvl; i++)
        {
            sellPrice += getUpgradePrice(i);
        }
        return (int)(sellPrice * 0.8);
    }
}
