﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPTurret : Turret
{
    private List<GameObject> queue;
    [SerializeField] private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        queue = new List<GameObject>();
        firing = false;
    }

    public new void FireShot()
    {
        if (firing)
        {
            AudioSource.PlayClipAtPoint(ShotSound, transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy" && firing == false)
        {
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
            if (queue.Count == 0)
            {
                firing = false;
                _animator.SetBool("Firing", false);
                return;
            }

        }
        else if (collision.transform.tag == "Enemy" && queue.Count == 0)
        {
            _animator.SetBool("Firing", false);
            firing = false;
        }
    }
}