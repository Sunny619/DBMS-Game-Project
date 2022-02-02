using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Highscores : MonoBehaviour
{
    SqliteDatabase DB;
    public Converter C;
    public GameObject cardPrefab;
    public GameObject content;
    void Awake()
    {
        DB = new SqliteDatabase("GameDB.db");
        C = new Converter();
        FetchHighscores();
    }
    void Start()
    {
        FetchHighscores();
    }
    void FetchHighscores()
    {
        int childs = content.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(content.transform.GetChild(i).gameObject);
        }
        string query = "Select * from highscores";
        DataTable res = DB.ExecuteQuery(query);
        foreach (DataRow row in res.Rows)
        {
            GameObject card = Instantiate(cardPrefab) as GameObject;
            card.SetActive(true);
            card.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = row["username"].ToString();
            card.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = row["highscore"].ToString();
            card.transform.SetParent(content.transform, false);
        }
    }
    public void UpdateHighscores()
    {
        if(PlayerPrefs.GetInt("coins")>PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore",PlayerPrefs.GetInt("coins"));
            string query = C.SUpdateTable("playerprefs","highscore",PlayerPrefs.GetInt("highscore",0).ToString(),PlayerPrefs.GetString("username"));
            Debug.Log(query);
            DB.ExecuteNonQuery(query);
        }
            
    }
}
