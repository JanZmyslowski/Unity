using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour {
    [RangeAttribute(0.2f, 2f)]
    [SerializeField] float      m_speed = 1.0f;
    [RangeAttribute(0f, 3.2f)]
    [SerializeField] float      m_jumpForce = 2.5f;
    [SerializeField] GameController controller;
    [SerializeField]
    private GameObject sword;
    

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Bandit       m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_isDead = false;
    private bool movingEnable = true;
    private bool isWatchingLeft = true;
    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        sword = GameObject.FindGameObjectWithTag("Sword");
        sword.gameObject.SetActive(false);
        //sword.transform.position = new Vector3(sword.transform.position.x, transform.position.y, 200); 
    }
	
    public void MakeDead()
    {
        m_isDead = true;
        m_animator.SetTrigger("Death");
        m_animator.speed = 0;

    }
    public void MakeLife()
    {
        m_animator.SetTrigger("Recover");
        m_isDead = false;

    }
    public void DisableMoveing()
    {
        movingEnable = false;
        m_animator.speed = 0;
        m_body2d.velocity = new Vector2(0, 0);

    }
    private void knockBack()
    {
        StartCoroutine("Hurt");
        Vector2 vector = new Vector2(m_speed * .6f, .6f);
        m_body2d.velocity = vector;
        controller.SendMessage("Hurt");
        
    }
    public IEnumerator Hurt()
    {
        movingEnable = false;
        m_animator.SetTrigger("Hurt");
        yield return new WaitForSeconds(.7f);
        movingEnable = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Diamond")
        {
            Destroy(other.gameObject);
        }
        //if (other.gameObject.tag == "Enemy")
        //{

        //    Debug.Log("hit!");

        //}

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Enemy" && movingEnable)
        {
            //m_animator.SetTrigger("Hurt");
            //m_body2d.velocity = new Vector2(-collision.contacts[0].relativeVelocity.x * 100, m_body2d.velocity.y);
            //m_animator.SetInteger("AnimState", 2);
            knockBack();

        }
    }

    // Update is called once per frame
    void Update () {
        //Check if character just landed on the ground
        if (!m_isDead && movingEnable)
        {
            if (!m_grounded && m_groundSensor.State())
            {
                m_grounded = true;
                m_animator.SetBool("Grounded", m_grounded);
            }

            //Check if character just started falling
            if (m_grounded && !m_groundSensor.State())
            {
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
            }

            // -- Handle input and movement --
            float inputX = Input.GetAxis("Horizontal");

            // Swap direction of sprite depending on walk direction
            if (inputX > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                isWatchingLeft = false;

            }
            else if (inputX < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                isWatchingLeft = true;
            }
            // Move
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

            //Set AirSpeed in animator
            m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

            // -- Handle Animations -- 
            //Jump
            if (Input.GetKeyDown("space") && m_grounded)
            {
                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
                //SpawnBullet(sword);

            }
            //Fight
            else if (Input.GetKeyDown("q"))
            {
                SpawnBullet();
            }
            //Run
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
                m_animator.SetInteger("AnimState", 2);
            //Idle
            else
                m_animator.SetInteger("AnimState", 0);
        }    
        

    }

    void SpawnBullet()
    {
        GameObject bullet;
        sword.SetActive(true);
        bullet = Instantiate(sword);

        sword.SetActive(false);
        bullet.transform.position = new Vector3(isWatchingLeft ? m_body2d.position.x - 0.3f: m_body2d.position.x + 0.3f , m_body2d.position.y+0.3f);
        bullet.transform.eulerAngles = new Vector3(0, 0, isWatchingLeft ? -180 : 0);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(isWatchingLeft ? -1 : 1,0.6f);
\    }
}
