using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float fireRate;
    public GameObject viewFinded;

    float m_fireRate;
    bool m_isShooted;
    GameObject m_viewFindedClone;



    void Awake(){
        fireRate = 1.5f;
        m_fireRate = fireRate;
        m_viewFindedClone = viewFinded;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        m_viewFindedClone = Instantiate(viewFinded , Vector3.zero , Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0) && !m_isShooted)
        {
            ControlAud.Ins.PlaySound(ControlAud.Ins.shooting);
            CineController.Ins.ShakeTrigger();
            Shoot(mouse);
            m_isShooted = true;
        }
        if(m_isShooted)
        {
            m_fireRate -= Time.deltaTime;
            if(m_fireRate <= 0)
            {
                m_isShooted = false;
                m_fireRate = fireRate;
            }
            Debug.Log((float)m_fireRate / fireRate + "");
            GUIManager.Ins.UpdateFireRate((float) m_fireRate / fireRate);
        }
        if(m_viewFindedClone)
        {
            m_viewFindedClone.transform.position = new Vector3 (mouse.x, mouse.y, 0f);
        }
    }

    public void Shoot(Vector3 mouse)
    {
        Vector3 shootDir = Camera.main.transform.position - mouse;
        shootDir.Normalize();
        RaycastHit2D[] hit2Ds = Physics2D.RaycastAll(mouse, shootDir);
        if(hit2Ds != null && hit2Ds.Length > 0)
        {
            for (int i = 0; i < hit2Ds.Length; i++)
            {
                RaycastHit2D hit = hit2Ds[i];
                
                if(hit.collider && Vector3.Distance((Vector2) hit.collider.transform.position, (Vector2) mouse) <= 0.5f)
                {
                    Bird bird = hit.collider.GetComponent<Bird>();
                    if(bird)
                    {
                        bird.Die();
                        ControlGame.Ins.point++;
                        GUIManager.Ins.UpdateScore(ControlGame.Ins.point);
                    }
                }
            }

            
        }

    }
}
