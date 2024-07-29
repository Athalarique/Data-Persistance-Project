using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Linq;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class UIPlayScene : MonoBehaviour
{
    public static UIPlayScene Instance { get; private set; }
    //public string Name;

    public Text TopText;
    public Text ScoreText;


    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartMenuMainManeger.Instance.LoadHighScore();
        StartMenuMainManeger.Instance.LoadPlayerName();
        TopText.text = "Best Score : " + StartMenuMainManeger.Instance.HighScoreName + " : " + StartMenuMainManeger.Instance.HighScore;
        //StartMenuMainManeger.Instance.Load();
        //if (StartMenuMainManeger.Instance.NameList.Count > 0) { 
        //    TopText.text = "Best Score : " + StartMenuMainManeger.Instance.HighScoreName + " : " + StartMenuMainManeger.Instance.HighScore;
        //}

        
    }

    public void GoBack()
    {
        StartMenuMainManeger.Instance.SavePlayerName();
        SceneManager.LoadScene(0);
    }

    public void ResetHighScore()
    {
        StartMenuMainManeger.Instance.HighScore = 0;
        StartMenuMainManeger.Instance.HighScoreName = null;
        StartMenuMainManeger.Instance.SaveHighScore();
        TopText.text = "Best Score : " + StartMenuMainManeger.Instance.HighScoreName + " : " + StartMenuMainManeger.Instance.HighScore;
    }
}
