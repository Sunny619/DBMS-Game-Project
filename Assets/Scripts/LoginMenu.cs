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
    public GameObject wrongPassText;
    SqliteDatabase DB;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    string username;
    string password;
    void Awake()
    {
        PlayerPrefs.DeleteAll();
        DB = new SqliteDatabase("GameDB.db");
    }
    public void Login()
    {
        username = usernameInput.text;
        password = passwordInput.text;
        DataTable a  = DB.ExecuteQuery("Select * from Player where username = \""+username+"\" and password = \""+password+"\"");
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
        wrongPassText.SetActive(true);
    }
    void LoginSuccess()
    {
        PlayerPrefs.SetString("username",username);
        wrongPassText.SetActive(false);
        loginSucessPanel.SetActive(true);
        usernameText.text+= PlayerPrefs.GetString("username","user");
        Invoke("GotoMainMenu",2f);
    }
    void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
