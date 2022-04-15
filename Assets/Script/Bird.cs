using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    [SerializeField]
    float m_jumpImpact;

    public UnityAction OnGameOver;
    public UnityAction OnDieAnim;
    Rigidbody2D m_rigidbody2D;
    Animator m_animator;
    GameMng m_gameMng;

    private void Start()
    {
        m_gameMng = GetComponentInParent<GameMng>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        OnDieAnim += BirdDieAnim;
    }

    private void Update()
    {
        if (!GetComponent<Collider2D>().isTrigger)
        {
            Jump();
            JumpAnim();           
        }

    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            m_rigidbody2D.velocity = Vector2.zero;
            m_rigidbody2D.AddForce(Vector2.up * m_jumpImpact, ForceMode2D.Impulse);

        }
        if (m_rigidbody2D.velocity.y < -1)
            m_rigidbody2D.velocity = Vector2.down;
    }

    void JumpAnim()
    {
        if (m_rigidbody2D.velocity.y > 0)
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

    void BirdDieAnim()
    {
        m_rigidbody2D.velocity = Vector2.zero;
        m_rigidbody2D.gravityScale = 0;
        GetComponent<Collider2D>().isTrigger = true;
        m_animator.SetTrigger("Die");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Die")
            if (!GetComponent<Collider2D>().isTrigger)
                OnDieAnim?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Score")
            if (!GetComponent<Collider2D>().isTrigger)
                m_gameMng.ScoreUp();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Die")
            if (!GetComponent<Collider2D>().isTrigger)
                OnDieAnim?.Invoke();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

}
