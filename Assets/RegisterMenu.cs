using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class RegisterMenu : MonoBehaviour
{
    //public TextMeshProUGUI usernameText;
    //public GameObject registerSucessPanel;
    //public GameObject userExistsText;
    SqliteDatabase DB;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Toggle genderInput;
    public TMP_InputField DOBInput;
    string username;
    string password;
    string gender;
    string DOB;
    void Awake()
    {
        PlayerPrefs.DeleteAll();
        DB = new SqliteDatabase("GameDB.db");
    }
    public void Register()
    {
        //TODO:Check is user exists
        //TODO:Check Password and store playerprefs change scene
        username = usernameInput.text;
        password = passwordInput.text;
        gender = genderInput.isOn?"m":"f";
        DOB = DOBInput.text;
        // DataTable a  = DB.ExecuteQuery("Select * from Player where username = \""+username+"\" and password = \""+password+"\"");
        // if(a.Rows.Count==0)
        // {
        //     LoginFail();
        // }
        // else
        // {
        //     LoginSuccess();
        // }
        Debug.Log(username + " " + password+ " "+ gender + " " + DOB );
        
        
    }
    // void LoginFail()
    // {
    //     userExistsText.SetActive(true);
    // }
    // void LoginSuccess()
    // {
    //     PlayerPrefs.SetString("username",username);
    //     userExistsText.SetActive(false);
    //     registerSucessPanel.SetActive(true);
    //     usernameText.text+= PlayerPrefs.GetString("username","user");
    //     Invoke("GotoMainMenu",2f);
    // }
    // void GotoMainMenu()
    // {
    //     SceneManager.LoadScene("MainMenu");
    // }
    // void UserExists()
    // {
    //     //TODO:Check is user exists
    // }
    // Update is called once per frame
    void Update()
    {
        
    }
}
