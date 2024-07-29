using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class UIMainMenu : MonoBehaviour
{
    public TMP_InputField InputNameField;
    public Text RecormanText;

    public void OnStart()
    {
        //StartMenuMainManeger.Instance.LoadHighScore();
        //RecormanText.text = "Best Score : " + StartMenuMainManeger.Instance.HighScoreName + " : " + StartMenuMainManeger.Instance.HighScore;
    }

    public void Update()
    {
        RecormanText.text = "Best Score : " + StartMenuMainManeger.Instance.HighScoreName + " : " + StartMenuMainManeger.Instance.HighScore;
    }
    public void OnEnteringInNameField()
    {
        StartMenuMainManeger.Instance.CurrentPlayerName = InputNameField.text;
         //Debug.Log("iinput: " + newText); // only pretexted text
        //Debug.Log("input: " + InputNameField.text);
        Debug.Log("input: " + StartMenuMainManeger.Instance.CurrentPlayerName);
    }
    public void StartNew()
    {   
        //string newName = StartMenuMainManeger.Instance.CurrentPlayerName;
        //StartMenuMainManeger.Instance.Save(InputNameField.text);
        //StartMenuMainManeger.Instance.SavePlayerName(StartMenuMainManeger.Instance.GetComponent<TMP_InputField>().text);
        StartMenuMainManeger.Instance.SavePlayerName();
        SceneManager.LoadScene(1);
    }

    
    public void Exit()
    {
        StartMenuMainManeger.Instance.SavePlayerName();
        //MainManager.Instance.SaveColor();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
