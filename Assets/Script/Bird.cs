using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    float m_jumpImpact;

    Rigidbody2D m_rigidbody2D;
    Animator m_animator;

    private void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            m_rigidbody2D.velocity = Vector2.zero;
            m_rigidbody2D.AddForce(Vector2.up * m_jumpImpact, ForceMode2D.Impulse);

        }
        if (m_rigidbody2D.velocity.y < -1)
            m_rigidbody2D.velocity = Vector2.down;
        if(m_rigidbody2D.velocity.y > 0)
        {
            if (!m_animator.GetBool("IsUp"))
                m_animator.SetBool("IsUp", true);
        }
        else
        {
            if (m_animator.GetBool("IsUp"))
                m_animator.SetBool("IsUp", false);
        }

    }

}
