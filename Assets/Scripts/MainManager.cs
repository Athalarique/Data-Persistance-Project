using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public Text TopText;

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private string CurrentPlayerName;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        CurrentPlayerName = StartMenuMainManeger.Instance.CurrentPlayerName;
        ScoreText.text = CurrentPlayerName + $" : Score : {m_Points}";

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        StartMenuMainManeger.Instance.CurrentPlayerScore = m_Points;
        ScoreText.text = CurrentPlayerName + $" : Score : {m_Points}";
    }

    public void GameOver()
    {
        CheckHighScore();
        //StartMenuMainManeger.Instance.Score = m_Points;
        //StartMenuMainManeger.Instance.Save();
        //Debug.Log("game over name " + CurrentPlayerName);
        Debug.Log("game over m_Points" + m_Points);
        //StartMenuMainManeger.Instance.Save(CurrentPlayerName, m_Points);
        m_GameOver = true;
        GameOverText.SetActive(true);
        
    }


    private void CheckHighScore()
    {
        Debug.Log("game over debug m_Points " + m_Points);

        StartMenuMainManeger.Instance.LoadHighScore();

        if (StartMenuMainManeger.Instance.HighScore < 1)
        {
            Debug.Log("SAVING HIGH SCORE ");
            StartMenuMainManeger.Instance.HighScoreName = CurrentPlayerName;
            StartMenuMainManeger.Instance.HighScore = m_Points;
        }else if(m_Points > StartMenuMainManeger.Instance.HighScore)
        {
            Debug.Log("SAVING HIGH SCORE ");
            StartMenuMainManeger.Instance.HighScoreName = CurrentPlayerName;
            StartMenuMainManeger.Instance.HighScore = m_Points;
        }

        StartMenuMainManeger.Instance.SaveHighScore();
        Debug.Log("game over debug high score " + StartMenuMainManeger.Instance.HighScore);
        TopText.text = "Best Score : " + StartMenuMainManeger.Instance.HighScoreName + " : " + StartMenuMainManeger.Instance.HighScore;
    }

    //private void CheckHighScore()
    //{
    //    Debug.Log("game over debug m_Points " + m_Points);

    //    StartMenuMainManeger.Instance.Load();

    //    if (StartMenuMainManeger.Instance.NameList.Count == 0)
    //    {
    //        StartMenuMainManeger.Instance.NameList.Add(CurrentPlayerName);
    //        StartMenuMainManeger.Instance.ScoreList.Add(m_Points);
    //    }

    //    for (int i = 0; i < StartMenuMainManeger.Instance.NameList.Count; i++)
    //    {
    //        if (m_Points > StartMenuMainManeger.Instance.ScoreList[i])
    //        {
    //            if (StartMenuMainManeger.Instance.NameList[i] == CurrentPlayerName)
    //            {
    //                StartMenuMainManeger.Instance.ScoreList[i] = m_Points;
    //                StartMenuMainManeger.Instance.HighScoreName = CurrentPlayerName;
    //                StartMenuMainManeger.Instance.HighScore = m_Points;
    //            }
    //            else
    //            {
    //                StartMenuMainManeger.Instance.NameList.Add(CurrentPlayerName);
    //                StartMenuMainManeger.Instance.ScoreList.Add(m_Points);
    //                StartMenuMainManeger.Instance.HighScoreName = CurrentPlayerName;
    //                StartMenuMainManeger.Instance.HighScore = m_Points;
    //            }
    //        }
    //        else { }
    //    }

    //    StartMenuMainManeger.Instance.SaveHighScore();
    //    Debug.Log("game over debug high score " + StartMenuMainManeger.Instance.HighScore);
    //}
}
