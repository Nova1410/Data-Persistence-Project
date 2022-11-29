using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public string playerName;
    public string playerHSName;
    public int score;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string playerHSName;
        public int score;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.playerHSName = playerName;
        data.score = score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerHSName = data.playerHSName;
            score = data.score;

            scoreText.text = "Best Score: " + playerHSName + " - " + score;
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GetName(string nameInput)
    {
        playerName = nameInput;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
