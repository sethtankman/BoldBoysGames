using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBot : MonoBehaviour
{
    [SerializeField]
    private int direction;
    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == 0)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else if (direction == 1)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (direction == 2)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        } else if (direction == 3)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Happened");
        if (collision.gameObject.tag == "TurnSignal")
        {
            Debug.Log("Sensed TurnSignal");
            TurnSignal sig = collision.gameObject.GetComponent<TurnSignal>();
            if (sig != null)
            {
                direction = sig.GetDirection();
                Debug.Log("Changed Direction");
            }
        }
    }
}
