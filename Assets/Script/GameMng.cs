using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    int m_gameSpeed = 1;

    public GameObject m_hurdleObj;

    Queue<GameObject> m_hurdleObjPooling = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < 3; ++i)
            m_hurdleObjPooling.Enqueue(Instantiate(m_hurdleObj, GetComponent<Transform>(), false));
    }

}
