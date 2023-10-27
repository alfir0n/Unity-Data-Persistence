using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class UiManager : MonoBehaviour
{
    public const string highscorePath = "/highscore.json";

    public TMP_InputField nameInput;
    public TMP_Text highscoreText;

    public PlayerData currentPlayerData;
    public PlayerData highscorePlayerData;

    public static UiManager instance;



    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        instance.LoadHighScore();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        //set data
        UiManager.instance.currentPlayerData = new PlayerData();
        UiManager.instance.currentPlayerData.name = nameInput.text;
    }

    [SerializeField]
    public class PlayerData
    {
        public string name;
        public int highScore;
    }

    public void SaveHighScore( int score )
    {
        if ( score < UiManager.instance.highscorePlayerData.highScore)
        {
            return;
        }

        UiManager.instance.currentPlayerData.highScore = score;
        File.WriteAllText(Application.persistentDataPath + highscorePath, JsonUtility.ToJson(UiManager.instance.currentPlayerData));
        Debug.Log("save data");
    }

    public void LoadHighScore()
    {
        if (File.Exists(Application.persistentDataPath + highscorePath))
        {
            UiManager.instance.highscorePlayerData = JsonUtility.FromJson<PlayerData>(File.ReadAllText(Application.persistentDataPath + highscorePath));
            if (highscoreText!= null)
            {
                highscoreText.text = "High Score: " + UiManager.instance.highscorePlayerData.name + " - " + UiManager.instance.highscorePlayerData.highScore;
            }
            Debug.Log("load data");
        }

    }
}
