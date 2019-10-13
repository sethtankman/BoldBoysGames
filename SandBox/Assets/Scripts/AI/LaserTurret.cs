using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @author Addison Shuppy
/// </summary>
public class LaserTurret : Turret
{
    [SerializeField] private GameObject laser;
    [SerializeField] private Animator _animator;

    private GameObject activeTarget;
    private List<GameObject> queue;

    //private int cooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        _animator.SetBool("Firing", false);
        firing = false;
        queue = new List<GameObject>();

    }

    // Update is called once per frame
    public override void FireShot()
    {
        if(activeTarget != null)
        {

            AudioSource.PlayClipAtPoint(ShotSound, transform.position);
            Projectile shot = Instantiate(laser, new Vector3(transform.position.x, 
                transform.position.y + 0.17f, 0), Quaternion.identity).gameObject.GetComponent<Projectile>();
            shot.SetTarget(activeTarget);
        }
        //else if(cooldown > 0)
        //{
        //    cooldown--;
        //}
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
}
