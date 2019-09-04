using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @author Addison Shuppy
/// </summary>
public class Projectile : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        //initialize target
        //target = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        //move towards target
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            //if it hits, destroy this game object and do damage
            if (transform.position == target.transform.position)
            {
                target.GetComponent<EnemyBot>().TakeDamage(10);
                Destroy(this.gameObject);
            }
        } else if(target == null)
        {
            Destroy(this.gameObject);
        }
        
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
    }
}
