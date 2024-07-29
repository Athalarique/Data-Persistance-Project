using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SocialPlatforms.Impl;
using System.Xml.Linq;
using TMPro;



public class StartMenuMainManeger : MonoBehaviour
{
    public static StartMenuMainManeger Instance;
    

    public string CurrentPlayerName; // actual typed name on main menu
    public int CurrentPlayerScore;
    public List<string> NameList = new List<string>(); // pouzije sa pri loadovani
    public List<int> ScoreList = new List<int>();
    public string HighScoreName;
    public int HighScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //InputNameField = GetComponent<TMP_InputField>();

        LoadPlayerName();
        LoadHighScore();
    }

    private void Start()
    {
        //InputNameField.interactable = true; // asi nema vyznam?
        //InputNameField = GetComponent<TMP_InputField>();
    }

    //private void Update()
    //{
    //    if (InputNameField.isFocused)
    //    {
    //        OnEnteringInNameField();
    //    }
    //}
    

    [System.Serializable]
    class SaveName
    {
        public string Name;
    }

    class SaveData
    {
        //public string Name;
        //public int Score;

        public List<string> NameList;
        public List<int> ScoreList;
        //class Player
        //{
        //    public string Name; 
        //    public int Score;
        //}
    }

    class SaveHighScoreData
    {
        public string highScoreName;
        public int highScore;
    }

    public void Save(string savingName, int savingScore) //nieje potrebne, staci len savehighscore
    {
        //Debug.Log("tu tu tu");
        Debug.Log("Name: " + savingName);
        SaveData data = new SaveData();

        Debug.Log("count " + NameList.Count);
        if (NameList.Count > 0) { 
            
            for (int i = 0; i < NameList.Count; i++)
            {
                data.NameList.Add(savingName);
                data.ScoreList.Add(savingScore);
                //data.NameList[i] = savingName;
                //data.ScoreList[i] = savingScore;
            }
        } else
        {
            //data.NameList[0] = savingName;
            //data.ScoreList[0] = savingScore;
        }

        string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load() 
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            for (int i = 0; i < NameList.Count; i++)
            {
                NameList[i] = data.NameList[i];
                ScoreList[i] = data.ScoreList[i];
            }
        }
    }

    public void SavePlayerName()
    {
        //Name = InputNameField.text;
        //Debug.Log("tu tu tu");
        Debug.Log("Name: " + CurrentPlayerName);
        SaveName data = new SaveName();
        data.Name = CurrentPlayerName;


        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savenamefile.json", json);
    }
    public void LoadPlayerName()
    {
        string path = Application.persistentDataPath + "/savenamefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveName data = JsonUtility.FromJson<SaveName>(json);
            CurrentPlayerName = data.Name;
        }

        //InputNameField = new InputNameField;
    }

    public void SaveHighScore()
    {
        SaveHighScoreData data = new SaveHighScoreData();
        data.highScoreName = HighScoreName;
        data.highScore = HighScore;


        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveHighScorefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/saveHighScorefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveHighScoreData data = JsonUtility.FromJson<SaveHighScoreData>(json);
            HighScoreName = data.highScoreName;
            HighScore = data.highScore;
        }

        //InputNameField = new InputNameField;
    }
}
