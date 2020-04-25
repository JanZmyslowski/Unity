using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.collider.gameObject.tag == "Ground")
        {
            //m_animator.SetTrigger("Hurt");
            //m_body2d.velocity = new Vector2(-collision.contacts[0].relativeVelocity.x * 100, m_body2d.velocity.y);
            //m_animator.SetInteger("AnimState", 2);
            Debug.Log("miecz w ziemi");
            Destroy(this.gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.y < -15)
        {
            Destroy(this.gameObject);
        }
    }
    
}
