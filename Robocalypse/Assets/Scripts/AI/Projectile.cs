using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @author Addison Shuppy
/// The projectile shot by laser turrets.
/// </summary>
public class Projectile : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private float speed, damage;

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
                target.GetComponent<EnemyBot>().TakeDamage(damage);
                Destroy(this.gameObject);
            }
        } else if(target == null)
        {
            Destroy(this.gameObject);
        }
        
    }

    /// <summary>
    /// Sets the destination for this projectile.
    /// </summary>
    /// <param name="_target"></param>
    public void SetTarget(GameObject _target)
    {
        target = _target;
    }

    /// <summary>
    /// Sets the damage a projectile will do on collision.
    /// </summary>
    /// <param name="_damage"> the float amount of damage to do. </param>
    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
}
