using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float m_speed = 1.0f;
    [SerializeField] private Vector3 bound_right;
    [SerializeField] private Vector3 bound_left;
    private bool enable = true; 
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private bool goingLeft = false;

  
    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();

    }



    IEnumerator Flip()
    {
        if (m_speed > 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else if (m_speed < 0)
            GetComponent<SpriteRenderer>().flipX = false;
        yield return new WaitForSeconds(0.5f); 
    }
    // Update is called once per frame
    void Update()
    {
        if (enable)
        {
            // Swap direction of sprite depending on walk direction
            StartCoroutine(Flip());

            // Move
            m_body2d.velocity = new Vector2(m_speed * 2, m_body2d.velocity.y);
            //if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);
            if (transform.position.x > bound_right.x && goingLeft)
            {
                m_speed *= -1;
                goingLeft = false;
            }
            if (transform.position.x < bound_left.x && !goingLeft)
            {
                m_speed *= -1;
                goingLeft = true;
            }
        }     
        

    }
}
