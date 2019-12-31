using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @author Addison Shuppy
/// </summary>
public class LaserTurret : Turret
{
    [SerializeField] private GameObject _laser;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject[] _lasers;

    private int lvl;
    private GameObject activeTarget;
    private List<GameObject> queue;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator.SetBool("Firing", false);
        firing = false;
        queue = new List<GameObject>();
    }

    /// <summary>
    /// Called when the animation reaches the firing frame. 
    /// Instantiates a laser that does damage depending on level of the turret.
    /// </summary>
    public override void FireShot()
    {
        if(activeTarget != null)
        {
            AudioSource.PlayClipAtPoint(ShotSound, transform.position);
            if (lvl == 1)
            {
                _laser.GetComponent<Projectile>().SetDamage(8);
            } else if (lvl == 2)
            {
                _laser.GetComponent<Projectile>().SetDamage(12);
            } else if (lvl == 3)
            {
                _laser.GetComponent<Projectile>().SetDamage(20);
            }
            
            Projectile shot = Instantiate(_laser, new Vector3(transform.position.x, 
                transform.position.y + 0.17f, 0), Quaternion.identity).gameObject.GetComponent<Projectile>();
            shot.SetTarget(activeTarget);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy" && firing == false)
        {
            activeTarget = collision.gameObject;
            firing = true;
            _animator.SetBool("Firing", true);
        }
        else if (collision.transform.tag == "Enemy" && firing == true)
        {
            queue.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy" && queue.Count != 0)
        {
            queue.Remove(collision.gameObject);
            if(queue.Count == 0)
            {
                activeTarget = null;
                firing = false;
                _animator.SetBool("Firing", false);
                return;
            }
            activeTarget = queue.ToArray()[0];
            
        }
        else if (collision.transform.tag == "Enemy" && queue.Count == 0)
        {
            _animator.SetBool("Firing", false);
            activeTarget = null;
            firing = false;
        }
    }
    
    public override int getPrice()
    {
        return 30;
    }

    public override int getUpgradePrice(int _lvl)
    {
        if (_lvl == 0)
            return 30;
        if (_lvl == 1)
            return 20;
        if (_lvl == 2)
            return 50;
        else { return 0; }
    }

    public override void setLevel(int _lvl)
    {
        if (_lvl == 3)
        {
            _laser = _lasers[1];
            _animator.SetInteger("Level", _lvl);
        }
        lvl = _lvl;
    }

    public override int getSellPrice()
    {
        int sellPrice = 0;
        for(int i = 0; i < lvl; i++)
        {
            sellPrice += getUpgradePrice(i);
        }
        return (int)(sellPrice * 0.8);
    }
}
