using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LoginMenu : MonoBehaviour
{
    public TextMeshProUGUI usernameText;
    public GameObject loginSucessPanel;
    public TextMeshProUGUI wrongPassText;
    SqliteDatabase DB;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    string username;
    string password;
    Converter C = new Converter();
    void Awake()
    {
        PlayerPrefs.DeleteAll();
        DB = new SqliteDatabase("GameDB.db");
    }
    public void Login()
    {
        username = usernameInput.text;
        password = passwordInput.text;
        if(username.Contains("\"")||password.Contains("\""))
        {
            wrongPassText.text = "Use of \" character is restricted";
            return;
        }
        DataTable a  = DB.ExecuteQuery("Select * from player where username = \""+username+"\" and password = \""+password+"\";");
        if(a.Rows.Count==0)
        {
            LoginFail();
        }
        else
        {
            LoginSuccess();
        }
        Debug.Log(username + " " + password);
        
        
    }
    void LoginFail()
    {
        wrongPassText.text = "Username or password is wrong";
    }
    void LoginSuccess()
    {
        PlayerPrefs.SetString("username",username);
        wrongPassText.text="";
        loginSucessPanel.SetActive(true);
        usernameText.text+= PlayerPrefs.GetString("username","user");
        InitPrefs();
        Invoke("GotoMainMenu",2f);
    }
    void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void InitPrefs()
    {
        string current_skin ="", current_bg="", coins="",progress="", volume="";
        DataTable dt= DB.ExecuteQuery(C.SelectTable("playerprefs",PlayerPrefs.GetString("username","user")));
        foreach(DataRow row in dt.Rows)
        {
            current_skin += row["current_skin"].ToString();
            current_bg += row["current_bg"].ToString();
            coins += row["coins"].ToString();
            progress += row["progress"].ToString();
            volume += row["volume"].ToString();
        }
        Debug.Log(current_skin+current_bg+coins+progress+volume);
        PlayerPrefs.SetInt("current_skin",Int32.Parse(current_skin));
        PlayerPrefs.SetInt("current_bg",Int32.Parse(current_bg));
        PlayerPrefs.SetInt("coins",Int32.Parse(coins));
        PlayerPrefs.SetInt("progress",Int32.Parse(progress));
        PlayerPrefs.SetInt("volume",Int32.Parse(volume));
    }

}
