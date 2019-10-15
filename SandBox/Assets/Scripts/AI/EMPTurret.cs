using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPTurret : Turret
{
    private List<GameObject> queue;
    [SerializeField] private Animator _animator;
    private int cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        queue = new List<GameObject>();
        firing = false;
    }

    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown--;
        }
    }

    public override void FireShot()
    {
        if (firing && cooldown < 1)
        {
            AudioSource.PlayClipAtPoint(ShotSound, transform.position);
            foreach (GameObject bot in queue)
            {
                EnemyBot enemy = bot.GetComponent<EnemyBot>();
                enemy.Stun(3);
                cooldown = 300;
            }
        }
    }

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

    public override int getPrice()
    {
        return 40;
    }
}
