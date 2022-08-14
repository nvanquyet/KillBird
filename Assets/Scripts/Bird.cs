using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public GameObject deathVfx;
    public float xSpeed;
    public float yMin;
    public float yMax;
    

    Rigidbody2D m_rb;
    bool m_moveLeft;
    //bool m_isDead;
    void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        xSpeed = 2f;
        yMin = -2.5f;
        yMax = 2.5f;
        movingDir();
        //m_isDead = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        m_rb.velocity = m_moveLeft ? new Vector2(-xSpeed, Random.Range(yMin, yMax)) : 
        new Vector2(xSpeed, Random.Range(yMin, yMax));
    }

    public void movingDir()
    {
        m_moveLeft = transform.position.x > 0 ? true : false; 
        Flip();
    }

    public void Flip()
    {
        if(!m_moveLeft)
        {
            return;
        }
        transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void Die()
    {
        //m_isDead = true;
        Destroy(gameObject);
        if(deathVfx)
        {
            Instantiate(deathVfx, transform.position, Quaternion.identity);
        }
    }
}
