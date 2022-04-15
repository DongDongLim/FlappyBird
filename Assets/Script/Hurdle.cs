using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdle : MonoBehaviour
{
    GameMng gameManager;

    private void Awake()
    {
        gameManager = GetComponentInParent<GameMng>();
        gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        transform.position += Vector3.left * Time.deltaTime * gameManager.m_gameSpeed;
        if (transform.position.x <= -1)
        {
            gameManager.DelateHurdle(gameObject);
        }
    }

    private void OnEnable()
    {
        transform.position = Vector3.right;
    }
}
