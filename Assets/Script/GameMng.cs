using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMng : MonoBehaviour
{
    public int m_gameSpeed;

    public int m_gameScore;
    public int m_highScore;

    public GameObject m_hurdleObj;
    public GameObject m_playerObj;

    Bird m_player;

    UIMng m_uiMng;

    IEnumerator coroutine;

    Queue<GameObject> m_hurdleObjPooling = new Queue<GameObject>();

    List<GameObject> m_activeHurdle = new List<GameObject>();



    private void Awake()
    {
        DontDestroyOnLoad(this);
        m_uiMng = GameObject.FindWithTag("UIMng").GetComponent<UIMng>();
        m_uiMng.Start += GameStartSetting;
        m_uiMng.OnGameStart += GameStart;
    }
    private void Start()
    {
        CreatePooling();
        coroutine = HurdleMng();
        m_highScore = 0;
    }

    IEnumerator HurdleMng()
    {
        while (true)
        {
            CreateHurdle();
            yield return new WaitForSeconds(1.5f);
        }
    }

    void GameStartSetting()
    {
        Time.timeScale = 0;
        m_gameScore = 0;
        m_uiMng.SetScore(m_gameScore);
        m_gameSpeed = 1;
        m_player = Instantiate(m_playerObj, GetComponent<Transform>(), false).GetComponent<Bird>();
        m_player.OnDieAnim += GameOverSetting;
        m_player.OnGameOver += m_uiMng.GameOver;
        m_player.OnGameOver += DelatePlayer;
        for (int i = 0; i < m_activeHurdle.Count; ++i)
            DelateHurdle(m_activeHurdle[0]);

        StartCoroutine(coroutine);
    }

    void GameStart()
    {
        Time.timeScale = 1;
    }

    void GameOverSetting()
    {
        m_gameSpeed = 0;
        m_highScore = m_highScore < m_gameScore ? m_gameScore : m_highScore;
        StopCoroutine(coroutine);
        m_uiMng.SetHighScore(m_highScore);
    }


    void CreatePooling()
    {
        for (int i = 0; i < 10; ++i)
            m_hurdleObjPooling.Enqueue(Instantiate(m_hurdleObj, GetComponent<Transform>(), false));
    }

    void CreateHurdle()
    {
        if (0 != m_hurdleObjPooling.Count)
        {
            GameObject obj = m_hurdleObjPooling.Dequeue();
            obj.SetActive(true);
            m_activeHurdle.Add(obj);
        }
        else
        {
            CreatePooling();
            CreateHurdle();
        }
    }

    void DelatePlayer()
    {
        Destroy(m_player.gameObject);
    }

    public void DelateHurdle(GameObject obj)
    {
        m_activeHurdle.Remove(obj);
        obj.SetActive(false);
        m_hurdleObjPooling.Enqueue(obj);
    }

    public void ScoreUp()
    {
        m_uiMng.SetScore(++m_gameScore);
    }


}
