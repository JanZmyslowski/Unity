using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPlatform : MonoBehaviour
{
    [SerializeField]
    private Vector3 velocity;
    private bool moving;
    [SerializeField]
    private Vector3 bounds_left;
    [SerializeField]
    private Vector3 bounds_right;
    private bool directionLeft;
    private bool directionUp;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moving = true;
            collision.collider.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.parent = null;
        }
    }
    private void FixedUpdate()
    {
        if(moving)
        {
            transform.position += (velocity * Time.deltaTime);
        }
        if (transform.position.x > bounds_right.x && !directionLeft)
        {
            velocity.x *= -1;
            directionLeft = true;
        }
        if (transform.position.x < bounds_left.x && directionLeft)
        {
            velocity.x *= -1;
            directionLeft = false;

        }
        if (transform.position.y > bounds_left.y && !directionUp)
        {
            velocity.y *= -1;
            directionUp = true;

        }
        if (transform.position.y < bounds_right.y && directionUp)
        {
            velocity.y *= -1;
            directionUp = false;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (velocity.x < 0)
        {
            directionLeft = true;
        } else
        {
            directionLeft = false;
        }
        if (velocity.y < 0)
        {
            directionUp = false;
        }
        else
        {
            directionLeft = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
