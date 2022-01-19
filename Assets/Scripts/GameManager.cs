using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    SqliteDatabase DB;
    public static GameManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DB = new SqliteDatabase("GameDB.db");
        
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }
    public void GotoMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Resume();
        Application.Quit();
    }
    public void UpdateDBCoins(int coin)
    {
        string query = "Update playerprefs set coins = " + coin +" where username = \""+PlayerPrefs.GetString("username","user")+"\"";
        Debug.Log(query);
        DB.ExecuteNonQuery(query);
    }
}
