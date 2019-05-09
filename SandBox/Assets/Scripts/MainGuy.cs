using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGuy : MonoBehaviour
{
    [SerializeField]
    private GameObject _turretOne;

    [SerializeField]
    private float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlaceTurret1();
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

        //sets vertical bounds for player        
        if (transform.position.y > 3.5f)
        {
            transform.position = new Vector3(transform.position.x, 3.5f, 0);
        } else if (transform.position.y < -3.5f)
        {
            transform.position = new Vector3(transform.position.x, -3.5f, 0);
        }

        //sets the horizontal bounds for player
        if (transform.position.x > 9.0f)
        {
            transform.position = new Vector3(9.0f, transform.position.y, 0);
        } else if (transform.position.x < -9.4f)
        {
            transform.position = new Vector3(-9.4f, transform.position.y, 0);
        }
    }

    private void PlaceTurret1()
    {
        Instantiate(_turretOne, transform.position + new Vector3(-0.63f, 0, 0), Quaternion.identity);
    }
}
